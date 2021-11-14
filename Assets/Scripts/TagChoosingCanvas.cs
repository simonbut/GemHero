using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClassHelper;

public class TagChoosingCanvas : ControlableUI
{
    public ListScrollView listScrollView;

    public override void OnRemoveUI()
    {
        UIManager.Instance.HideAllDataUI();

        base.OnRemoveUI();
    }


    public override void OnShow()
    {
        Refresh();
        //MainMenuView.Instance.tankModelShowCase.ShowTankModelByTankDeckId(Database.userDataJson.selectingDeckId);

        base.OnShow();
    }

    public void Refresh()
    {
        listScrollView.Setup("Tag List", this, ClickData, SelectingData, DisSelectingData);

        GenerateList();
    }

    void GenerateList()
    {
        foreach (TagData _t in TagManager.Instance.GetTagDataFullList())
        {
            GameObject gridItemInstance = listScrollView.GenerateItem(_t.name.GetString(), _t.id);
        }
    }

    void ClickData(int id, ListItem gi)
    {
        CompositeView.Instance.GenerateChoosingTag(id);
    }

    void SelectingData(int id, ListItem gi)
    {

    }

    void DisSelectingData(int id, ListItem gi)
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
