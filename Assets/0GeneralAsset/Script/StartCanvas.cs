using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClassHelper;
using UnityEngine.UI;

public class StartCanvas : ControlableUI
{

    //public List<Sprite> highlightedSprite;
    //public List<Sprite> normalSprite;

    //public List<Button> buttonList;
    //public Button tutorialButton;

    //public GameObject continueButton;

    public ListMenuView listMenuView;

    public override void OnShow()
    {
        listMenuView.SetUp(new List<string> { "Contine", "New Game", "Settings", "Exit" }, this, new List<Callback> { OnContinueButtonClick, OnStartButtonClick, OnSettingButtonClick, OnExitButtonClick });

        base.OnShow();
    }

    // Update is called once per frame
    void Update()
    {
        if (!UIManager.Instance.IsCurrentUI(this))
        {
            return;
        }
    }

    public void OnStartButtonClick()//TODO cancel it
    {
        StartView.Instance.NewGameClicked();
    }

    public void OnContinueButtonClick()
    {
        StartView.Instance.ContinueClicked();
    }

    public void OnSettingButtonClick()
    {
        StartView.Instance.OpenSettingUI();
    }

    public void OnExitButtonClick()
    {
        StartView.Instance.ExitGame();
    }
}
