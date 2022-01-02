using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClassHelper;

public class InGameMainMenuUI : ControlableUI
{
    public ListMenuView listMenuView;

    public override void OnShow()
    {
        listMenuView.SetUp(new List<string> { "Composite", "PlayerTag", "Equipment", "Item", "Sleep", "Quit" }, this, new List<Callback> { OnCompositeButtonClick, OnPlayerTagButtonClick, OnEquipmentButtonClick, OnItemButtonClick, OnSleepButtonClick, OnQuitButtonClick });

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
        MainGameView.Instance.OpenCompositeCanvas();
    }

    void OnEquipmentButtonClick()
    {
        MainGameView.Instance.OpenEquipmentCanvas();
    }

    void OnPlayerTagButtonClick()
    {
        MainGameView.Instance.OpenPlayerTagCanvas();
    }

    void OnItemButtonClick()
    {
        MainGameView.Instance.OpenItemCanvas();
    }

    void OnSleepButtonClick()
    {
        MainGameView.Instance.OpenSleepCanvas();
    }

    void OnQuitButtonClick()
    {
        MainGameView.Instance.Quit();
    }
}

