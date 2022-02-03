using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ClassHelper;

public class DestinyShareChoosingCanvas : TagChoosingCanvas
{
    public ListScrollView listScrollView;
    public DestinyShareTagCanvas destinyShareTagCanvas;


    public override void OnRemoveUI()
    {
        UIManager.Instance.HideAllDataUI();

        base.OnRemoveUI();
    }


    public override void OnShow()
    {
        tagDescription.SetActive(false);
        Refresh();

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
            GameObject gridItemInstance = listScrollView.GenerateItem(tagList[i].GetTagData().name.GetString(), i);
            //TODO type graphic
        }
        GameObject gridItemInstance2 = listScrollView.GenerateItem("Complete", -1);
        //TODO complete graphic

        listScrollView.gameObject.SetActive(tagList.Count > 0);
    }

    void ClickData(int id, ListItem gi)
    {
        if (id == -1)
        {
            CompleteDestinyShare();
            return;
        }

        if (tagList[id].GetTagData().tagType == TagType.FixedTag)
        {
            return;
        }

        if (UIManager.Instance.IsCurrentUI(this))
        {
            tagList[id].offset = Vector2Int.zero;
            tagList[id].isReady = false;
            //TODO remove existing tag (check if FixedTag)
            destinyShareTagCanvas.GenerateChoosingTag(tagList[id]);

            UIManager.Instance.AddEmptyUI();
        }

    }

    void SelectingData(int id, ListItem gi)
    {
        if (id == -1)
        {
            return;
        }
        DisplayTagDescription(tagList[id].GetTagData().id);
    }

    void DisSelectingData(int id, ListItem gi)
    {
        if (id == -1)
        {
            return;
        }
    }

    List<Tag> tagList;

    public void AddUI(int _destinyShareId)
    {
        destinyShareTagCanvas.ShowPlayerTag();

        List<DestinyShareData> _dsdl = TagManager.Instance.GetDestinyShareDataByCharacterId(_destinyShareId);

        tagList = new List<Tag>();
        for (int i = 0; i < _dsdl.Count; i++)
        {
            tagList.Add(Tag.CreateTag(_dsdl[i].tagId, Vector2Int.zero, new List<int>()));
            tagList[i].localIndex = i + 1000;//don't repeat with player tag local index
        }

        base.AddUI();
    }

    // Update is called once per frame
    void Update()
    {

        if (ControlView.Instance.controls.Map1.Cancel.triggered)
        {
            if (destinyShareTagCanvas.choosingTag != null)
            {
                destinyShareTagCanvas.DisselectChoosingTag();
                UIManager.Instance.OnBackPressed();
            }
        }

        if (!UIManager.Instance.IsCurrentUI(this))
        {
            return;
        }

        if (ControlView.Instance.controls.Map1.Cancel.triggered)
        {
            if (destinyShareTagCanvas.choosingTag == null)
            {
                destinyShareTagCanvas.Hide();
                OnBackPressed();
            }
        }
    }

    void CompleteDestinyShare()
    {
        bool isAllTagsPlaced = true;
        foreach (Tag _t in tagList)
        {
            if (!destinyShareTagCanvas.GetExistingTagList().Contains(_t))
            {
                isAllTagsPlaced = false;
            }
        }
        if (!isAllTagsPlaced)
        {
            return;
        }

        Database.userDataJson.playerTags = destinyShareTagCanvas.GetExistingTagList();

        destinyShareTagCanvas.Hide();
        OnBackPressed();

        //Dialog after
        ResourcePointData _rpd = ResourcePointManager.Instance.GetResourcePointDataByDialogType(MainGameView.Instance.reactingObject.resourcePointId, DialogType.afterDestinyShare);
        print(_rpd.id);
        Database.CompleteDestinyShare(_rpd.characterId);
        MainGameView.Instance.dialogCanvas.Setup(_rpd.targetDialogId, MainGameView.Instance.CheckQuestAfterDestinyShare);
    }
}
