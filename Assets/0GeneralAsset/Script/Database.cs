using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.IO;
using ClassHelper;
using Newtonsoft.Json;


public class Database : MonoBehaviour
{
    //[HideInInspector]
    //private static List<Status> status = new List<Status>();

    //public static Controller controller = new Controller();

    //public static StageDataBase stageDataJson = new StageDataBase();
    public static UserData userDataJson = new UserData();

    //public static bool isBattleTutorial = false;

    public static int saveId = 0;
    public static int seed = 0;
    //public static int stageInBattle = 0;

    public static float descriptionMessageAppendY = 0;
    //public static int bossEvent = 0;
    //public static string systemMessage = "";
    public static string descriptionMessage = "";
    public static string descriptionMessage2 = "";
    public static string descriptionMessage3 = "";
    public static Vector2 descriptionMessagePos = Vector2.zero;
    public static string descriptionMessageAlign = "";
    public static GlobalData globalData;
    public static string deadReason = "";

    public static bool isDemo = true;
    public static bool isTestMode = false;
    public static int versionNo = 20200130;



    //public static bool enterGameByLoadSave = false;

    //public static float energyConsumeVelocity = 100;

    public static bool isInit = false;

    #region Init
    public static void InitDatabaseData()
    {
        //Manager
        //AchievementManager.Instance.LoadAchievementData();
        //ParameterManager.Instance.LoadParameterData();
        TagManager.Instance.LoadTagData();
        //


        //LoadStatusData();
        LoadLocalizationData();

        //bossEvent = 0;
        deadReason = "";

        //globalData.language = Language.zh;//text
    }

    // Use this for initialization
    public static void Init()
    {

        LoadGlobalSave();

        InitDatabaseData();

        if (saveId != -1)
        {
            LoadSave(saveId);
        }
        else
        {
            StartNewGame();
        }

        Directory.CreateDirectory(UnityEngine.Application.persistentDataPath + "/stampset");

        SaveGlobalSave();

        isInit = true;
    }
    #endregion

    #region Save / Load

    public class UserData
    {

        public int chapter = 1;

        public float currentStageDifficulty = 1;

        public bool isSaveCorrupted;
        public bool isGameClear;

        public int version = 0;

        public int coin = 1000;

        //Progression
        //public bool isWelcomeTutorialComplete = false;
        //public bool isChapter1Complete = false;
        public List<int> completedStoryStages = new List<int>();
        public int lastStoryStage = 1;
        public List<int> completedTowerStages = new List<int>();
        public int lastTowerStage = 1;
        //

        //back door
        public bool isBackDoor = false;


    }




    public static void LoadSave(int _saveId)
    {
        //enterGameByLoadSave = true;
        globalData.lastLoadData = _saveId;
        SaveGlobalSave();
        if (System.IO.File.Exists(UnityEngine.Application.persistentDataPath + "/userDataJson" + _saveId + ".sav"))
        {
            InitDataFromSave(_saveId);
        }
        else
        {
            InitSave(_saveId, Random.Range(0, 100000), 1);
        }

        //if (isDemo)
        //{
        //    globalData.is_tutorial_clear = false;
        //}
    }

    public static void LoadGlobalSave()
    {
        if (!System.IO.File.Exists(UnityEngine.Application.persistentDataPath + "/globalSaveDataJson.sav"))
        {
            InitGlobalSaveData();
            return;
        }
        string _globalDataJson = System.IO.File.ReadAllText(UnityEngine.Application.persistentDataPath + "/globalSaveDataJson.sav");
        //globalData = JsonUtility.FromJson<GlobalData>(_globalDataJson);
        globalData = JsonConvert.DeserializeObject<GlobalData>(_globalDataJson);


        SaveGlobalSave();

        ApplySetting();
    }

    public class SaveListItem
    {
        public int save_id = 0;
        //public bool is_corrupting = false;
        public bool is_corrupted = false;
        public bool is_clear = false;

