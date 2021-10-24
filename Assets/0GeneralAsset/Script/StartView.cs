using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ClassHelper;
using UnityEngine.UI;

public class StartView : MonoBehaviour
{
    #region instance
    private static StartView m_instance;

    public static StartView Instance
    {
        get
        {
            return m_instance;
        }
    }

    void Awake()
    {
        m_instance = this;
    }
    #endregion


    public StartPanelMain startPanelMain;
    //public GameModePanel gameModePanel;
    public AchievementDataUI achievementDataUI;
    //public TotemDataUI totemDataUI;
    //public GameObject hardButton;

    //public GameObject mainCanvasObject;
    [SerializeField] Image frontBlacklay;

    //public GameModePanel gameModelPanel;
    //public SelectChapterPanel selectChapterPanel;

    // Start is called before the first frame update
    void Start()
    {
        AchievementManager.Instance.SyncSteamAchievements();

        TimeScaleManager.Instance.Init();
        Cursor.visible = true;
        //AudioManager.instance.PlayBGM(AudioManager.instance.test);//

        UIManager.Instance?.CleanAllUI();
        startPanelMain.AddUI(); // This is base UI

        //if (!Database.globalData.isWelcomeTutorialComplete)
        //{
        //    UIManager.Instance.AddInformationUI("TutorialHint");
        //}

        GlobalCommunicateManager.insideGame = false;


        Database.SaveGlobalSave();
        //test
        //UIManager.Instance.AddCreditUI();
    }

    // Update is called once per frame
    void Update()
    {
        Database.descriptionMessagePos = new Vector2(Input.mousePosition.x * 1f / Screen.width * 160f, Input.mousePosition.y * 1f / Screen.height * 90f) - new Vector2(160 / 2, 90 / 2);



    }

    public void NewGameButton()
    {
        GlobalCommunicateManager.isTutorial = false;
        //selectChapterPanel.AddUI();
        //if (TotemManager.Instance.GetTotemGetList().Count<= 4)
        //{
        //    GlobalCommunicateManager.chapter = 1;
        //    NormalModeGameStart();
        //}
        //else
        //{
        //    selectChapterPanel.AddUI();
        //    //if (!Database.globalData.isChapter1Complete)
        //    //{
        //    //    ChooseChapter(1);
        //    //    //SelectChapter(1);
        //    //    //GlobalCommunicateManager.chapter = 1;
        //    //    //gameModelPanel.AddUI();
        //    //}
        //    //else
        //    //{
        //    //    selectChapterPanel.AddUI();
        //    //}
        //}
    }

    void StartNewGame()
    {
        //StartCoroutine(LeavingSceneTransition());
        Database.SetDescriptionMessage("");
        Resources.UnloadUnusedAssets();
        //AudioManager.instance.PlaySFX(AudioManager.instance.gameStart, AudioManager.instance.Battle_Start);//game start
    }

    public void TutorialClicked()
    {
        GlobalCommunicateManager.isTutorial = true;
        StartNewGame();
    }

    //public void NewGameClicked()
    //{

    //}

    //public void GotoLoadingSceneB()
    //{
    //    //if player is a newbie
    //    //if (TotemManager.Instance.GetFacilityTotemGetList().Count + TotemManager.Instance.GetFortressTotemGetList().Count + TotemManager.Instance.GetFightingTotemGetList().Count + TotemManager.Instance.GetFusionTotemGetList().Count <= 4)
    //    //{
    //    //    Database.StartNewGame(new Totem[] { TotemManager.Instance.GetTotem(1), TotemManager.Instance.GetTotem(2), TotemManager.Instance.GetTotem(3), TotemManager.Instance.GetTotem(4) }, GlobalCommunicateManager.isSandBox, GlobalCommunicateManager.isHard);
    //    //    //SceneManager.LoadSceneAsync("craftTest1");
    //    //    //StartCoroutine(LoadCraftSceneAsync());
    //    //    SceneManager.LoadScene("LoadingSceneB");
    //    //    return;
    //    //}
    //    if (!GlobalCommunicateManager.isSandBox)
    //    {
    //        Totem[] totemList = TotemManager.Instance.GenerateStartingFourTotem();
    //        Database.StartNewGame(new Totem[] { totemList[0], totemList[1], totemList[2], totemList[3] }, GlobalCommunicateManager.isSandBox, GlobalCommunicateManager.isHard, GlobalCommunicateManager.isClassic, GlobalCommunicateManager.isNormal, GlobalCommunicateManager.chapter);

