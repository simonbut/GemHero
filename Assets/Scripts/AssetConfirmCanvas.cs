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

    List<Asset> assetList = new List<Asset>();
    public void AddUI(List<Asset> _al)
    {
        assetList.AddRange(_al);
        NextAsset();

        base.AddUI();
    }

    public void NextAsset()
    {
        if (assetList.Count > 0)
        {
            UIManager.Instance.assetDataUI.HideInstantly();
            UIManager.Instance.assetDataUI.Show(assetList[0]);
            assetList.RemoveAt(0);
        }
        else
        {
            UIManager.Instance.assetDataUI.Hide();
            OnBackPressed();
        }
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
            NextAsset();
        }
    }
}
