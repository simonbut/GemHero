using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClassHelper;

public class CompositeTagChoosingCanvas : ControlableUI
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
        for (int i = 0; i < tagList.Count; i++)
        {
            GameObject gridItemInstance = listScrollView.GenerateItem(tagList[i].tagData.name.GetString(), i);
        }
        //TODO "complete" button
    }

    void ClickData(int id, ListItem gi)
    {
        tagList[id].offset = Vector2Int.zero;
        //TODO remove existing tag (check if FixedTag)
        TagBaseCanvas.Instance.GenerateChoosingTag(tagList[id]);
    }

    void SelectingData(int id, ListItem gi)
    {

    }

    void DisSelectingData(int id, ListItem gi)
    {

    }

    List<Tag> tagList;
    //assetSelectList, TargetTagListWithEnoughPoints()
    public void AddUI(List<Tag> _tagList,List<Tag> _staticTagList)
    {
        tagList = new List<Tag>();
        tagList.AddRange(_tagList);
        tagList.AddRange(_staticTagList);

        base.AddUI();
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