        public int currentStageId = 0;
        public float difficulty = 0;
        public int chapter = 1;
        //public int version_no = 0;

        public bool isHard = false;
        public bool isSandbox = false;
    }

    public static List<SaveListItem> GetSaveList()
    {
        List<SaveListItem> result = new List<SaveListItem>();
        int _saveId = 1;
        while (_saveId <= globalData.play_times)
        {
            if (System.IO.File.Exists(UnityEngine.Application.persistentDataPath + "/userDataJson" + _saveId + ".sav") && System.IO.File.Exists(UnityEngine.Application.persistentDataPath + "/stageDataJson" + _saveId + ".sav"))
            {
                SaveListItem _save = new SaveListItem();
                _save.save_id = _saveId;

                string _userDataJson = System.IO.File.ReadAllText(UnityEngine.Application.persistentDataPath + "/userDataJson" + _saveId + ".sav");
                //userDataJson = JsonUtility.FromJson<UserDataBase>(_userDataJson);
                userDataJson = JsonConvert.DeserializeObject<UserData>(_userDataJson);

                //_save.is_corrupting = userDataJson.isSaveCorrupting;
                _save.is_corrupted = userDataJson.isSaveCorrupted;
                _save.is_clear = userDataJson.isGameClear;
                _save.difficulty = Mathf.FloorToInt(userDataJson.currentStageDifficulty);
                _save.chapter = userDataJson.chapter;
                //_save.version_no = userDataJson.version;

                result.Add(_save);
            }
            _saveId++;
        }
        return result;
    }

    public static void InitSave(int _saveId, int _seed, int difficulty)
    {

        if (GlobalCommunicateManager.isTutorial)
        {
            saveId = -1;
        }
        else
        {
            if (System.IO.File.Exists(UnityEngine.Application.persistentDataPath + "/userDataJson" + _saveId + ".sav"))
            {
                globalData.play_times++;
                InitSave(_saveId + 1, _seed, difficulty);
                //LoadSave(_saveId);
                return;
            }
            saveId = _saveId;
            globalData.play_times++;
            globalData.lastLoadData = saveId;
        }
        seed = _seed;
        print(UnityEngine.Application.persistentDataPath);

        //stageDataJson = InitStageData(difficulty);
        userDataJson = InitUserData();



        //System.IO.File.WriteAllText(UnityEngine.Application.persistentDataPath + "/stageDataJson" + saveId + ".sav", JsonUtility.ToJson(stageDataJson));
        //System.IO.File.WriteAllText(UnityEngine.Application.persistentDataPath + "/userDataJson" + saveId + ".sav", JsonUtility.ToJson(userDataJson));

        //StreamWriter sw = new StreamWriter(UnityEngine.Application.persistentDataPath + "/stageDataJson" + saveId + ".sav", false);
        //sw.WriteLine(JsonConvert.SerializeObject(stageDataJson, Formatting.None,
        //            new JsonSerializerSettings()
        //            {
        //                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        //            }));
        //sw.Close();

        StreamWriter sw2 = new StreamWriter(UnityEngine.Application.persistentDataPath + "/userDataJson" + saveId + ".sav", false);
        sw2.WriteLine(JsonConvert.SerializeObject(userDataJson, Formatting.None,
                    new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    }));
        sw2.Close();

        InitDataFromSave(saveId);

