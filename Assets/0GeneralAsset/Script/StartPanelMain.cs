using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClassHelper;
using UnityEngine.UI;
using TMPro;

public class StartPanelMain : ControlableUI
{

    public List<Sprite> highlightedSprite;
    public List<Sprite> normalSprite;

    public List<Button> buttonList;
    public Button tutorialButton;

    public GameObject continueButton;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (UIManager.Instance.IsCurrentUI(this))
        {
            for (int i = 0; i < buttonList.Count; i++)
            {
                if (GlobalCommunicateManager.selectingId == i)
                {
                    buttonList[i].GetComponent<Image>().sprite = highlightedSprite[i];
                }
                else
                {
                    buttonList[i].GetComponent<Image>().sprite = normalSprite[i];
                }
            }

            if (JoyStickManager.Instance.IsJoyStickEnable())
            {
                tutorialButton.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = Database.GetInputIcon(KeyBoardInput.joystick_square) + " " + Database.GetLocalizedText("Tutorial");
                if (JoyStickManager.Instance.IsInputDown("Square"))
                {
                    StartView.Instance.TutorialClicked();
                }

                if (JoyStickManager.Instance.IsInputDown("Triangle"))
                {
                    UIManager.Instance.AddSettingUI();
                }

                if (JoyStickManager.Instance.IsInputDown("Escape"))
                {
                    UIManager.Instance.AddSettingUI();
                }

                if (JoyStickManager.Instance.IsInputDown("Circle"))
                {
                    if (buttonList.Count > GlobalCommunicateManager.selectingId)
                    {
                        //OnButtonClicked(GlobalCommunicateManager.selectingId);
                        buttonList[GlobalCommunicateManager.selectingId].GetComponent<Button>().onClick.Invoke();
                    }
                }

                if (JoyStickManager.Instance.IsInputDown("Down"))
                {
                    if (GlobalCommunicateManager.selectingId + 1 < buttonList.Count)
                    {
                        GlobalCommunicateManager.selectingId++;
                    }
                    else
                    {
                        GlobalCommunicateManager.selectingId = 0;
                    }
                    while (!buttonList[GlobalCommunicateManager.selectingId].gameObject.activeInHierarchy)
                    {
                        if (GlobalCommunicateManager.selectingId + 1 < buttonList.Count)
                        {
                            GlobalCommunicateManager.selectingId++;
                        }
                        else
                        {
                            GlobalCommunicateManager.selectingId = 0;
                        }
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
                        GlobalCommunicateManager.selectingId = buttonList.Count - 1;
                    }
                    while (!buttonList[GlobalCommunicateManager.selectingId].gameObject.activeInHierarchy)
                    {
                        if (GlobalCommunicateManager.selectingId - 1 >= 0)
                        {
                            GlobalCommunicateManager.selectingId--;
                        }
                        else
                        {
                            GlobalCommunicateManager.selectingId = buttonList.Count - 1;
                        }
                    }
                }
            }
        }
    }

    public override void OnShow()
    {

        GlobalCommunicateManager.selectingId = 1;
        if (JoyStickManager.Instance.IsJoyStickEnable())
        {
            ControlMeaningUI.Instance?.Show(Database.GetInputIcon(KeyBoardInput.joystick_triangle) + " " + Database.GetLocalizedText("Setting") + "     " + Database.GetInputIcon(KeyBoardInput.joystick_cross) + " " + Database.GetLocalizedText("Confirm"));
        }
        else
        {
            ControlMeaningUI.Instance?.Show(Database.GetInputIcon(KeyBoardInput.Input_Left_Click) + " " + Database.GetLocalizedText("Confirm"));
        }




        if (Database.globalData.lastLoadData != -1)
        {
            //if (!Database.GetSaveList()[Database.globalData.lastLoadData - 1].is_clear && !Database.GetSaveList()[Database.globalData.lastLoadData - 1].is_corrupted && !Database.GetSaveList()[Database.globalData.lastLoadData - 1].is_corrupting)
            if (!Database.GetSaveList()[Database.globalData.lastLoadData - 1].is_clear && !Database.GetSaveList()[Database.globalData.lastLoadData - 1].is_corrupted && !Database.GetIsSaveIncompatible(Database.GetSaveList()[Database.globalData.lastLoadData - 1].save_id))//list is zero base
            {
                continueButton.SetActive(true);
            }
            else
            {
                continueButton.SetActive(false);
            }
        }
        else
        {
            continueButton.SetActive(false);
        }



        base.OnShow();
    }

    public void OnButtonSelecing(int index)
    {
        GlobalCommunicateManager.selectingId = index;
    }
}
