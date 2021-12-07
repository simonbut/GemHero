using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClassHelper;
using UnityEngine.UI;

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
        gridScrollView.Setup("Asset List", this, ClickData, SelectingData, DisSelectingData);

        GenerateList();
    }

    void GenerateList()
    {
        foreach (Asset _a in AssetManager.Instance.GetAssetListByType(recipe.assetTypeList[session]))
        {
            GameObject gridItemInstance = gridScrollView.GenerateItem("", _a.assetUid);//Graph TODO
            //Graph if used
        }
    }

    void ClickData(int uid, GridItem gi)
    {
        foreach (int _usedUid in assetSelectList)
        {
            if (_usedUid == uid)
            {
                print("this is already used");
                return;
            }
        }
        assetSelectList[session] = uid;
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
        if (!UIManager.Instance.IsCurrentUI(this))
        {
            return;
        }

        //if (JoyStickManager.Instance.IsInputDown("Cross") || Input.GetButtonDown("Escape"))
        //{
        //    OnBackPressed();
        //}
    }

    public List<GameObject> sessions;
    public int[] assetSelectList;
    RecipeData recipe;
    public int session = 0;
    public void AddUI(int _recipeId)
    {
        recipe = AssetManager.Instance.GetRecipeData(_recipeId);
        assetSelectList = new int[recipe.assetTypeList.Count];
        DefineSessions();
        session = 0;
        AddUI();
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
            print("NextStep");
            return;
        }
        else
        {
            SelectSession(nextSession);
        }
    }

    public void SelectSession(int _session)
    {
        print("select session " + _session);
        session = _session;
        Refresh();
    }
}
