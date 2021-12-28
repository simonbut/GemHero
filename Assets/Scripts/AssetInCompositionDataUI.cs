using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClassHelper;
using UnityEngine.UI;

public class AssetInCompositionDataUI : DataUI
{

    [SerializeField] GameObject tagListParent;
    [SerializeField] List<GameObject> tagList;
    [SerializeField] GameObject graphic;
    [SerializeField] GameObject assetName;
    //[SerializeField] GameObject assetType;
    [SerializeField] GameObject quality;
    [SerializeField] GameObject rank;
    [SerializeField] GameObject realityPoint;
    [SerializeField] GameObject dreamPoint;
    [SerializeField] GameObject idealPoint;
    public void Show(Asset _ra)
    {
        graphic.GetComponent<Image>().sprite = Resources.Load<Sprite>("Asset/" + _ra.assetId.ToString("000"));

        AssetData _ad = _ra.GetAssetData();

        assetName.transform.Find("Text").GetComponent<Text>().text = _ad.name.GetString();
        //assetType.transform.Find("Text").GetComponent<Text>().text = AssetManager.Instance.AssetTypeListToString(_ad.GetAssetTypeList());
        //quality.transform.Find("Text").GetComponent<Text>().text = "Quality: " + _ra.GetQuality().ToString("0");
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
            if (i < _ra.GetDreamPoint())
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


        realityPoint.SetActive(_ra.GetRealityPoint() > 0);
        dreamPoint.SetActive(_ra.GetDreamPoint() > 0);

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
