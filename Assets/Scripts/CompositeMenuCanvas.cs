using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClassHelper;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class CompositeMenuCanvas : ControlableUI
{
    public GridScrollView gridScrollView;

    public override void OnRemoveUI()
    {
        UIManager.Instance.HideAllDataUI();

        base.OnRemoveUI();
    }


    public override void OnShow()
    {
        Refresh();

        base.OnShow();
    }

    public void Refresh()
    {
        //UIManager.Instance.assetInCompositionDataUI.Hide();
        //UIManager.Instance.compositionDataUI.Hide();

        gridScrollView.Setup("Asset List", this, ClickData, SelectingData, DisSelectingData);

        GenerateList();
    }

    void GenerateList()
    {
        foreach (Asset _a in AssetManager.Instance.GetAssetList(0, 0, 0, recipe.assetTypeList[session]))
        {
            GameObject gridItemInstance = gridScrollView.GenerateItem("Asset/" + _a.assetId.ToString("000"), _a.assetUid);
        }
    }

    void ClickData(int uid, GridItem gi)
    {
        for (int i = 0; i < assetSelectList.Length; i++)
        {
            if (assetSelectList[i] == uid)
            {
                if (session == i)
                {
                    print("disselect");
                    assetSelectList[session] = 0;
                    sessions[i].transform.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>(AssetManager.Instance.GetRecipeAssetIconPath(recipe.assetTypeList[i]));//"AssetType/" + AssetManager.Instance.GetAssetTypeData(recipe.assetTypeList[i]).id.ToString("000")

                    NextSession();
                    return;
                }
                else
                {
                    print("this is already used");
                    return;
                }
            }
        }
        assetSelectList[session] = uid;
        sessions[session].transform.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("Asset/" + AssetManager.Instance.GetAssetByUid(assetSelectList[session]).assetId.ToString("000"));
        NextSession();
    }

    void SelectingData(int uid, GridItem gi)
    {
        Asset _a = AssetManager.Instance.GetAssetByUid(uid);
        UIManager.Instance.assetInCompositionDataUI.Show(_a);
    }

    void DisSelectingData(int uid, GridItem gi)
    {

    }

    // Update is called once per frame
    void Update()
    {

        for (int i = 0; i < assetSelectList.Length; i++)
        {
            sessions[i].transform.Find("BlackOverlay").gameObject.SetActive(session != i);
            sessions[i].transform.Find("Tick").gameObject.SetActive(assetSelectList[i] != 0);
        }

        if (!UIManager.Instance.IsCurrentUI(this))
        {
            return;
        }

        if (ControlView.Instance.controls.Map1.Cancel.triggered)
        {
            UIManager.Instance.assetInCompositionDataUI.Hide();
            UIManager.Instance.compositionDataUI.Hide();
            OnBackPressed();
        }
    }

    public List<GameObject> sessions;
    public int[] assetSelectList;
    RecipeData recipe;
    public int session = 0;
    public void AddUI(int _recipeId)
    {
        SetupRecipe(_recipeId);

        UIManager.Instance.recipeDataUI.Hide();
        UIManager.Instance.compositionDataUI.Show(_recipeId, true);

        AddUI();
    }

    void SetupRecipe(int _recipeId)
    {
        recipe = AssetManager.Instance.GetRecipeData(_recipeId);
        assetSelectList = new int[recipe.assetTypeList.Count];
        DefineSessions();
        session = 0;
    }

    void DefineSessions()
    {
        List<int> atl = recipe.assetTypeList;
        for (int i = 0; i < sessions.Count; i++)
        {
            if (i < atl.Count)
            {
                sessions[i].SetActive(true);
                AssetTypeData _atd = AssetManager.Instance.GetAssetTypeData(atl[i]);
                sessions[i].transform.Find("Text").GetComponent<Text>().text = _atd.name.GetString();
                sessions[i].transform.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>(AssetManager.Instance.GetRecipeAssetIconPath(_atd.id));
            }
            else
            {
                sessions[i].SetActive(false);
            }
        }
    }

    void NextSession()
    {
        bool isAllChossen = true;
        int nextSession = 0;
        for (int i = 0; i < assetSelectList.Length; i++)
        {
            int _s = (i + session) % assetSelectList.Length;
            if (assetSelectList[_s] == 0)
            {
                isAllChossen = false;
                nextSession = _s;
                break;
            }
        }
        if (isAllChossen)
        {
            if (AssetManager.Instance.CreateTagListByAssets(assetSelectList).Count == 0)
            {
                UIManager.Instance.choiceUI.Setup(new Vector2(Screen.width / 2f, Screen.height / 2f), new List<string> { "Composite", "Redo" }, new List<Callback> { NextStep, ReSelectAsset });
            }
            else
            {
                UIManager.Instance.choiceUI.Setup(new Vector2(Screen.width / 2f, Screen.height / 2f), new List<string> { "Next Step", "Redo" }, new List<Callback> { NextStep, ReSelectAsset });
            }
            return;
        }
        else
        {
            SelectSession(nextSession);
        }
    }

    void NextStep()
    {
        UIManager.Instance.assetInCompositionDataUI.Hide();
        UIManager.Instance.compositionDataUI.Hide();
        //MainGameView.Instance.tagBaseCanvas.Show(recipe.shape, AssetManager.Instance.CreateTagList(TargetTagListWithEnoughPoints(), recipe.targetPos));

        if (AssetManager.Instance.CreateTagListByAssets(assetSelectList).Count == 0)
        {
            MainGameView.Instance.tagChoosingCanvas.Setup(assetSelectList, GetQuality(), recipe, AssetManager.Instance.CreateTagListByAssets(assetSelectList), AssetManager.Instance.CreateTagList(TargetTagListWithEnoughPoints(), recipe.targetPos));
            MainGameView.Instance.tagChoosingCanvas.CompleteComposite();
        }
        else
        {
            MainGameView.Instance.tagChoosingCanvas.AddUI(assetSelectList, GetQuality(), recipe, AssetManager.Instance.CreateTagListByAssets(assetSelectList), AssetManager.Instance.CreateTagList(TargetTagListWithEnoughPoints(), recipe.targetPos));
        }

    }

    void ReSelectAsset()
    {
        SetupRecipe(recipe.id);
    }

    public int GetQuality()
    {
        int result = 0;
        foreach (int _uid in assetSelectList)
        {
            if (_uid != 0)
            {
                result += AssetManager.Instance.GetAssetByUid(_uid).GetQuality();
            }
        }
        switch (GetScoreOverflow())
        {
            case 0:

                break;
            case 1:
                result = Mathf.FloorToInt(result * 0.5f);
                break;
            case 2:
                result = Mathf.FloorToInt(result * 0.3f);
                break;
            default:
                result = Mathf.FloorToInt(result * 0.2f);
                break;
        }
        return result;
    }

    public int GetScoreOverflow()
    {
        int result = 0;
        result += Mathf.Max(0, GetPoints()[0] - recipe.capacity[0]);
        result += Mathf.Max(0, GetPoints()[1] - recipe.capacity[1]);
        result += Mathf.Max(0, GetPoints()[2] - recipe.capacity[2]);
        return result;
    }

    public void SelectSession(int _session)
    {
        print("select session " + _session);
        session = _session;
        Refresh();
    }

    public List<int> TargetTagListWithEnoughPoints()
    {
        List<int> result = new List<int> ();
        for (int i = 0; i < 3; i++)
        {
            if (IsTargetReach(i))
            {
                result.Add(recipe.targetTag[i]);
            }
            else
            {
                result.Add(0);
            }
        }
        return result;
    }

    public bool IsTargetReach(int index)
    {
        return (recipe.targetScore[index] <= GetPoints()[index]);
    }

    public int[] GetPoints()
    {
        int[] result = new int[3];
        if (MainGameView.Instance.compositeMenuCanvas.assetSelectList != null)
        {
            foreach (int _uid in MainGameView.Instance.compositeMenuCanvas.assetSelectList)
            {
                if (_uid > 0)
                {
                    Asset _a = AssetManager.Instance.GetAssetByUid(_uid);
                    if (_a.GetRealityPoint()>0)
                    {
                        result[0] += _a.GetRealityPoint();
                    }
                    else
                    {
                        result[1] += -_a.GetRealityPoint();
                    }
                    result[2] += _a.GetIdealPoint();
                }
            }
        }
        return result;
    }
}
