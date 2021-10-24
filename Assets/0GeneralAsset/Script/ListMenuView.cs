using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ListMenuView : MonoBehaviour
{
    public bool isYAffected = true;

    int buttonCount = 0;
    public Sprite highlightedSprite;
    public Sprite normalSprite;

    public List<Button> buttonList;
    //public Button button2;
    //public Button button3;
    //public Button button4;

    public delegate void Callback();
    List<Callback> onButtonClickedCallbackList = new List<Callback>();
    //public Callback onButton2ClickedCallback;
    //public Callback onButton3ClickedCallback;
    //public Callback onButton4ClickedCallback;

    // Start is called before the first frame update

    ControlableUI controlableUI;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (JoyStickManager.Instance.IsJoyStickEnable())
        {
            for (int i = 0; i < buttonList.Count; i++)
            {
                buttonList[i].GetComponent<Image>().color = Color.white;
                if (GlobalCommunicateManager.selectingId == i)
                {
                    buttonList[i].GetComponent<Image>().sprite = highlightedSprite;
                }
                else
                {
                    buttonList[i].GetComponent<Image>().sprite = normalSprite;
                }
            }
        }

        for (int i = 0; i < buttonList.Count; i++)
        {
            if (!buttonList[i].interactable)
            {
                buttonList[i].GetComponent<Image>().color = Color.gray;
            }
            else
            {
                buttonList[i].GetComponent<Image>().color = Color.white;
            }


            if (isYAffected)
            {

                Vector2 tempVector = buttonList[i].GetComponent<RectTransform>().anchoredPosition;
                if (GlobalCommunicateManager.selectingId == i)
                {
                    tempVector.x = 150f;
                }
                else
                {
                    tempVector.x = 70f;
                }
                buttonList[i].GetComponent<RectTransform>().anchoredPosition = (tempVector + buttonList[i].GetComponent<RectTransform>().anchoredPosition) / 2f;
            }
        }

        if (JoyStickManager.Instance.IsJoyStickEnable() && UIManager.Instance.IsCurrentUI(controlableUI))
        {
            if (JoyStickManager.Instance.IsInputDown("Circle"))
            {
                if (buttonList.Count > GlobalCommunicateManager.selectingId)
                {
                    OnButtonClicked(GlobalCommunicateManager.selectingId);
                    //buttonList[GlobalCommunicateManager.selectingId].GetComponent<Button>().onClick.Invoke();
                }
            }

            if (JoyStickManager.Instance.IsInputDown("Down"))
            {
                if (GlobalCommunicateManager.selectingId + 1 < buttonCount)
                {
                    GlobalCommunicateManager.selectingId++;
                }
                else
                {
                    GlobalCommunicateManager.selectingId = 0;
                }
            }
            if (JoyStickManager.Instance.IsInputDown("Up"))
            {
                if (GlobalCommunicateManager.selectingId - 1 >= 0)
                {
                    GlobalCommunicateManager.selectingId--;
                }
                else
                {
                    GlobalCommunicateManager.selectingId = buttonCount - 1;
                }
            }
        }
    }

    public void SetUp(List<string> _titleList, ControlableUI _controlableUI, List<Callback> _callbackList)
    {
        controlableUI = _controlableUI;

        foreach (Button _b in buttonList)
        {
            _b.gameObject.SetActive(false);
        }
        onButtonClickedCallbackList = _callbackList;
        for (int i = 0; i < _titleList.Count; i++)
        {
            buttonList[i].gameObject.SetActive(true);
            buttonList[i].gameObject.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = Database.GetLocalizedText(_titleList[i]);
        }

        buttonCount = _titleList.Count;
        GlobalCommunicateManager.selectingId = 0;
        GlobalCommunicateManager.selectingScrollViewId = 0;
        if (JoyStickManager.Instance.IsJoyStickEnable())
        {
            OnButtonSelecing(0);
        }
    }

    public void OnButtonClicked(int index)
    {
        onButtonClickedCallbackList[index]?.Invoke();
    }

    public void OnButtonSelecing(int index)
    {
        GlobalCommunicateManager.selectingId = index;
    }
}
