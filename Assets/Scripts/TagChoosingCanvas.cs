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
        listScrollView.Setup("DeckList", this, ClickData, SelectingData, DisSelectingData);

        GenerateList();
    }

    void GenerateList()
    {
        GameObject gridItemInstance = listScrollView.GenerateItem("test tag" , 1);
    }

    void ClickData(int id, ListItem gi)
    {
        if (id == 1)
        {

        }
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
