using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClassHelper;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class ItemCanvas : ControlableUI
{
    public GridScrollView gridScrollView;
    public GridItemCallback clickCallback;
    public int filterQuality = 0;
    public int filterTagId = 0;
    public int filterAssetId = 0;

    public override void OnRemoveUI()
    {
        UIManager.Instance.HideAllDataUI();

        base.OnRemoveUI();
    }


    public override void OnShow()
    {
        Refresh();

        base.OnShow();
    }

    public void Refresh()
    {
        gridScrollView.Setup("Asset List", this, ClickData, SelectingData, DisSelectingData);

        GenerateList();
    }

    void GenerateList()
    {
        foreach (Asset _a in AssetManager.Instance.GetAssetList(filterQuality, filterTagId, filterAssetId))
        {
            GameObject gridItemInstance = gridScrollView.GenerateItem("Asset/" + _a.assetId.ToString("000"), _a.assetUid);
        }
    }

    void ClickData(int uid, GridItem gi)
    {
        if (clickCallback == null)
        {
            return;
        }
        clickCallback(uid, gi);
    }

    void SelectingData(int uid, GridItem gi)
    {
        Asset _a = AssetManager.Instance.GetAssetByUid(uid);
        UIManager.Instance.assetDataUI.Show(_a, 1200);
    }

    void DisSelectingData(int uid, GridItem gi)
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!UIManager.Instance.IsCurrentUI(this))
        {
            return;
        }

        if (ControlView.Instance.controls.Map1.Cancel.triggered)
        {
            UIManager.Instance.assetInCompositionDataUI.Hide();
            UIManager.Instance.compositionDataUI.Hide();
            OnBackPressed();
        }
    }

    public void AddUI(GridItemCallback _clickCallback = null, int _filterQuality = 0, int _filterTagId = 0, int _filterAssetId = 0)
    {
        clickCallback = _clickCallback;
        filterQuality = _filterQuality;
        filterTagId = _filterTagId;
        filterAssetId = _filterAssetId;

        base.AddUI();
    }
}
