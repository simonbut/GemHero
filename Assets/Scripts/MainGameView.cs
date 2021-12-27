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
    public ItemCanvas itemCanvas;
    
    //public TagBaseCanvas tagBaseCanvas;
    public PlayerTagChoosingCanvas playerTagChoosingCanvas;
    public DestinyShareChoosingCanvas destinyShareChoosingCanvas;

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
                Database.TimePass(10);
                MainGameView.Instance.assetConfirmCanvas.AddUI(ResourcePointManager.Instance.DrawAsset(reactingObject.resourcePointId));
                //UIManager.Instance.assetDataUI.Show(ResourcePointManager.Instance.DrawAsset(reactingObject.resourcePointId));
                break;
            case ResourceType.talk:
                TalkDialogList _td = ResourcePointManager.Instance.GetTalkData(reactingObject.resourcePointId);
                if (!Database.userDataJson.destinyShareCompletion.Contains(_td.characterId))
                {
                    MainGameView.Instance.dialogCanvas.Setup(3, DestinyShare);
                }
                else
                {
                    if (!Database.userDataJson.questCompletion.Contains(_td.afterItemQuest.questId))
                    {
                        MainGameView.Instance.dialogCanvas.Setup(5, ItemQuest);
                    }
                    else
                    {
                        if (!Database.userDataJson.questCompletion.Contains(_td.afterBattleQuest.questId))
                        {
                            MainGameView.Instance.dialogCanvas.Setup(6, BattleQuest);
                        }
                        else
                        {
                            MainGameView.Instance.dialogCanvas.Setup(_td.normal.targetDialogId, null);
                        }
                    }
                }
                break;
            case ResourceType.mainQuest:
                MainQuestDialogList _md = ResourcePointManager.Instance.GetMainQuestData(reactingObject.resourcePointId);
                if (_md.talkQuest != null)
                {
                    if (!Database.userDataJson.questCompletion.Contains(_md.talkQuest.questId))
                    {
                        MainGameView.Instance.dialogCanvas.Setup(_md.talkQuest.targetDialogId, CheckQuestAfterMainTalkQuest);
                    }
                }
                else
                {
                    if (_md.beforeMainBattleQuest != null)
                    {
                        if (!Database.userDataJson.questCompletion.Contains(_md.beforeMainBattleQuest.questId))
                        {
                            MainGameView.Instance.dialogCanvas.Setup(_md.beforeMainBattleQuest.targetDialogId, MainBattleQuest);
                        }
                    }
                }
                break;
        }
    }

    public void CheckQuestAfterMainTalkQuest()
    {
        MainQuestDialogList _md = ResourcePointManager.Instance.GetMainQuestData(reactingObject.resourcePointId);
        Database.AddQuest(_md.talkQuest.afterQuestId);
    }

    public void MainBattleQuest()
    {
        CheckQuestAfterMainBattleQuest();//TODO
    }

    public void CheckQuestAfterMainBattleQuest()
    {
        MainQuestDialogList _md = ResourcePointManager.Instance.GetMainQuestData(reactingObject.resourcePointId);
        Database.AddQuest(_md.afterMainBattleQuest.afterQuestId);
    }

    public void ItemQuest()
    {
        TalkDialogList _td = ResourcePointManager.Instance.GetTalkData(reactingObject.resourcePointId);
        QuestData _qd = QuestManager.Instance.GetQuestData(_td.afterItemQuest.questId);
        MainGameView.Instance.itemCanvas.AddUI(ConfirmItemSubmit, _qd.itemQuality, _qd.itemTag, _qd.itemId);
    }

    public void CheckQuestAfterItemQuest()
    {
        TalkDialogList _td = ResourcePointManager.Instance.GetTalkData(reactingObject.resourcePointId);
        Database.ConsumeQuest(_td.afterItemQuest.questId);
        Database.AddQuest(_td.afterItemQuest.afterQuestId);
    }

    public void BattleQuest()
    {
        CheckQuestAfterBattleQuest();//TODO
    }

    public void CheckQuestAfterBattleQuest()
    {
        TalkDialogList _td = ResourcePointManager.Instance.GetTalkData(reactingObject.resourcePointId);
        Database.ConsumeQuest(_td.afterBattleQuest.questId);
        Database.AddQuest(_td.afterBattleQuest.afterQuestId);
    }

    public void DestinyShare()
    {
        TalkDialogList _td = ResourcePointManager.Instance.GetTalkData(reactingObject.resourcePointId);
        destinyShareChoosingCanvas.AddUI(_td.characterId);
    }

    public void CheckQuestAfterDestinyShare()
    {
        TalkDialogList _td = ResourcePointManager.Instance.GetTalkData(reactingObject.resourcePointId);
        Database.AddQuest(_td.afterDestinyShare.afterQuestId);
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
    }

    public void LeaveDestinyShare()
    {
        //TODO
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

    public void ConfirmItemSubmit(int uid, GridItem gi)
    {
        GlobalCommunicateManager.varInt = uid;
        UIManager.Instance.choiceUI.Setup(new Vector2(Screen.width / 2f, Screen.height / 2f), new List<string> { "Submit", "Redo" }, new List<Callback> { ItemSubmit, null });
    }

    public void ItemSubmit()
    {
        Database.ConsumeAsset(GlobalCommunicateManager.varInt);
        TalkDialogList _td = ResourcePointManager.Instance.GetTalkData(reactingObject.resourcePointId);
        UIManager.Instance.HideAllDataUI();
        UIManager.Instance.OnBackPressed();
        MainGameView.Instance.dialogCanvas.Setup(_td.afterItemQuest.targetDialogId, CheckQuestAfterItemQuest);
    }

    public void Sleep()
    {
        Database.TimePass(2 * 60);
        Database.RecoverHp(Database.userDataJson.hpTotal);
    }
}
