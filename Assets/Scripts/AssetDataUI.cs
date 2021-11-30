using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClassHelper;
using UnityEngine.UI;

public class AssetDataUI : DataUI
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [SerializeField] GameObject tagListParent;
    [SerializeField] List<GameObject> tagList;
    [SerializeField] GameObject graphic;
    [SerializeField] GameObject assetName;
    [SerializeField] GameObject assetType;
    [SerializeField] GameObject quality;
    [SerializeField] GameObject rank;
    [SerializeField] GameObject firePoint;
    [SerializeField] GameObject waterPoint;
    [SerializeField] GameObject earthPoint;
    public void Show(Asset _ra)
    {
        //graphic TODO

        assetName.transform.Find("Text").GetComponent<Text>().text = _ra.GetAssetData().name.GetString();
        assetType.transform.Find("Text").GetComponent<Text>().text = "Types: " + AssetManager.Instance.AssetTypeListToString(_ra.GetAssetData().GetAssetTypeList());
        quality.transform.Find("Text").GetComponent<Text>().text = "Quality: " + _ra.GetQuality().ToString("0");
        rank.transform.Find("Text").GetComponent<Text>().text = _ra.GetRank().ToString();
        firePoint.transform.Find("Text").GetComponent<Text>().text = "Fire: " + _ra.GetFirePoint().ToString();
        waterPoint.transform.Find("Text").GetComponent<Text>().text = "Water: " + _ra.GetWaterPoint().ToString();
        earthPoint.transform.Find("Text").GetComponent<Text>().text = "Earth: " + _ra.GetEarthPoint().ToString();

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
