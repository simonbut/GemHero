using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClassHelper;
using UnityEngine.UI;

public class CompositionDataUI : DataUI
{
    [SerializeField] GameObject graphic;
    [SerializeField] GameObject assetName;
    [SerializeField] GameObject assetType;
    [SerializeField] GameObject shape;
    [SerializeField] GameObject realityPoint;
    [SerializeField] GameObject dreamPoint;
    [SerializeField] GameObject idealPoint;
    [SerializeField] GameObject attr1;
    [SerializeField] GameObject attr2;

    //Composition Part
    [SerializeField] GameObject compositionPart;
    [SerializeField] List<GameObject> realitySlots;
    [SerializeField] List<GameObject> dreamSlots;
    [SerializeField] List<GameObject> idealSlots;
    [SerializeField] List<GameObject> compositeMarks;

    //Prefab
    [SerializeField] Sprite emptySlotSprite;
    [SerializeField] Sprite fillSlotSprite;

    bool showCompositePart = false;

    public void Show(int _recipeId, bool _showCompositePart = false)
    {
        RecipeData _rd = AssetManager.Instance.GetRecipeData(_recipeId);
        print(_rd.id);
        print(_rd.targetCompoundId);
        AssetData _ad = AssetManager.Instance.GetAssetData(_rd.targetCompoundId);
        print(_ad.id);

        //graphic TODO
        //shape TODO

        assetName.transform.Find("Text").GetComponent<Text>().text = _ad.name.GetString();
        assetType.transform.Find("Text").GetComponent<Text>().text = "Types: " + AssetManager.Instance.AssetTypeListToString(_ad.GetAssetTypeList());


        realityPoint.transform.Find("Text").GetComponent<Text>().text = "Reality: " + _ad.realityPoint.ToString();
        dreamPoint.transform.Find("Text").GetComponent<Text>().text = "Dream: " + _ad.dreamPoint.ToString();
        idealPoint.transform.Find("Text").GetComponent<Text>().text = "Ideal: " + _ad.idealPoint.ToString();

        switch (_ad.compoundType)
        {
            case CompoundType.asset:
            case CompoundType.compound:
            case CompoundType.consumable:
                attr1.SetActive(false);
                attr2.SetActive(false);
                break;
            case CompoundType.weapon:
            case CompoundType.accessory:
                if (_ad.basicStatTypeList.Count > 0)
                {
                    attr1.SetActive(true);
                    attr1.transform.Find("Text").GetComponent<Text>().text = _ad.basicStatTypeList[0].ToString() + ": " + _ad.basicStatList[0].ToString("0");
                }
                if (_ad.basicStatTypeList.Count > 1)
                {
                    attr2.SetActive(true);
                    attr1.transform.Find("Text").GetComponent<Text>().text = _ad.basicStatTypeList[1].ToString() + ": " + _ad.basicStatList[1].ToString("0");
                }
                break;
        }

        int markCount = 0;
        compositionPart.SetActive(_showCompositePart);
        for (int i = 0; i < realitySlots.Count; i++)
        {
            realitySlots[i].SetActive(i < _rd.capacity[0]);
            realitySlots[i].transform.Find("Text").GetComponent<Text>().text = "";
        }
        if (_rd.targetTag[0] != 0)
        {
            realitySlots[_rd.targetScore[0] - 1].transform.Find("Text").GetComponent<Text>().text = "A";
            markCount++;
        }

        for (int i = 0; i < dreamSlots.Count; i++)
        {
            dreamSlots[i].SetActive(i < _rd.capacity[1]);
        }
        if (_rd.targetTag[1] != 0)
        {
            switch (markCount)
            {
                case 0:
                    dreamSlots[_rd.targetScore[1] - 1].transform.Find("Text").GetComponent<Text>().text = "A";
                    break;
                case 1:
                    dreamSlots[_rd.targetScore[1] - 1].transform.Find("Text").GetComponent<Text>().text = "B";
                    break;
            }
        }

        for (int i = 0; i < idealSlots.Count; i++)
        {
            idealSlots[i].SetActive(i < _rd.capacity[2]);
        }
        if (_rd.targetTag[2] != 0)
        {
            switch (markCount)
            {
                case 0:
                    idealSlots[_rd.targetScore[2] - 1].transform.Find("Text").GetComponent<Text>().text = "A";
                    break;
                case 1:
                    idealSlots[_rd.targetScore[2] - 1].transform.Find("Text").GetComponent<Text>().text = "B";
                    break;
                case 2:
                    idealSlots[_rd.targetScore[2] - 1].transform.Find("Text").GetComponent<Text>().text = "C";
                    break;
            }
        }

        showCompositePart = _showCompositePart;

        OnShow();
    }

    public void Update()
    {
        if (showCompositePart && MainGameView.Instance != null)
        {
            int realityPt = MainGameView.Instance.compositeMenuCanvas.GetPoints()[0];
            int dreamPt = MainGameView.Instance.compositeMenuCanvas.GetPoints()[1];
            int idealPt = MainGameView.Instance.compositeMenuCanvas.GetPoints()[2];
            for (int i = 0; i < realitySlots.Count; i++)
            {
                if (i < realityPt)
                {
                    realitySlots[i].GetComponent<Image>().sprite = fillSlotSprite;
                }
                else
                {
                    realitySlots[i].GetComponent<Image>().sprite = emptySlotSprite;
                }
            }
            for (int i = 0; i < dreamSlots.Count; i++)
            {
                if (i < dreamPt)
                {
                    dreamSlots[i].GetComponent<Image>().sprite = fillSlotSprite;
                }
                else
                {
                    dreamSlots[i].GetComponent<Image>().sprite = emptySlotSprite;
                }
            }
            for (int i = 0; i < idealSlots.Count; i++)
            {
                if (i < idealPt)
                {
                    idealSlots[i].GetComponent<Image>().sprite = fillSlotSprite;
                }
                else
                {
                    idealSlots[i].GetComponent<Image>().sprite = emptySlotSprite;
                }
            }
        }
    }

    public void Hide()
    {
        showCompositePart = false;
        OnHide();
    }
}
