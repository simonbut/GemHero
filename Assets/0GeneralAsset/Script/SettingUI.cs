using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using ClassHelper;

public class SettingUI : ControlableUI
{
    [SerializeField] Slider bgmSlider;
    [SerializeField] Slider vfxSlider;
    [SerializeField] Button languageDropDownButton;
    [SerializeField] Toggle fullScreenToggle;
    //[SerializeField] Toggle isSimpleModeToggle;
    [SerializeField] Button resolutionDropDownButton;
    DropdownUI languageDropDown;
    DropdownUI resolutionDropDown;
    //[SerializeField] Slider speedInBattleSlider;

    //[SerializeField] Button creditButton;
    [SerializeField] Button titleButton;

    [SerializeField] Text versionNo;

    [SerializeField] Slider gameSpeedSlider;

    public GameObject selectingSlot;
    public List<GameObject> titleList;
    int titleCount = 5;

    bool loadComplete = true;

    public Image toTitleButtonImage;

    private void Awake()
    {

        Init();
    }

    private void Start()
    {
        languageDropDownButton.onClick.AddListener(LanguageDropDownClicked);
        resolutionDropDownButton.onClick.AddListener(ResolutionDropDownClicked);
    }

    public override void OnShow()
    {
        GlobalCommunicateManager.selectingId = 0;

        //if (JoyStickManager.Instance.IsJoyStickEnable())
        //{
        //    toTitleButtonImage.enabled = false;
        //    //ControlMeaningUI.Instance?.Show(Database.GetInputIcon(KeyBoardInput.joystick_cross) + " " + Database.GetLocalizedText("Confirm") + "     " + Database.GetInputIcon(KeyBoardInput.joystick_circle) + " " + Database.GetLocalizedText("Back"));
        //}
        //else
        //{
        //    toTitleButtonImage.enabled = true;
        //    //ControlMeaningUI.Instance?.Show(Database.GetInputIcon(KeyBoardInput.Input_Left_Click) + " " + Database.GetLocalizedText("Confirm") + "     " + Database.GetInputIcon(KeyBoardInput.Input_Right_Click) + " " + Database.GetLocalizedText("Back"));
        //}

        base.OnShow();
    }

    public void Init()
    {
        versionNo.text = "ver. " + Database.versionNo;
        LoadSetting();
        //if (MainMenuView.Instance == null)
        //{
        //    titleCount = 7;
        //    titleButton.gameObject.SetActive(true);
        //}
        //else
        //{
        //    titleCount = 6;
        //    titleButton.gameObject.SetActive(false);
        //}
    }

    public void LoadSetting()
    {
        loadComplete = false;

        bgmSlider.value = Database.globalData.bgm;
        vfxSlider.value = Database.globalData.sfx;
        gameSpeedSlider.value = Database.globalData.gameSpeed;
        languageDropDown = UIManager.Instance.GenerateDropdownUI(languageDropDown, Database.GetLocalizedText("Language"), new List<string> { "English", "繁體中文", "简体中文" }, languageDropDownButton, languageDropDownButton.transform.position, (int)Database.globalData.language);
        resolutionDropDown = UIManager.Instance.GenerateDropdownUI(resolutionDropDown, Database.GetLocalizedText("Resolution"), new List<string> { "1280 x 720", "1600 x 900", "1920 x 1080" }, resolutionDropDownButton, resolutionDropDownButton.transform.position, Database.globalData.screenResolution);

        fullScreenToggle.isOn = Database.globalData.isFullScreen;
        //isSimpleModeToggle.isOn = Database.globalData.isSimpleMode;

        loadComplete = true;
    }

    void SaveSetting()
    {
        if (Database.globalData.language != (Language)languageDropDown.value)
        {
            Database.globalData.language = (Language)languageDropDown.value;
            LocalizationText[] _ltList = FindObjectsOfType<LocalizationText>();
            foreach (LocalizationText _lt in _ltList)
            {
                _lt.LocalizeText();
            }
        }

        Database.globalData.bgm = Mathf.FloorToInt(bgmSlider.value);
        Database.globalData.sfx = Mathf.FloorToInt(vfxSlider.value);
        Database.globalData.gameSpeed = Mathf.FloorToInt(gameSpeedSlider.value);
        Database.globalData.screenResolution = resolutionDropDown.value;
        Database.globalData.isFullScreen = fullScreenToggle.isOn;
        //Database.globalData.isSimpleMode = isSimpleModeToggle.isOn;
        Database.SaveGlobalSave();
    }

    public override void OnRemoveUI()
    {
        SaveSetting();
        Database.ApplySetting();
        //if (startScene!=null)
        //{
        //    startScene.OnBackPressed();
        //}
        //if (coreFlow != null)
        //{
        //    coreFlow.OnBackPressed();
        //    Time.timeScale = Database.globalData.speedInBattle;
        //}
        //if (mainbattle != null)
        //{
        //    mainbattle.OnBackPressed();
        //    Time.timeScale = Database.globalData.speedInBattle;
        //}
        base.OnRemoveUI();
        UIManager.Instance.StopAnimation();
    }

    public void OnSettingChange()
    {
        if (loadComplete)
        {
            SaveSetting();
            Database.ApplySetting();
        }
    }

    //public void TextSFX()
    //{
    //    if (loadComplete)
    //    {
    //        SaveSetting();
    //        Database.ApplySetting();
    //        StartCoroutine(PlaySFX());
    //    }
    //}

    public void OnTitleButtonPressed()
    {
        OnRemoveUI();
        //SaveSetting();
        //Database.ApplySetting();
        SceneManager.LoadScene("StartScene");
    }

