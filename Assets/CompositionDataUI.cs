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
    [SerializeField] GameObject firePoint;
    [SerializeField] GameObject waterPoint;
    [SerializeField] GameObject earthPoint;
    [SerializeField] GameObject attr1;
    [SerializeField] GameObject attr2;
    public void Show(int _compoundId)
    {
        AssetData _ad = AssetManager.Instance.GetAssetData(_compoundId);

        //graphic TODO
        //shape TODO

        assetName.transform.Find("Text").GetComponent<Text>().text = _ad.name.GetString();
        assetType.transform.Find("Text").GetComponent<Text>().text = "Types: " + AssetManager.Instance.AssetTypeListToString(_ad.GetAssetTypeList());


        firePoint.transform.Find("Text").GetComponent<Text>().text = "Fire: " + _ad.realityPoint.ToString();
        waterPoint.transform.Find("Text").GetComponent<Text>().text = "Water: " + _ad.dreamPoint.ToString();
        earthPoint.transform.Find("Text").GetComponent<Text>().text = "Earth: " + _ad.idealPoint.ToString();

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

        OnShow();
    }

    public void Hide()
    {
        OnHide();
    }
}
