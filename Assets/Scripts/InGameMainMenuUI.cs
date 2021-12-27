using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using ClassHelper;

public class InGameMainMenuUI : ControlableUI
{
    public ListMenuView listMenuView;

    public override void OnShow()
    {
        listMenuView.SetUp(new List<string> { "Composite", "PlayerTag", "Item", "Sleep", "Quit" }, this, new List<Callback> { OnCompositeButtonClick, OnPlayerTagButtonClick, OnItemButtonClick, OnSleepButtonClick, OnQuitButtonClick });

        base.OnShow();
    }

    // Update is called once per frame
    void Update()
    {
        if (!UIManager.Instance.IsCurrentUI(this))
        {
            return;
        }

        if (ControlView.Instance.controls.Map1.Cancel.triggered || ControlView.Instance.controls.Map1.AssetKey.triggered)
        {
            OnBackPressed();
        }
    }

    void OnCompositeButtonClick()
    {
        MainGameView.Instance.OpenRecipeMenu();
    }

    void OnQuitButtonClick()
    {
        SceneManager.LoadScene("StartScene");
    }

    void OnPlayerTagButtonClick()
    {
        MainGameView.Instance.playerTagChoosingCanvas.AddUI();
    }

    void OnItemButtonClick()
    {
        MainGameView.Instance.itemCanvas.AddUI();
    }

    void OnSleepButtonClick()
    {
        UIManager.Instance.choiceUI.Setup(new Vector2(Screen.width / 2f, Screen.height / 2f), new List<string> { "Confirm", "Cancel" }, new List<Callback> { MainGameView.Instance.Sleep, null },"Do you want to sleep? It will consume 2 hrs.");
    }
}

