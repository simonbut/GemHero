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


    public StartCanvas startCanvas;

    [SerializeField] Image frontBlacklay;

    // Start is called before the first frame update
    void Start()
    {
        UIManager.Instance.CleanAllUI();

        AchievementManager.Instance.SyncSteamAchievements();
        GlobalCommunicateManager.insideGame = false;

        startCanvas.AddUI(); // This is base UI

        Database.SaveGlobalSave();
    }

    public void NewGameClicked()
    {
        GlobalCommunicateManager.isTutorial = false;
        StartNewGame();
    }

    void StartNewGame()
    {
        Resources.UnloadUnusedAssets();
        Database.StartNewGame();
        SceneManager.LoadScene("TileMap");
    }

    public void TutorialClicked()
    {
        GlobalCommunicateManager.isTutorial = true;
        StartNewGame();
    }


    public void ContinueClicked()
    {
        GlobalCommunicateManager.isTutorial = false;

        Resources.UnloadUnusedAssets();
        Database.ContinueGame();
        SceneManager.LoadScene("TileMap");
        //SceneManager.LoadScene("LoadingSceneB");
    }

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


}
