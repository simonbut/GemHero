using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClassHelper;

public class CompositeMenuCanvas : ControlableUI
{
    public GridScrollView gridScrollView;

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
        foreach (Asset _a in Database.userDataJson.assetList)
        {
            GameObject gridItemInstance = gridScrollView.GenerateItem("", _a.assetUid);//Graph TODO
        }
    }

    void ClickData(int uid, GridItem gi)
    {

    }

    void SelectingData(int uid, GridItem gi)
    {
        Asset _a = AssetManager.Instance.GetAssetByUid(uid);
        UIManager.Instance.assetInCompositionDataUI.Show(_a);
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

        //if (JoyStickManager.Instance.IsInputDown("Cross") || Input.GetButtonDown("Escape"))
        //{
        //    OnBackPressed();
        //}
    }
}
