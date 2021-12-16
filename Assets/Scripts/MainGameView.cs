using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ClassHelper;

public class MainGameView : MonoBehaviour
{
    #region instance
    private static MainGameView m_instance;

    public static MainGameView Instance
    {
        get
        {
            return m_instance;
        }
    }

    void Awake()
    {
        if (MainGameView.Instance == null)
        {
            UIManager.Instance.CleanAllUI();
            m_instance = this;
        }
    }
    #endregion

    public GameObject gridPrefab;
    public GameObject tagBasePrefab;

    public ResourcePoint reactingObject;
    public List<ResourcePoint> resourcePointList = new List<ResourcePoint>();

    public InGameMainMenuUI inGameMainMenuUI;
    public RecipeMenuCanvas recipeMenuCanvas;
    public CompositeMenuCanvas compositeMenuCanvas;
    public CompositeTagChoosingCanvas tagChoosingCanvas;
    public AssetConfirmCanvas assetConfirmCanvas;
    public DialogCanvas dialogCanvas;
    
    public TagBaseCanvas tagBaseCanvas;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        InteractiveDialog.transform.position = MathManager.WorldPosToCanvasPos(ControlView.Instance.player.transform.position + Vector3.up * 1f);

        foreach (ResourcePoint _rp in resourcePointList)
        {
            if (_rp == null)
            {
                continue;
            }
            if (reactingObject == _rp)
            {
                _rp.GetComponent<SpriteRenderer>().material.SetFloat("IsReactable", 1);

                List<ResourcePointData> _rpd = ResourcePointManager.Instance.GetResourcePointDataList(reactingObject.resourcePointId);
                if (_rpd.Count <= 0)
                {
                    print("Bug");
                    return;
                }
                switch (_rpd[0].resourceType)
                {
                    case ResourceType.collect:
                        ShowInteractiveDialog("Collect");
                        break;
                    case ResourceType.talk:
                    case ResourceType.mainQuest:
                        ShowInteractiveDialog("Talk");
                        break;
                }
            }
            else
            {
                _rp.GetComponent<SpriteRenderer>().material.SetFloat("IsReactable", 0);
            }
        }
        if (reactingObject == null || !UIManager.Instance.IsNoUI())
        {
            HideInteractiveDialog();
        }
    }

    public GameObject InteractiveDialog;
    public void ShowInteractiveDialog(string _text)
    {
        InteractiveDialog.SetActive(true);
        InteractiveDialog.transform.Find("Text").GetComponent<Text>().text = _text;
    }

    public void HideInteractiveDialog()
    {
        InteractiveDialog.SetActive(false);
    }

    public void CharacterReact()
    {
        if (!UIManager.Instance.IsNoUI())
        {
            return;
        }
        if (reactingObject == null)
        {
            return;
        }

        List<ResourcePointData> _rpdl = ResourcePointManager.Instance.GetResourcePointDataList(reactingObject.resourcePointId);
        if (_rpdl.Count <= 0)
        {
            print("Bug");
            return;
        }
        switch (_rpdl[0].resourceType)
        {
            case ResourceType.collect:
                MainGameView.Instance.assetConfirmCanvas.AddUI(ResourcePointManager.Instance.DrawAsset(reactingObject.resourcePointId));
                //UIManager.Instance.assetDataUI.Show(ResourcePointManager.Instance.DrawAsset(reactingObject.resourcePointId));
                break;
            case ResourceType.talk:
            case ResourceType.mainQuest:
                ResourcePointData _rpd = _rpdl[0];//TODO determine which DialogType
                MainGameView.Instance.dialogCanvas.Setup(_rpd.targetDialogId, 1);
                break;
        }
    }

    public void OpenRecipeMenu()
    {
        recipeMenuCanvas.AddUI();
    }

    public void LeaveComposition()
    {
        UIManager.Instance.RemoveUI(compositeMenuCanvas);
        UIManager.Instance.RemoveUI(tagChoosingCanvas);
        compositeMenuCanvas.gameObject.SetActive(false);
        tagChoosingCanvas.gameObject.SetActive(false);

        UIManager.Instance.HideAllDataUI();
        tagBaseCanvas.Hide();
    }

    public ResourcePoint FindResourcePointByCharacterId(int _chId)
    {
        foreach (ResourcePoint _rp in resourcePointList)
        {
            if (ResourcePointManager.Instance.GetResourcePointDataList(_rp.resourcePointId)[0].characterId == _chId)
            {
                return _rp;
            }
        }
        return null;
    }
}
