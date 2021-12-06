using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClassHelper;
using UnityEngine.UI;

public class AssetDataUI : DataUI
{

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
    public void Show(Asset _ra)
    {
        //graphic TODO

        AssetData _ad = _ra.GetAssetData();

        assetName.transform.Find("Text").GetComponent<Text>().text = _ad.name.GetString();
        assetType.transform.Find("Text").GetComponent<Text>().text = "Types: " + AssetManager.Instance.AssetTypeListToString(_ad.GetAssetTypeList());
        quality.transform.Find("Text").GetComponent<Text>().text = "Quality: " + _ra.GetQuality().ToString("0");
        rank.transform.Find("Text").GetComponent<Text>().text = _ra.GetRank().ToString();
        realityPoint.transform.Find("Text").GetComponent<Text>().text = "Reality: " + _ra.GetRealityPoint().ToString();
        dreamPoint.transform.Find("Text").GetComponent<Text>().text = "Dream: " + _ra.GetDreamPoint().ToString();
        idealPoint.transform.Find("Text").GetComponent<Text>().text = "Ideal: " + _ra.GetIdealPoint().ToString();

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
                    attr1.transform.Find("Text").GetComponent<Text>().text = _ad.basicStatTypeList[0].ToString() + ": " + _ra.GetAttr1().ToString("0");
                }
                if (_ad.basicStatTypeList.Count > 1)
                {
                    attr2.SetActive(true);
                    attr1.transform.Find("Text").GetComponent<Text>().text = _ad.basicStatTypeList[1].ToString() + ": " + _ra.GetAttr2().ToString("0");
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
            for (int i = 0; i < tagList.Count; i++)
            {
                if (i < _ra.tagList.Count)
                {
                    tagList[i].transform.Find("Text").GetComponent<Text>().text = TagManager.Instance.GetTag(_ra.tagList[i]).name.GetString();
                    //TODO show shape
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
