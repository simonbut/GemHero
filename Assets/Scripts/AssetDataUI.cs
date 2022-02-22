using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClassHelper;
using UnityEngine.UI;

public class AssetDataUI : DataUI
{
    [SerializeField] GameObject background;
    [SerializeField] GameObject contents;

    [SerializeField] GameObject tagListParent;
    [SerializeField] List<GameObject> tagList;
    [SerializeField] GameObject graphic;
    [SerializeField] GameObject assetName;
    [SerializeField] GameObject assetType;
    [SerializeField] GameObject quality;
    [SerializeField] GameObject rank;
    [SerializeField] GameObject realityPoint;
    [SerializeField] GameObject dreamPoint;
    [SerializeField] GameObject idealPoint;
    [SerializeField] GameObject attr1;
    [SerializeField] GameObject attr2;
    public void Show(Asset _ra,float x = 800)
    {
        background.transform.position = new Vector2(x, background.transform.position.y);
        contents.transform.position = new Vector2(x, contents.transform.position.y);

        graphic.GetComponent<Image>().sprite = Resources.Load<Sprite>("Asset/" + _ra.assetId.ToString("000"));

        AssetData _ad = _ra.GetAssetData();

        assetName.transform.Find("Text").GetComponent<Text>().text = _ad.name.GetString();
        assetType.transform.Find("Text").GetComponent<Text>().text = AssetManager.Instance.AssetTypeListToString(_ad.GetAssetTypeList());
        quality.transform.Find("Text2").GetComponent<Text>().text = _ra.GetQuality().ToString("0");
        quality.transform.Find("Fill").GetComponent<Image>().fillAmount = _ra.GetQuality() * 1f / 100f;
        rank.transform.Find("Text").GetComponent<Text>().text = _ra.GetRank().ToString();

        for (int i = 0; i < 5; i++)
        {
            if (i < _ra.GetRealityPoint())
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
            if (i < -_ra.GetRealityPoint())
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
            if (i < _ra.GetIdealPoint())
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
                    float _value = _ra.GetAttr()[0] * 1f;
                    if (_ad.basicStatTypeList[0] == StatType.ats)
                    {
                        _value /= 1000f;
                    }
                    attr1.transform.Find("Text").GetComponent<Text>().text = _ad.basicStatTypeList[0].ToString() + ": " + _value.ToString("0.0");
                }
                if (_ad.basicStatTypeList.Count > 1)
                {
                    attr2.SetActive(true);
                    float _value = _ra.GetAttr()[1] * 1f;
                    if (_ad.basicStatTypeList[1] == StatType.ats)
                    {
                        _value /= 1000f;
                    }
                    attr2.transform.Find("Text").GetComponent<Text>().text = _ad.basicStatTypeList[1].ToString() + ": " + _value.ToString("0.0");
                }
                break;
        }

        if (_ra.tagList.Count == 0)
        {
            tagListParent.SetActive(false);
        }
        else
        {
            tagListParent.SetActive(true);
            List<Tag> _tl = AssetManager.Instance.CreateTagListByAssets(new int[1] { _ra.assetUid });
            for (int i = 0; i < tagList.Count; i++)
            {
                if (i < _tl.Count)
                {
                    tagList[i].SetActive(true);
                    tagList[i].transform.Find("Text").GetComponent<Text>().text = _tl[i].GetTagData().name.GetString();
                    ShapeGenerator.GenerateShape(tagList[i].transform.Find("Grid").gameObject, null, new List<Tag> { _tl[i] }, 0.15f);
                }
                else
                {
                    tagList[i].SetActive(false);
                }
            }
        }

        OnShow();
    }

    public void Hide()
    {
        OnHide();
    }
}