    //bool hint = false;
    //float timer = 0;
    private void Update()
    {
        if (!UIManager.Instance.IsCurrentUI(this))
        {
            return;
        }

        //if (JoyStickManager.Instance.IsInputDown("Cross") || Input.GetButtonDown("Escape"))
        //{
        //    OnBackPressed();
        //}


        selectingSlot.gameObject.SetActive(false);
        //if (JoyStickManager.Instance.IsJoyStickEnable())
        //{
        //    selectingSlot.gameObject.SetActive(true);
        //    selectingSlot.transform.position = titleList[GlobalCommunicateManager.selectingId].transform.position;


        //    if (JoyStickManager.Instance.IsInputDown("Down"))
        //    {
        //        if (GlobalCommunicateManager.selectingId + 1 < titleCount)
        //        {
        //            GlobalCommunicateManager.selectingId++;
        //        }
        //        else
        //        {
        //            GlobalCommunicateManager.selectingId = 0;
        //        }
        //    }
        //    if (JoyStickManager.Instance.IsInputDown("Up"))
        //    {
        //        if (GlobalCommunicateManager.selectingId - 1 >= 0)
        //        {
        //            GlobalCommunicateManager.selectingId--;
        //        }
        //        else
        //        {
        //            GlobalCommunicateManager.selectingId = titleCount - 1;
        //        }
        //    }

        //    switch (GlobalCommunicateManager.selectingId)
        //    {
        //        case 0://bgm
        //            if (JoyStickManager.Instance.IsInputDown("Left"))
        //            {
        //                if (bgmSlider.value > 0)
        //                {
        //                    bgmSlider.value--;
        //                    OnSettingChange();
        //                }
        //            }
        //            if (JoyStickManager.Instance.IsInputDown("Right"))
        //            {
        //                if (bgmSlider.value < bgmSlider.maxValue)
        //                {
        //                    bgmSlider.value++;
        //                    OnSettingChange();
        //                }
        //            }
        //            break;
        //        case 1://sfx
        //            if (JoyStickManager.Instance.IsInputDown("Left"))
        //            {
        //                if (vfxSlider.value > 0)
        //                {
        //                    vfxSlider.value--;
        //                    OnSettingChange();
        //                }
        //            }
        //            if (JoyStickManager.Instance.IsInputDown("Right"))
        //            {
        //                if (vfxSlider.value < vfxSlider.maxValue)
        //                {
        //                    vfxSlider.value++;
        //                    OnSettingChange();
        //                }
        //            }
        //            break;
        //        case 2://language
        //            if (JoyStickManager.Instance.IsInputDown("Circle"))
        //            {
        //                LanguageDropDownClicked();
        //            }
        //            //if (JoyStickManager.Instance.IsInputDown("Left"))
        //            //{
        //            //    if (languageDropDown.value > 0)
        //            //    {
        //            //        languageDropDown.value--;
        //            //        OnSettingChange();
        //            //    }
        //            //}
        //            //if (JoyStickManager.Instance.IsInputDown("Right"))
        //            //{
        //            //    if (languageDropDown.value < languageDropDown.options.Count - 1)
        //            //    {
        //            //        languageDropDown.value++;
        //            //        OnSettingChange();
        //            //    }
        //            //}
        //            break;
        //        case 3://full screen
        //            if (JoyStickManager.Instance.IsInputDown("Circle"))
        //            {
        //                fullScreenToggle.isOn = !fullScreenToggle.isOn;
        //                OnSettingChange();
        //            }
        //            break;
        //        case 4://resolution
        //            if (JoyStickManager.Instance.IsInputDown("Circle"))
        //            {
        //                ResolutionDropDownClicked();
        //            }
        //            //if (JoyStickManager.Instance.IsInputDown("Left"))
        //            //{
        //            //    if (resolutionDropDown.value > 0)
        //            //    {
        //            //        resolutionDropDown.value--;
        //            //        OnSettingChange();
        //            //    }
        //            //}
        //            //if (JoyStickManager.Instance.IsInputDown("Right"))
        //            //{
        //            //    if (resolutionDropDown.value < resolutionDropDown.options.Count - 1)
        //            //    {
        //            //        resolutionDropDown.value++;
        //            //        OnSettingChange();
        //            //    }
        //            //}
        //            break;
        //        case 5://simple mode
        //            if (JoyStickManager.Instance.IsInputDown("Circle"))
        //            {
        //                isSimpleModeToggle.isOn = !isSimpleModeToggle.isOn;
        //                OnSettingChange();
        //            }
        //            break;
        //        case 6://to main screen
        //            if (JoyStickManager.Instance.IsInputDown("Circle"))
        //            {
        //                OnTitleButtonPressed();
        //            }
        //            break;
        //    }
        //}

        //    if (hint)
        //    {
        //        while (timer < 1f)
        //        {
        //            hintGameObject.transform.localPosition = new Vector3(0, Mathf.Max(hint.transform.localPosition.y, -100f * (1f - timer)), 0);
        //            timer += Time.deltaTime;
        //            yield return null;
        //        }
        //        yield return new WaitForSeconds(1f);

        //        float timer2 = 0;
        //        while (timer2 < 1f)
        //        {
        //            hint.transform.localPosition = new Vector3(0, Mathf.Min(hint.transform.localPosition.y, -100f * timer2), 0);
        //            timer2 += Time.deltaTime;
        //            yield return null;
        //        }
        //        hint = false;
        //    }

    }

    public void LanguageDropDownClicked()
    {
        languageDropDown.AddUI();
    }

    public void ResolutionDropDownClicked()
    {
        resolutionDropDown.AddUI();
    }
}
