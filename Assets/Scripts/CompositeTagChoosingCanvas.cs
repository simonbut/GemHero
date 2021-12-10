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
        GameObject gridItemInstance2 = listScrollView.GenerateItem("Complete", -1);
    }

    void ClickData(int id, ListItem gi)
    {
        if (id == -1)
        {
            CompleteComposite();
            return;
        }

        tagList[id].offset = Vector2Int.zero;
        //TODO remove existing tag (check if FixedTag)
        TagBaseCanvas.Instance.GenerateChoosingTag(tagList[id]);
    }

    void SelectingData(int id, ListItem gi)
    {
        if (id == -1)
        {
            return;
        }
    }

    void DisSelectingData(int id, ListItem gi)
    {
        if (id == -1)
        {
            return;
        }
    }

    List<Tag> tagList;
    RecipeData recipe;
    int compoundQuality;
    //assetSelectList, TargetTagListWithEnoughPoints()
    public void AddUI(int _compoundQuality, RecipeData _recipe, List<Tag> _tagList,List<Tag> _staticTagList)
    {
        recipe = _recipe;
        compoundQuality = _compoundQuality;

        tagList = new List<Tag>();
        tagList.AddRange(_tagList);
        tagList.AddRange(_staticTagList);

        for (int i = 0; i < tagList.Count; i++)
        {
            tagList[i].localIndex = i;
        }

        MainGameView.Instance.tagBaseCanvas.Show(_recipe.shape, _staticTagList);

        base.AddUI();
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
            MainGameView.Instance.tagBaseCanvas.Hide();
            OnBackPressed();
        }
    }

    void CompleteComposite()
    {
        Asset compositeAsset = new Asset();
        compositeAsset.assetId = recipe.targetCompoundId;
        compositeAsset.tagList = MainGameView.Instance.tagBaseCanvas.GetExistingTagIdList();
        compositeAsset.qualityAffect = compositeAsset.CalculateQualityAffectByQuality(compoundQuality);
        Database.AddAsset(compositeAsset);

        MainGameView.Instance.LeaveComposition();

        MainGameView.Instance.assetConfirmCanvas.AddUI(compositeAsset);
    }
}
