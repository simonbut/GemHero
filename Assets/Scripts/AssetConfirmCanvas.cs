using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClassHelper;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class AssetConfirmCanvas : ControlableUI
{

    public override void OnRemoveUI()
    {
        UIManager.Instance.HideAllDataUI();

        base.OnRemoveUI();
    }


    public override void OnShow()
    {

        base.OnShow();
    }

    public void AddUI(Asset _a)
    {
        UIManager.Instance.assetDataUI.Show(_a);

        base.AddUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (!UIManager.Instance.IsCurrentUI(this))
        {
            return;
        }

        if (ControlView.Instance.controls.Map1.React.triggered || ControlView.Instance.controls.Map1.Cancel.triggered)
        {
            UIManager.Instance.assetDataUI.Hide();
            OnBackPressed();
        }
    }
}
