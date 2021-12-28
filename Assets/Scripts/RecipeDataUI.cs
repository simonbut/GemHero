using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClassHelper;
using UnityEngine.UI;

public class RecipeDataUI : DataUI
{
    [SerializeField] List<GameObject> recipeAssetList;
    public void Show(int _recipeId)
    {
        RecipeData _rd = AssetManager.Instance.GetRecipeData(_recipeId);

        for (int i = 0; i < recipeAssetList.Count; i++)
        {
            if (i < _rd.assetTypeList.Count)
            {
                recipeAssetList[i].transform.Find("Name").transform.Find("Text").GetComponent<Text>().text = AssetManager.Instance.GetAssetTypeData(_rd.assetTypeList[i]).name.GetString();
                recipeAssetList[i].transform.Find("IconBg").transform.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("AssetType/" + _rd.assetTypeList[i].ToString("000"));
            }
            else
            {
                recipeAssetList[i].SetActive(false);
            }
        }

        OnShow();
    }

    public void Hide()
    {
        OnHide();
    }
}
