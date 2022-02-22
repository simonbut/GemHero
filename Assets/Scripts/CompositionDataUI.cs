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
    [SerializeField] GameObject quality;
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
    //[SerializeField] List<GameObject> compositeMarks;

    //Prefab
    [SerializeField] Sprite emptySlotSprite;
    [SerializeField] Sprite fillSlotSprite;

    [SerializeField] Image warnGraph;
    [SerializeField] Text warnText;

    bool showCompositePart = false;

    public void Show(int _recipeId, bool _showCompositePart = false)
    {
        RecipeData _rd = AssetManager.Instance.GetRecipeData(_recipeId);
        AssetData _ad = AssetManager.Instance.GetAssetData(_rd.targetCompoundId);

        graphic.GetComponent<Image>().sprite = Resources.Load<Sprite>("Asset/" + _ad.id.ToString("000"));

        ShapeGenerator.GenerateShape(shape, _rd.shape, null, 0.2f);

        assetName.transform.Find("Text").GetComponent<Text>().text = _ad.name.GetString();
        assetType.transform.Find("Text").GetComponent<Text>().text = AssetManager.Instance.AssetTypeListToString(_ad.GetAssetTypeList());

        for (int i = 0; i < 5; i++)
        {
            if (i < _ad.realityPoint)
            {
                realityPoint.transform.Find("PtGrid" + i).Find("Fill").GetComponent<Image>().fillAmount = 1f;
            }
            else
            {
                realityPoint.transform.Find("PtGrid" + i).Find("Fill").GetComponent<Image>().fillAmount = 0f;
            }
        }

        for (int i = 0; i < 5; i++)
        {
            if (i < _ad.dreamPoint)
            {
                dreamPoint.transform.Find("PtGrid" + i).Find("Fill").GetComponent<Image>().fillAmount = 1f;
            }
            else
            {
                dreamPoint.transform.Find("PtGrid" + i).Find("Fill").GetComponent<Image>().fillAmount = 0f;
            }
        }

        for (int i = 0; i < 5; i++)
        {
            if (i < _ad.idealPoint)
            {
                idealPoint.transform.Find("PtGrid" + i).Find("Fill").GetComponent<Image>().fillAmount = 1f;
            }
            else
            {
                idealPoint.transform.Find("PtGrid" + i).Find("Fill").GetComponent<Image>().fillAmount = 0f;
            }
        }

        switch (_ad.GetCompoundType())
        {
            case CompoundType.asset:
            case CompoundType.compound:
            case CompoundType.consumable:
                attr1.SetActive(false);
                attr2.SetActive(false);
                break;
            case CompoundType.weapon:
            case CompoundType.accessory:
            case CompoundType.clothing:
                attr1.SetActive(false);
                attr2.SetActive(false);
                if (_ad.basicStatTypeList.Count > 0)
                {
                    attr1.SetActive(true);
                    float _min = _ad.basicStatListMin[0] * 1f;
                    float _max = _ad.basicStatListMax[0] * 1f;
                    if (_ad.basicStatTypeList[0] == StatType.ats)
                    {
                        _min /= 1000f;
                        _max /= 1000f;
                    }
                    attr1.transform.Find("Text").GetComponent<Text>().text = _ad.basicStatTypeList[0].ToString() + ": " + _min.ToString("0") + " - " + _max.ToString("0");
                }
                if (_ad.basicStatTypeList.Count > 1)
                {
                    attr2.SetActive(true);
                    float _min = _ad.basicStatListMin[1] * 1f;
                    float _max = _ad.basicStatListMax[1] * 1f;
                    if (_ad.basicStatTypeList[1] == StatType.ats)
                    {
                        _min /= 1000f;
                        _max /= 1000f;
                    }
                    attr2.transform.Find("Text").GetComponent<Text>().text = _ad.basicStatTypeList[1].ToString() + ": " + _min.ToString("0") + " - " + _max.ToString("0");
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
            if (_rd.targetScore[0] > 0)
            {
                realitySlots[_rd.targetScore[0] - 1].transform.Find("Text").GetComponent<Text>().text = "A";
                markCount++;
            }
        }

        for (int i = 0; i < dreamSlots.Count; i++)
        {
            dreamSlots[i].SetActive(i < _rd.capacity[1]);
            dreamSlots[i].transform.Find("Text").GetComponent<Text>().text = "";
        }
        if (_rd.targetTag[1] != 0)
        {
            if (_rd.targetScore[1] > 0)
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
                markCount++;
            }
        }

        for (int i = 0; i < idealSlots.Count; i++)
        {
            idealSlots[i].SetActive(i < _rd.capacity[2]);
            idealSlots[i].transform.Find("Text").GetComponent<Text>().text = "";
        }
        if (_rd.targetTag[2] != 0)
        {
            if (_rd.targetScore[2] > 0)
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
                markCount++;
            }
        }

        quality.SetActive(_showCompositePart);

        showCompositePart = _showCompositePart;

        OnShow();
    }

    public void Update()
    {
        foreach (Transform _c in shape.transform)
        {
            _c.transform.localScale = Vector3.one;
        }

        if (showCompositePart && MainGameView.Instance != null)
        {
            quality.transform.Find("Text2").GetComponent<Text>().text = MainGameView.Instance.compositeMenuCanvas.GetQuality().ToString("0");
            quality.transform.Find("Fill").GetComponent<Image>().fillAmount = MainGameView.Instance.compositeMenuCanvas.GetQuality() * 1f / 100f;

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

            warnText.color = new Color(1, 1, 1, Mathf.Sin(Time.time));
            warnGraph.color = new Color(1, 0, 0, Mathf.Sin(Time.time));
            warnText.gameObject.SetActive(MainGameView.Instance.compositeMenuCanvas.GetScoreOverflow() > 0);
            warnGraph.gameObject.SetActive(MainGameView.Instance.compositeMenuCanvas.GetScoreOverflow() > 0);
            switch (MainGameView.Instance.compositeMenuCanvas.GetScoreOverflow())
            {
                case 0:
                    warnText.text = "";
                    break;
                case 1:
                    warnText.text = "超過了一格，品質下降 50%";
                    break;
                case 2:
                    warnText.text = "超過了兩格，品質下降 70%";
                    break;
                default:
                    warnText.text = "超過了三格或以上，品質下降 80%";
                    break;
            }
        }
    }

    public void Hide()
    {
        showCompositePart = false;
        OnHide();
    }
}
