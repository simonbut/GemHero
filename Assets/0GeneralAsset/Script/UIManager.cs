using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region instance
    private static UIManager m_instance;

    public static UIManager Instance
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

    //ControlableUI controlingUI;
    public List<ControlableUI> ControlableUIList = new List<ControlableUI>();
    public ConfirmUI confirmUI;
    public ConfirmUI confirmSmallUI;
    public InformationUI informationUI;
    public InformationUI informationParagraphUI;
    public SettingUI settingUI;
    public ControlableUIGeneral emptyUI;
    public NewAchievementUI newAchievementUI;


    [SerializeField] GameObject staticCanvas;
    [SerializeField] DropdownUI dropdownUIPrefab;

    public AssetDataUI assetDataUI;
    public RecipeDataUI recipeDataUI;
    public CompositionDataUI compositionDataUI;
    public AssetInCompositionDataUI assetInCompositionDataUI;
    public CompositionPage2DataUI compositionPage2DataUI;
    public AchievementDataUI achievementDataUI;

    public ChoiceUI choiceUI;
    public void HideAllDataUI()
    {
        assetDataUI.Hide();
        recipeDataUI.Hide();
        compositionDataUI.Hide();
        assetInCompositionDataUI.Hide();
        compositionPage2DataUI.Hide();
        achievementDataUI.Hide();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //print("Last Current UI " + ControlableUIList[ControlableUIList.Count - 1].gameObject.name);
    }

    public void AddEmptyUI()
    {
        emptyUI.AddUI();
    }

    public void HideUI(ControlableUI UIToShow)
    {
        UIToShow.gameObject.SetActive(false);
    }

    public void HideLastUI()
    {
        if (ControlableUIList.Count > 1 && ControlableUIList[ControlableUIList.Count - 2] != null)
        {
            ControlableUIList[ControlableUIList.Count - 2].gameObject.SetActive(false);
        }
    }
    
    public void ShowUI(ControlableUI UIToShow,Canvas _canvas = null)
    {
        UIToShow.gameObject.SetActive(true);
    }

    public void ShowLastUI()
    {
        if (ControlableUIList.Count > 0 && ControlableUIList[ControlableUIList.Count - 1] != null)
        {
            ControlableUIList[ControlableUIList.Count - 1].gameObject.SetActive(true);
            ControlableUIList[ControlableUIList.Count - 1].OnResume();
        }
    }

    public void OnEnterNewUI()
    {
        if (ControlableUIList.Count > 0 && ControlableUIList[ControlableUIList.Count - 1] != null)
        {
            ControlableUIList[ControlableUIList.Count - 1].OnEnterNewUI();
        }
    }

    public void InsetUI(ControlableUI UIToAdd)
    {
        //Debug.Log("AddUI");
        ControlableUIList.Insert(ControlableUIList.Count - 2, UIToAdd);
        Database.SetDescriptionMessage("");
    }

    public void AddUI(ControlableUI UIToAdd)
    {
        //Debug.Log("AddUI");
        ControlableUIList.Add(UIToAdd);
        Database.SetDescriptionMessage("");
    }

    public void RemoveUI(ControlableUI UIToRemove)
    {
        //Debug.Log("RemoveUI");
        ControlableUIList.Remove(UIToRemove);
    }

    public bool IsNoUI()
    {
        return (ControlableUIList.Count <= 0);
    }

    public bool IsCurrentUI(ControlableUI UIToCheck)
    {
        if (IsNoUI())
        {
            return false;
        }
        if (UIToCheck == ControlableUIList[ControlableUIList.Count-1])
        {
            return true;
        }
        return false;
    }

    public bool IsCurrentUIConfirmUI()
    {
        bool result = false;
        if (ControlableUIList.Count <= 0)
        {
            return false;
        }
        if (confirmUI == ControlableUIList[ControlableUIList.Count - 1] || confirmSmallUI == ControlableUIList[ControlableUIList.Count - 1])
        {
            return true;
        }
        return result;
    }

    public bool IsCurrentUIInformationUI()
    {
        if (ControlableUIList.Count <= 0)
        {
            return false;
        }
        if (informationUI == ControlableUIList[ControlableUIList.Count - 1])
        {
            return true;
        }
        return false;
    }
    
    public ControlableUI GetCurrentUI()
    {
        if (ControlableUIList.Count <= 0)
        {
            return null;
        }
        return ControlableUIList[ControlableUIList.Count - 1];
    }

    public void CleanAllUI()
    {
        ControlableUIList = new List<ControlableUI>();
    }

    public void GoBack()//force back
    {
        ControlableUIList[ControlableUIList.Count - 1].OnRemoveUI();
        ShowLastUI();
        Database.SetDescriptionMessage("");
    }

    //public void AddInputTextUI(string _text, InputTextUI.InputTextCallBack _yesCallBack, InputTextUI.SimpleCallBack _noCallBack = null, bool _isHideLastUI = true)
    //{
    //    inputTextUI.isHideLastUI = _isHideLastUI;
    //    inputTextUI.AddUI(_text, _yesCallBack, _noCallBack);
    //    inputTextUI.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
    //}

    public void AddConfirmUI(string _text, ConfirmUI.SimpleCallBack _yesCallBack, ConfirmUI.SimpleCallBack _noCallBack = null,bool _isHideLastUI = true)
    {
        confirmUI.isHideLastUI = _isHideLastUI;
        confirmUI.AddUI(_text, _yesCallBack, _noCallBack);
        confirmUI.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
    }

    public void AddConfirmSmallUI(string _text, ConfirmUI.SimpleCallBack _yesCallBack, ConfirmUI.SimpleCallBack _noCallBack = null)
    {
        confirmSmallUI.AddUI(_text, _yesCallBack, _noCallBack);
        confirmSmallUI.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
    }

    public void AddConfirmUILow(string _text, ConfirmUI.SimpleCallBack _yesCallBack, ConfirmUI.SimpleCallBack _noCallBack = null, bool _isHideLastUI = true)
    {
        confirmUI.isHideLastUI = _isHideLastUI;
        confirmUI.AddUI(_text, _yesCallBack, _noCallBack);
        confirmUI.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -20);
    }

    public void AddInformationUI(string _text, InformationUI.SimpleCallBack _yesCallBack = null)
    {
        informationUI.AddUI(_text, _yesCallBack);
    }

    public void AddInformationParagraphUI(string _text, InformationUI.SimpleCallBack _yesCallBack = null)
    {
        informationParagraphUI.AddUI(_text, _yesCallBack);
    }

    public void RefreshUI()
    {
        ShowLastUI();
        StopAnimation();
        Database.SetDescriptionMessage("");
    }


    public void AddAchievementUI()
    {
        newAchievementUI.AddUI();
    }

    public void AddSettingUI()
    {
        settingUI.AddUI();
    }


    //public LoadingUI loadingUI;
    //public void ShowLoadingUI(string _sceneName)
    //{
    //    loadingUI.AddUI(_sceneName);
    //}

    //public void HideLoadingUI()
    //{
    //    loadingUI.OnBackPressed();
    //}

    public void StopAnimation()
    {
        ControlableUIList[ControlableUIList.Count - 1].StopAnimation();
    }

    public DropdownUI GenerateDropdownUI(DropdownUI _dropdownUI, string _title,List<string> optionList, Button _triggerButton ,Vector2 _pos,int initialValue = 0)
    {
        if (_dropdownUI != null && _dropdownUI.gameObject != null)
        {
            Destroy(_dropdownUI.gameObject);
        }

        GameObject dropdownUI = Instantiate(dropdownUIPrefab.gameObject);
        dropdownUI.transform.SetParent(staticCanvas.transform);
        dropdownUI.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        dropdownUI.GetComponent<RectTransform>().sizeDelta = Vector2.zero;
        dropdownUI.transform.localPosition = Vector3.zero;
        dropdownUI.transform.localScale = Vector3.one * 0.05f;
        dropdownUI.GetComponent<DropdownUI>().listPositionIndicator.transform.position = _pos;
        dropdownUI.GetComponent<DropdownUI>().triggerButton = _triggerButton;
        dropdownUI.GetComponent<DropdownUI>().optionList = optionList;
        dropdownUI.GetComponent<DropdownUI>().title = _title;
        dropdownUI.GetComponent<DropdownUI>().value = initialValue;
        _triggerButton.GetComponentInChildren<Text>().text = optionList[initialValue];
        dropdownUI.SetActive(false);

        return dropdownUI.GetComponent<DropdownUI>();
    }

    //public void ShowItemChoosingIndicator(Vector3 _v)
    //{
    //    itemChoosingIndicator.Show(_v);
    //}

    public void OnBackPressed()
    {
        GetCurrentUI().OnBackPressed();
    }

}

