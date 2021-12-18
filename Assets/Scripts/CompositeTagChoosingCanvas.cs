using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ClassHelper;

public class CompositeTagChoosingCanvas : ControlableUI
{
    public ListScrollView listScrollView;

    public GameObject tagDescription;

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
    }

    void ClickData(int id, ListItem gi)
    {
        if (id == -1)
        {
            CompleteComposite();
            return;
        }

        if (tagList[id].GetTagData().tagType == TagType.FixedTag)
        {
            return;
        }

        if (UIManager.Instance.IsCurrentUI(this))
        {
            tagList[id].offset = Vector2Int.zero;
            //TODO remove existing tag (check if FixedTag)
            TagBaseCanvas.Instance.GenerateChoosingTag(tagList[id]);

            UIManager.Instance.AddEmptyUI();
        }

    }

    void SelectingData(int id, ListItem gi)
    {
        if (id == -1)
        {
            tagDescription.SetActive(false);
            return;
        }
        tagDescription.SetActive(true);

        tagDescription.transform.Find("Text").GetComponent<Text>().text = tagList[id].GetTagData().name.GetString() + "\n" + tagList[id].GetTagData().description.GetString();
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
        tagList.AddRange(_staticTagList);
        tagList.AddRange(_tagList);

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

        if (ControlView.Instance.controls.Map1.Cancel.triggered)
        {
            if (TagBaseCanvas.Instance.choosingTag != null)
            {
                TagBaseCanvas.Instance.DisselectChoosingTag();
                UIManager.Instance.OnBackPressed();
            }
        }

        if (!UIManager.Instance.IsCurrentUI(this))
        {
            return;
        }

        if (ControlView.Instance.controls.Map1.Cancel.triggered)
        {
            if (TagBaseCanvas.Instance.choosingTag == null)
            {
                MainGameView.Instance.tagBaseCanvas.Hide();
                OnBackPressed();
            }
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