        //if (!isBattleTutorial)
        //{
        //    globalData.lastLoadData = _saveId;
        //}
        SaveGlobalSave();

    }


    public class GlobalData
    {
        public bool isCredit1Seen = false;

        public List<int> completed_achievement_id = new List<int>();//Card
        public List<string> read_oice = new List<string>();//memory
        public int play_times = 0;
        public List<int> read_achievement_id = new List<int>();
        //public List<int> read_bonus_id = new List<int>();

        public int bgm = 10;
        public int sfx = 10;
        public int gameSpeed = 0;

        public bool isFullScreen = false;
        public bool isSimpleMode = false;

        public int screenResolution = 1;
        public Language language = 0;
        //public int speedInBattle = 1;

        public bool isFastEditTutorialClear = false;

        public int lastLoadData = -1;

    }

    public static void InitGlobalSaveData()
    {
        GlobalData result = new GlobalData();

        if (Screen.currentResolution.width < 1600 || Screen.currentResolution.height < 900)
        {
            result.screenResolution = 0;
        }
        if (Screen.currentResolution.width < 1280 || Screen.currentResolution.height < 720)
        {
            result.isFullScreen = true;
        }
        if (isDemo)
        {
            result.isFullScreen = true;
        }

        //result.language = Language.en;
        switch (Application.systemLanguage)
        {
            case SystemLanguage.ChineseTraditional:
            case SystemLanguage.Chinese:
                result.language = Language.zh;
                break;
            case SystemLanguage.ChineseSimplified:
                result.language = Language.cn;
                break;
            case SystemLanguage.English:
            default:
                result.language = Language.en;
                break;
                //case SystemLanguage.Japanese:
                //    result.language = Language.jp;
                //    break;
                //case SystemLanguage.German:
                //    result.language = Language.de;
                //    break;
        }

        globalData = result;
        SaveGlobalSave();
        LoadGlobalSave();
    }

    static UserData InitUserData()
    {
        UserData result = new UserData();


        result.version = versionNo;

        //result.componentList.Add(0);//make it 1 base
        //result.character_id = 1;

        return result;
    }

    //static StageDataBase InitStageData(int difficulty)
    //{
    //    StageDataBase result = new StageDataBase();

    //    return result;
    //}



    public static List<int> SuffleList(int headInclude, int tailInclude)
    {
        List<int> result = new List<int>();
        for (int i = headInclude; i <= tailInclude; i++)
        {
            result.Add(i);
        }
        for (int t = 0; t < result.Count; t++)
        {
            int tmp = result[t];
            int r = Random.Range(t, result.Count);
            result[t] = result[r];
            result[r] = tmp;
        }
        return result;
    }

    public static void Save()
    {
        Save(saveId);
    }

    public static void Save(int _saveId)
    {
        saveId = _saveId;
        //print(UnityEngine.Application.persistentDataPath);
        //System.IO.File.WriteAllText(UnityEngine.Application.persistentDataPath + "/stageDataJson" + saveId + ".sav", JsonUtility.ToJson(stageDataJson));
        //System.IO.File.WriteAllText(UnityEngine.Application.persistentDataPath + "/userDataJson" + saveId + ".sav", JsonUtility.ToJson(userDataJson));



        //Write some text to the test.txt file
        //StreamWriter sw = new StreamWriter(UnityEngine.Application.persistentDataPath + "/stageDataJson" + saveId + ".sav", false);
        //sw.WriteLine(JsonConvert.SerializeObject(stageDataJson, Formatting.None,
        //            new JsonSerializerSettings()
        //            {
        //                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        //            }));
        //sw.Close();

        //Write some text to the test.txt file
        StreamWriter sw2 = new StreamWriter(UnityEngine.Application.persistentDataPath + "/userDataJson" + saveId + ".sav", false);
        sw2.WriteLine(JsonConvert.SerializeObject(userDataJson, Formatting.None,
                    new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    }));
        sw2.Close();
    }

    public static bool GetIsSaveCorrupted()
    {
        return (userDataJson.isSaveCorrupted);
    }

    //public static bool GetIsSaveCorrupted(int _saveId)
    //{
    //    string _userDataJson = System.IO.File.ReadAllText(UnityEngine.Application.persistentDataPath + "/userDataJson" + _saveId + ".sav");
    //    //userDataJson = JsonUtility.FromJson<UserDataBase>(_userDataJson);
    //    userDataJson = JsonConvert.DeserializeObject<UserData>(_userDataJson);

    //    return (userDataJson.isSaveCorrupted);
    //}

    public static bool GetIsSaveIncompatible(int _saveId)
    {
        string _userDataJson = System.IO.File.ReadAllText(UnityEngine.Application.persistentDataPath + "/userDataJson" + _saveId + ".sav");
        //userDataJson = JsonUtility.FromJson<UserDataBase>(_userDataJson);
        userDataJson = JsonConvert.DeserializeObject<UserData>(_userDataJson);

        return (userDataJson.version < 20200612);
    }

    //public static bool GetIsSaveClear(int _saveId)
    //{
    //    string _userDataJson = System.IO.File.ReadAllText(UnityEngine.Application.persistentDataPath + "/userDataJson" + _saveId + ".sav");
    //    //userDataJson = JsonUtility.FromJson<UserDataBase>(_userDataJson);
    //    userDataJson = JsonConvert.DeserializeObject<UserData>(_userDataJson);

    //    return userDataJson.isGameClear;
    //}

    public static void InitDataFromSave(int _saveId)
    {
        saveId = _saveId;
        //for (int i = 0; i < itemlist.Length; i++)
        //{
        //    stageDataJson["item"][i.ToString()] = itemlist[i];
        //}
        print(UnityEngine.Application.persistentDataPath);
        //string _stageDataJson = System.IO.File.ReadAllText(UnityEngine.Application.persistentDataPath + "/stageDataJson" + saveId + ".sav");
        string _userDataJson = System.IO.File.ReadAllText(UnityEngine.Application.persistentDataPath + "/userDataJson" + saveId + ".sav");
        //stageDataJson = JsonUtility.FromJson<StageData>(_stageDataJson);
        //stageDataJson = JsonConvert.DeserializeObject<StageDataBase>(_stageDataJson);
        //userDataJson = JsonUtility.FromJson<UserDataBase>(_userDataJson);
        userDataJson = JsonConvert.DeserializeObject<UserData>(_userDataJson);

        //print("InitDataFromSave " + _saveId);

        InitDatabaseData();


        //GameEventManager.RefreshCurrentEventList();
        //SkillManager.RefreshSkillList();
    }

    #endregion

    #region DataStructure




    static void LoadLocalizationData()
    {
        localizationTable = new Dictionary<string, LocalizedString>();

        //TextAsset localizationData = Resources.Load("database/Localization - localization") as TextAsset;
        string localizationData = ReadLocalization();
        if (localizationData.Length > 0)
        {
            string[] _a = localizationData.Split('\n');
            for (int i = 1; i < _a.Length; i++)
            {
                string[] _c = _a[i].Split('\t');

                //if (!localizationSubTable.ContainsKey(_c[1]) && _c.Length > (int)globalData.language)
                if (!localizationTable.ContainsKey(_c[1]))
                {
                    localizationTable.Add(_c[1], new LocalizedString(_c[2], _c[3], _c[4], ""));
                }
            }
        }

    }

    static public string ReadLocalization()
    {
        return ReadString("Localization - Terminology.tsv");
    }

    static public string ReadDatabaseWithoutLanguage(string fileName)
    {
        return ReadString("Database - " + fileName + ".tsv");
    }

    //static public string ReadDatabaseWithLanguage(string fileName)
    //{
    //    return ReadString("Database - " + globalData.language.ToString() + " - " + fileName + ".tsv");
    //}

    static string ReadString(string path)
    {
        string storagepath = UnityEngine.Application.dataPath + "/_Database/";

        //StreamReader reader = new StreamReader(path);
        //string result = reader.ReadToEnd();
        //reader.Close();
        string result = System.IO.File.ReadAllText(storagepath + path);
        return result;
    }
    #endregion

    #region Localization
    static Dictionary<string, LocalizedString> localizationTable = new Dictionary<string, LocalizedString>();
    //static Dictionary<string, string> localizationTable = new Dictionary<string, string>();
    public static string GetLocalizedText(string _key)
    {
        //Dictionary<string, string> localizationSubTable = null;
        //localizationTable.TryGetValue((int)Database.globalData.language, out localizationSubTable);
        LocalizedString globalString = null;
        string result = "";
        localizationTable.TryGetValue(_key, out globalString);
        if (globalString == null)
        {
            result = _key;
        }
        else
        {
            result = globalString.GetString();
        }
        if (result.Contains("「") && result.Contains("」"))
        {
            result = result.Replace("「", "<b> ");
            result = result.Replace("」", " </b>");
        }
        result = result.Replace("，", "， ");
        result = result.Replace("、", "、 ");
        result = result.Replace("。", "。 ");
        return result;
    }
    #endregion

    #region setting
    public static float speedInBattle = 1;
    public static void ApplySetting()
    {
        //bgm
        //AudioManager.instance.bgmVolume = globalData.bgm;
        //vfx
        //AudioManager.instance.sfxVolume = globalData.sfx;
        //globalData.language //TODO
        //screen resolution,full screen
        if (globalData.isFullScreen)
        {
            switch (globalData.screenResolution)
            {
                case 0:
                    Screen.SetResolution(1280, 720, globalData.isFullScreen);
                    break;
                case 1:
                    Screen.SetResolution(1600, 900, globalData.isFullScreen);
                    break;
                case 2:
                    Screen.SetResolution(1920, 1080, globalData.isFullScreen);
                    break;
            }
        }
        else
        {
            switch (globalData.screenResolution)
            {
                case 0:
                    Screen.SetResolution(Mathf.FloorToInt(1280 * 0.9f), Mathf.FloorToInt(720 * 0.9f), globalData.isFullScreen);
                    break;
                case 1:
                    Screen.SetResolution(Mathf.FloorToInt(1600 * 0.9f), Mathf.FloorToInt(900 * 0.9f), globalData.isFullScreen);
                    break;
                case 2:
                    Screen.SetResolution(Mathf.FloorToInt(1920 * 0.9f), Mathf.FloorToInt(1080 * 0.9f), globalData.isFullScreen);
                    break;
            }
        }
        //speed in battle
        //speedInBattle = 1f + (globalData.speedInBattle - 1) * 0.5f;
    }
    #endregion

    #region backdoor
    public static void DeleteAllSave()
    {
        for (int _saveId = Database.globalData.play_times + 1; _saveId >= 0; _saveId--)
        {
            if (System.IO.File.Exists(UnityEngine.Application.persistentDataPath + "/stageDataJson" + _saveId + ".sav"))
            {
                System.IO.File.Delete(UnityEngine.Application.persistentDataPath + "/stageDataJson" + _saveId + ".sav");
                System.IO.File.Delete(UnityEngine.Application.persistentDataPath + "/userDataJson" + _saveId + ".sav");
                //_saveId++;
            }
        }
    }
    #endregion

    #region runtimeLogic

    //public static void SaveCorrupted(int _saveId)
    //{
    //    InitDataFromSave(_saveId);
    //    userDataJson.isSaveCorrupted = true;
    //    Save(_saveId);
    //}


    //public static void GameOver(string _deadReason)
    //{
    //    ESLog("GameOver");
    //    //AudioManager.instance.PlayVoiceLosing();
    //    //AchievementManager.Instance.CompleteAchievement(2);//首次死亡
    //    //userDataJson.isSaveCorrupting = true;
    //    userDataJson.isSaveCorrupted = true;
    //    deadReason = _deadReason;
    //    //if (!globalData.is_tutorial_clear)
    //    //{
    //    //    deadReason = "loseTutorial";
    //    //}
    //    Save();
    //    globalData.lastLoadData = -1;
    //    SaveGlobalSave();
    //}

    public static void ContinueGame()
    {
        if (globalData.lastLoadData != -1)
        {
            LoadSave(globalData.lastLoadData);
        }
    }


    public static void StartNewGame()
    {
        //List<int> initialTotem = new List<int>();
        //initialTotem.Add(totemList[0].totemId);
        //initialTotem.Add(totemList[1].totemId);
        //initialTotem.Add(totemList[2].totemId);
        //initialTotem.Add(totemList[3].totemId);

        int _difficulty = 1;
        //if (chapter == 2)
        //{
        //    _difficulty = 4;
        //}
        //if (chapter == 3)
        //{
        //    _difficulty = 7;
        //}

        StartNewGame(_difficulty);

        //EnterLastStageMode();//test
    }

    public static void StartNewGame(int difficulty)
    {
        InitSave(globalData.play_times + 1, Random.Range(0, 100000), difficulty);
        //enterGameByLoadSave = false;

    }


    public static void ResetSave()
    {
        InitSave(0, 0, 1);//test///
        InitDataFromSave(0);//test///
    }


    public static void SetDescriptionMessage(string message1, string message2 = "", string message3 = "")
    {
        if (message1.Trim() == "")
        {
            descriptionMessageAppendY = 0;
            //return;
        }
        descriptionMessage = GetLocalizedText(message1).Replace("\\n", "\n").Replace("「", "<color=#ff5555ff>").Replace("」", "</color>");
        descriptionMessage2 = GetLocalizedText(message2).Replace("\\n", "\n").Replace("「", "<color=#ff5555ff>").Replace("」", "</color>");
        descriptionMessage3 = GetLocalizedText(message3).Replace("\\n", "\n").Replace("「", "<color=#ff5555ff>").Replace("」", "</color>");
    }

    //public static void SetSystemMessage(string message)
    //{
    //    systemMessage = message.Replace("\\n", "\n");
    //    systemMessage = Database.GetLocalizedText(systemMessage);
    //}

    //public bool isDescriptionMessageFollowMouse = true;

    public static bool IsDescriptionMessage()
    {
        return descriptionMessage.Length > 0;
    }
    //public static bool IsSystemMessage()
    //{
    //    return systemMessage.Length > 0;
    //}

    public static void SaveGlobalSave()
    {

        //System.IO.File.WriteAllText(UnityEngine.Application.persistentDataPath + "/globalSaveDataJson.sav", JsonUtility.ToJson(globalData));

        StreamWriter sw = new StreamWriter(UnityEngine.Application.persistentDataPath + "/globalSaveDataJson.sav", false);
        sw.WriteLine(JsonConvert.SerializeObject(globalData, Formatting.None,
                    new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    }));
        sw.Close();
    }

    public static int SearchForUnreadAchievement()
    {
        foreach (int _id in globalData.completed_achievement_id)
        {
            if (!globalData.read_achievement_id.Contains(_id))
            {
                return _id;
            }
        }
        return 0;
    }

    public static void ReadAchievement(int _id)
    {
        if (!globalData.read_achievement_id.Contains(_id))
        {
            globalData.read_achievement_id.Add(_id);
            SaveGlobalSave();
        }
    }


    public static bool hasGameoverSave()
    {
        //bool result = false;
        foreach (SaveListItem _s in GetSaveList())
        {
            //if (_s.is_corrupting || _s.is_corrupted)
            if (_s.is_corrupted)
            {
                return true;
            }
        }
        return false;
    }

    public static string TimeToRomanString(int time)
    {
        string result = "";
        switch (time)
        {
            case 0:
            case 12:
                result = "XII";
                break;
            case 1:
                result = "I";
                break;
            case 2:
                result = "II";
                break;
            case 3:
                result = "III";
                break;
            case 4:
                result = "IV";
                break;
            case 5:
                result = "V";
                break;
            case 6:
                result = "VI";
                break;
            case 7:
                result = "VII";
                break;
            case 8:
                result = "VIII";
                break;
            case 9:
                result = "IX";
                break;
            case 10:
                result = "X";
                break;
            case 11:
                result = "XI";
                break;
        }
        return result;
    }


    public static string TimeToString(float _time)
    {
        int sec = Mathf.FloorToInt(_time) % 60;
        int min = Mathf.FloorToInt(_time / 60f);
        return min.ToString("00") + ":" + sec.ToString("00");
    }

    public static string GetInputIcon(KeyBoardInput input)
    {
        return "<sprite=" + (int)input + ">";
    }


    #endregion

}
