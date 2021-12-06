using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClassHelper;

public class RecipeMenuCanvas : ControlableUI
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
        listScrollView.Setup("Recipe List", this, ClickData, SelectingData, DisSelectingData);

        GenerateList();
    }

    void GenerateList()
    {
        foreach (RecipeData _r in AssetManager.Instance.GetRecipeDataFullList())
        {
            AssetData _compound = AssetManager.Instance.GetAssetData(_r.targetCompoundId);
            GameObject gridItemInstance = listScrollView.GenerateItem(_compound.name.GetString(), _r.id);
        }
    }

    void ClickData(int id, ListItem gi)
    {
        //CompositeView.Instance.StartAssetChoosing(id);
    }

    void SelectingData(int id, ListItem gi)
    {
        RecipeData recipeData = AssetManager.Instance.GetRecipeData(id);
        UIManager.Instance.compositionDataUI.Show(id);
        UIManager.Instance.recipeDataUI.Show(id);
    }

    void DisSelectingData(int id, ListItem gi)
    {
        UIManager.Instance.compositionDataUI.Hide();
        UIManager.Instance.recipeDataUI.Hide();
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
