using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ClassHelper;

public class CompositeTagChoosingCanvas : TagChoosingCanvas
{
    public ListScrollView listScrollView;
    public TagBaseCanvas tagBaseCanvas;

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
            tagBaseCanvas.GenerateChoosingTag(tagList[id]);

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

    public int[] assetSelectList;
    List<Tag> tagList;
    RecipeData recipe;
    int compoundQuality;
    public void AddUI(int[] _assetSelectList,int _compoundQuality, RecipeData _recipe, List<Tag> _tagList,List<Tag> _staticTagList)
    {
        assetSelectList = _assetSelectList;
        recipe = _recipe;
        compoundQuality = _compoundQuality;

        tagList = new List<Tag>();
        tagList.AddRange(_staticTagList);
        tagList.AddRange(_tagList);

        for (int i = 0; i < tagList.Count; i++)
        {
            tagList[i].localIndex = i;
        }

        tagBaseCanvas.Show(_recipe.shape, _staticTagList);

        base.AddUI();
    }

    // Update is called once per frame
    void Update()
    {

        if (ControlView.Instance.controls.Map1.Cancel.triggered)
        {
            if (tagBaseCanvas.choosingTag != null)
            {
                tagBaseCanvas.DisselectChoosingTag();
                UIManager.Instance.OnBackPressed();
            }
        }

        if (!UIManager.Instance.IsCurrentUI(this))
        {
            return;
        }

        if (ControlView.Instance.controls.Map1.Cancel.triggered)
        {
            if (tagBaseCanvas.choosingTag == null)
            {
                tagBaseCanvas.Hide();
                OnBackPressed();
            }
        }
    }

    void CompleteComposite()
    {
        Asset compositeAsset = new Asset();
        compositeAsset.assetId = recipe.targetCompoundId;
        compositeAsset.tagList = tagBaseCanvas.GetExistingTagIdList();
        compositeAsset.qualityAffect = compositeAsset.CalculateQualityAffectByQuality(compoundQuality);
        Database.AddAsset(compositeAsset);

        foreach (int _uid in assetSelectList)
        {
            Database.ConsumeAsset(_uid);
        }

        Database.ConsumeHp(recipe.hpLoss);

        tagBaseCanvas.Hide();
        MainGameView.Instance.LeaveComposition();

        MainGameView.Instance.assetConfirmCanvas.AddUI(new List<Asset> { compositeAsset });
    }
}
