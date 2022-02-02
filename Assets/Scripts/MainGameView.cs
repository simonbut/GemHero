using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
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

    public PlayerStatusCanvas playerStatusCanvas;

    public InGameMainMenuUI inGameMainMenuUI;
    public RecipeMenuCanvas recipeMenuCanvas;
    public CompositeMenuCanvas compositeMenuCanvas;
    public CompositeTagChoosingCanvas tagChoosingCanvas;
    public AssetConfirmCanvas assetConfirmCanvas;
    public DialogCanvas dialogCanvas;
    public ItemCanvas itemCanvas;
    public RewardAfterBattleCanvas rewardAfterBattleCanvas;
    public EquipmentCanvas equipmentCanvas;
    public GameoverCanvas gameoverCanvas;
    public LibraryCanvas libraryCanvas;
    public BookConfirmCanvas bookConfirmCanvas;

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
        if (Database.userDataJson.isSaveCorrupted)
        {
            return;
        }

        if (Player.GetHp() <= 0 || IsMainQuestTimeOut())
        {
            GameOver();
        }

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
                    print("Bug in " + reactingObject.resourcePointId);
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
                    case ResourceType.changePos:
                        ShowInteractiveDialog("React");
                        break;
                    case ResourceType.special:
                        switch (_rpd[0].resourcePointId)
                        {
                            case 101:
                                ShowInteractiveDialog("Composite");
                                break;
                            case 102:
                                ShowInteractiveDialog("PlayerTag");
                                break;
                            case 103:
                                ShowInteractiveDialog("Item");
                                break;
                            case 104:
                                ShowInteractiveDialog("Sleep");
                                break;
                        }
                        break;
                    case ResourceType.library:
                        ShowInteractiveDialog("Enter");
                        break;
                    case ResourceType.monster:
                        ShowInteractiveDialog("Fight");
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
            print("Bug in " + reactingObject.resourcePointId);
            return;
        }
        switch (_rpdl[0].resourceType)
        {
            case ResourceType.collect:
                Database.TimePass(30);
                MainGameView.Instance.assetConfirmCanvas.AddUI(new List<Asset> { ResourcePointManager.Instance.DrawAsset(reactingObject.resourcePointId),ResourcePointManager.Instance.DrawAsset(reactingObject.resourcePointId),ResourcePointManager.Instance.DrawAsset(reactingObject.resourcePointId)});
                //UIManager.Instance.assetDataUI.Show(ResourcePointManager.Instance.DrawAsset(reactingObject.resourcePointId));
                break;
            case ResourceType.talk:
                TalkDialogList _td = ResourcePointManager.Instance.GetTalkData(reactingObject.resourcePointId);
                if (!Database.userDataJson.destinyShareCompletion.Contains(_td.characterId))
                {
                    if (Database.userDataJson.sideQuest.Count >= 3)
                    {
                        //TODO warn player too many quest
                        return;
                    }
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
            case ResourceType.changePos:
                ChangeMap(_rpdl[0].mapId);
                ChangePlayerPos(_rpdl[0].pos);
                break;
            case ResourceType.special:
                switch (reactingObject.resourcePointId)
                {
                    case 101:
                        OpenCompositeCanvas();
                        break;
                    case 102:
                        OpenPlayerTagCanvas();
                        break;
                    case 103:
                        OpenItemCanvas();
                        break;
                    case 104:
                        OpenSleepCanvas();
                        break;
                }
                break;
            case ResourceType.library:
                libraryCanvas.AddUI(reactingObject.resourcePointId);
                break;
            case ResourceType.monster:
                TurnBaseBattleView.Instance.StartBattleByMonsterPoint(reactingObject.resourcePointId);
                break;
        }
    }

    public List<GameObject> mapList;
    public int currentMapId = 1;
    public void ChangeMap(int _mapId)
    {
        if (currentMapId == _mapId)
        {
            return;
        }
        for (int i = 0; i < mapList.Count; i++)
        {
            mapList[i].SetActive(false);
        }
        mapList[_mapId].SetActive(true);
        currentMapId = _mapId;

        Database.userDataJson.mapId = currentMapId;
        Database.Save();
    }

    public void ChangePlayerPos(Vector2 _pos)
    {
        ControlView.Instance.UpdateCameraPosition(new Vector3(_pos.x, _pos.y, 0) - ControlView.Instance.player.transform.position);
        ControlView.Instance.player.transform.position = _pos;
        Rope.Instance.Init();

        Database.userDataJson.pos = new Vector2Int(Mathf.FloorToInt(_pos.x), Mathf.FloorToInt(_pos.y));
        Database.Save();
    }

    public void OpenCompositeCanvas()
    {
        OpenRecipeMenu();
    }

    public void OpenPlayerTagCanvas()
    {
        playerTagChoosingCanvas.AddUI();
    }

    public void OpenItemCanvas()
    {
        itemCanvas.AddUI(ConfirmUseConsumable);
    }

    public void ConfirmUseConsumable(int uid, GridItem gi)
    {
        if (AssetManager.Instance.GetAssetByUid(uid).GetAssetData().GetCompoundType() != CompoundType.consumable)
        {
            return;
        }
        GlobalCommunicateManager.varInt = uid;
        UIManager.Instance.choiceUI.Setup(new Vector2(Screen.width / 2f, Screen.height / 2f), new List<string> { "Use", "Cancel" }, new List<Callback> { UseConsumable, null });
    }

    public void UseConsumable()
    {
        print("GlobalCommunicateManager.varInt " + GlobalCommunicateManager.varInt);
        Asset _a = AssetManager.Instance.GetAssetByUid(GlobalCommunicateManager.varInt);
        switch (_a.assetId)
        {
            //TODO consumable effect
            case 1:
                break;
        }
        Database.ConsumeAsset(GlobalCommunicateManager.varInt);

        UIManager.Instance.assetDataUI.Hide();
        itemCanvas.Refresh();
    }

    public void OpenEquipmentCanvas()
    {
        equipmentCanvas.AddUI();
    }

    public void OpenSleepCanvas()
    {
        UIManager.Instance.choiceUI.Setup(new Vector2(Screen.width / 2f, Screen.height / 2f), new List<string> { "Confirm", "Cancel" }, new List<Callback> { MainGameView.Instance.Sleep, null }, "Do you want to sleep? It will consume 2 hrs.");
    }

    public void Quit()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void CheckQuestAfterMainTalkQuest()
    {
        MainQuestDialogList _md = ResourcePointManager.Instance.GetMainQuestData(reactingObject.resourcePointId);
        Database.AddQuest(_md.talkQuest.afterQuestId);
    }

    public void MainBattleQuest()
    {
        MainQuestDialogList _md = ResourcePointManager.Instance.GetMainQuestData(reactingObject.resourcePointId);
        TurnBaseBattleView.Instance.StartBattleByQuest(_md.afterMainBattleQuest.questId);
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
        TalkDialogList _td = ResourcePointManager.Instance.GetTalkData(reactingObject.resourcePointId);
        TurnBaseBattleView.Instance.StartBattleByQuest(_td.afterBattleQuest.questId);
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
        Database.RecoverHp(Player.GetTotalHp());
    }

    public void GameOver()
    {
        Database.globalData.isSaveCorrupted = true;
        Database.userDataJson.isSaveCorrupted = true;
        Database.Save();
        gameoverCanvas.AddUI();
    }

    bool IsMainQuestTimeOut()
    {
        if (Database.userDataJson.mainQuest.questId > 0)
        {
            QuestData _q = QuestManager.Instance.GetQuestData(Database.userDataJson.mainQuest.questId);
            if (Database.userDataJson.time > _q.timeLimit + Database.userDataJson.mainQuest.startTime)
            {
                return true;
            }
        }

        return false;
    }
}
