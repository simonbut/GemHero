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
        listMenuView.SetUp(new List<string> { "Composite", "PlayerTag", "Item", "Quit"}, this, new List<Callback> { OnCompositeButtonClick, OnPlayerTagButtonClick, OnItemButtonClick, OnQuitButtonClick });

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
}

