using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClassHelper;

public class RecipeMenuCanvas : ControlableUI
{
    public RecipeListScrollView recipeListScrollView;

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
        recipeListScrollView.Setup("Recipe List", this, ClickData, SelectingData, DisSelectingData);

        GenerateList();
    }

    void GenerateList()
    {
        foreach (RecipeData _r in AssetManager.Instance.GetRecipeDataFullList())
        {
            if (Database.userDataJson.chapter >= _r.lockUntilStage || AssetManager.Instance.GetAllLearnedRecipe().Contains(_r.id))
            {
                AssetData _compound = AssetManager.Instance.GetAssetData(_r.targetCompoundId);
                GameObject gridItemInstance = recipeListScrollView.GenerateItem(_compound.name.GetString(), _r.id, _r.hpLoss);
            }
        }
    }

    void ClickData(int id, ListItem gi)
    {
        currentRecipeId = id;
        //CompositeView.Instance.StartAssetChoosing(id);
        UIManager.Instance.compositionPage2DataUI.Hide();
        MainGameView.Instance.compositeMenuCanvas.AddUI(id);
    }

    void SelectingData(int id, ListItem gi)
    {
        currentRecipeId = id;
        RecipeData recipeData = AssetManager.Instance.GetRecipeData(id);
        UIManager.Instance.compositionDataUI.Show(id);
        UIManager.Instance.recipeDataUI.Show(id);
        UIManager.Instance.compositionPage2DataUI.Hide();
        isPage2Showing = false;
    }

    void DisSelectingData(int id, ListItem gi)
    {
        currentRecipeId = 0;
        UIManager.Instance.compositionDataUI.Hide();
        UIManager.Instance.recipeDataUI.Hide();
        UIManager.Instance.compositionPage2DataUI.Hide();
    }

    int currentRecipeId = 1;
    bool isPage2Showing = false;

    // Update is called once per frame
    void Update()
    {
        if (!UIManager.Instance.IsCurrentUI(this))
        {
            return;
        }

        if (ControlView.Instance.controls.Map1.Cancel.triggered)
        {
            UIManager.Instance.compositionDataUI.Hide();
            UIManager.Instance.recipeDataUI.Hide();
            OnBackPressed();
        }

        if (ControlView.Instance.controls.Map1.AssetKey.triggered)
        {
            if(currentRecipeId > 0)
            {
                print("currentRecipeId " + currentRecipeId);
                isPage2Showing = !isPage2Showing;
                if (isPage2Showing)
                {
                    UIManager.Instance.compositionPage2DataUI.Show(currentRecipeId);
                    UIManager.Instance.compositionDataUI.Hide();
                    UIManager.Instance.recipeDataUI.Hide();
                }
                else
                {
                    UIManager.Instance.compositionDataUI.Show(currentRecipeId);
                    UIManager.Instance.recipeDataUI.Show(currentRecipeId);
                    UIManager.Instance.compositionPage2DataUI.Hide();
                }
            }
        }
    }
}