    //        SceneManager.LoadScene("LoadingSceneB");
    //        //return;
    //    }
    //    else
    //    {
    //        SceneManager.LoadScene("ChooseThreeTotemScene");
    //    }
    //    //

    //}

    //IEnumerator LoadCraftSceneAsync()
    //{
    //    // The Application loads the Scene in the background as the current Scene runs.
    //    // This is particularly good for creating loading screens.
    //    // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
    //    // a sceneBuildIndex of 1 as shown in Build Settings.

    //    AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("craftTest1");

    //    // Wait until the asynchronous scene fully loads
    //    while (!asyncLoad.isDone)
    //    {
    //        yield return null;
    //    }
    //}

    //public void ChooseChapter(int chapterId)
    //{
    //    if (TotemManager.Instance.GetTotemGetList().Count <= 4)
    //    {
    //        GlobalCommunicateManager.chapter = 1;
    //        EasyModeGameStart();
    //        UIManager.Instance.AddEmptyUI();
    //        return;
    //    }

    //    GlobalCommunicateManager.chapter = chapterId;
    //    gameModelPanel.AddUI();
    //}

    public void ContinueClicked()
    {
        GlobalCommunicateManager.isTutorial = false;
        Resources.UnloadUnusedAssets();

        Database.ContinueGame();
        //SceneManager.LoadSceneAsync("craftTest1");
        //StartCoroutine(LoadCraftSceneAsync());
        SceneManager.LoadScene("LoadingSceneB");
    }

    public void LoadClicked()
    {
        GlobalCommunicateManager.isTutorial = false;
        SceneManager.LoadScene("LoadSaveScene");
    }

    public void ShowAchievementData(int index)
    {
        if (index == 0)
        {
            achievementDataUI.Hide();
            return;
        }
        achievementDataUI.Show(index);
    }

    //public void ShowTotemData(int totemId)
    //{
    //    if (totemId == 0)
    //    {
    //        totemDataUI.Hide();
    //        return;
    //    }
    //    totemDataUI.Show(totemId);
    //}

    public void OpenSettingUI()
    {
        UIManager.Instance.AddSettingUI();
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void GoToPatreon()
    {
        Application.OpenURL("https://www.patreon.com/firepillar2");
    }

    public void GoToDiscord()
    {
        Application.OpenURL("https://discord.gg/sZRGDS9");
    }


    //IEnumerator LeavingSceneTransition()
    //{
    //    UIManager.Instance.AddEmptyUI();
    //    gameModePanel.gameObject.SetActive(false);
    //    float timer = 0f;
    //    frontBlacklay.gameObject.SetActive(true);
    //    frontBlacklay.transform.SetAsLastSibling();

    //    while (timer < 1f)
    //    {
    //        timer += Time.deltaTime;

    //        frontBlacklay.color = new Color(0, 0, 0, MathManager.AbsSin(timer, 2f));
    //        //foreach (Transform _t in playerBasicUI.transform)
    //        //{
    //        //    if (_t.GetComponent<RectTransform>().position.y > Screen.height / 2f)
    //        //    {
    //        //        _t.GetComponent<RectTransform>().rotation = Quaternion.Euler(_t.GetComponent<RectTransform>().rotation.eulerAngles + new Vector3(-Time.deltaTime * 90f, 0, 0));
    //        //    }
    //        //    else
    //        //    {
    //        //        _t.GetComponent<RectTransform>().rotation = Quaternion.Euler(_t.GetComponent<RectTransform>().rotation.eulerAngles + new Vector3(Time.deltaTime * 90f, 0, 0));
    //        //    }
    //        //}

    //        yield return new WaitForEndOfFrame();
    //    }
    //    frontBlacklay.color = new Color(0, 0, 0, 1);

    //    if (GlobalCommunicateManager.isTutorial || Database.globalData.isWelcomeTutorialComplete)
    //    {
    //        GotoLoadingSceneB();
    //    }
    //    else
    //    {
    //        string uuid = MemoryManager.Instance.GetMemory(1, true).oice_uuid;
    //        Database.PlayOice(uuid, true, GotoLoadingSceneB);
    //        mainCanvasObject.SetActive(false);
    //        ControlMeaningUI.Instance?.Hide();
    //    }

    //}
}
