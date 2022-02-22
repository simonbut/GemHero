using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClassHelper;
using UnityEngine.UI;

public class CompositionPage2DataUI : DataUI
{
    [SerializeField] List<GameObject> realitySlots;
    [SerializeField] List<GameObject> dreamSlots;
    [SerializeField] List<GameObject> idealSlots;

    [SerializeField] GameObject realityMarkDescription;
    [SerializeField] GameObject dreamMarkDescription;
    [SerializeField] GameObject idealMarkDescription;

    [SerializeField] GameObject realityMarkTag;
    [SerializeField] GameObject dreamMarkTag;
    [SerializeField] GameObject idealMarkTag;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Show(int _recipeId)
    {
        RecipeData _rd = AssetManager.Instance.GetRecipeData(_recipeId);
        AssetData _ad = AssetManager.Instance.GetAssetData(_rd.targetCompoundId);

        int markCount = 0;
        for (int i = 0; i < realitySlots.Count; i++)
        {
            realitySlots[i].SetActive(i < _rd.capacity[0]);
            realitySlots[i].transform.Find("Text").GetComponent<Text>().text = "";
        }
        if (_rd.targetTag[0] != 0)
        {
            realityMarkDescription.SetActive(true);
            if (_rd.targetScore[0] > 0)
            {
                realitySlots[_rd.targetScore[0] - 1].transform.Find("Text").GetComponent<Text>().text = "A";
                markCount++;

                TagData _t = TagManager.Instance.GetTagData(_rd.targetTag[0]);
                realityMarkTag.transform.Find("Text").GetComponent<Text>().text = _t.name.GetString();
                ShapeGenerator.GenerateShape(realityMarkTag.transform.Find("Grid").gameObject, null, new List<Tag> { Tag.CreateTag(_t.id, Vector2Int.zero, new List<int>()) }, 0.15f);
                realityMarkDescription.transform.Find("Text").GetComponent<Text>().text = realitySlots[_rd.targetScore[0] - 1].transform.Find("Text").GetComponent<Text>().text + " mark:\n" + _t.description.GetString();

            }
            else
            {
                TagData _t = TagManager.Instance.GetTagData(_rd.targetTag[0]);
                realityMarkTag.transform.Find("Text").GetComponent<Text>().text = _t.name.GetString();
                realityMarkDescription.transform.Find("Text").GetComponent<Text>().text = "\n" + _t.description.GetString();
            }
        }
        else
        {
            //realityMarkDescription.SetActive(false);
            realityMarkDescription.transform.Find("Text").GetComponent<Text>().text = "無特殊效果";
        }

        for (int i = 0; i < dreamSlots.Count; i++)
        {
            dreamSlots[i].SetActive(i < _rd.capacity[1]);
            dreamSlots[i].transform.Find("Text").GetComponent<Text>().text = "";
        }
        if (_rd.targetTag[1] != 0)
        {
            dreamMarkDescription.SetActive(true);
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

                TagData _t = TagManager.Instance.GetTagData(_rd.targetTag[1]);
                dreamMarkTag.transform.Find("Text").GetComponent<Text>().text = _t.name.GetString();
                ShapeGenerator.GenerateShape(dreamMarkTag.transform.Find("Grid").gameObject, null, new List<Tag> { Tag.CreateTag(_t.id, Vector2Int.zero, new List<int>()) }, 0.15f);
                dreamMarkDescription.transform.Find("Text").GetComponent<Text>().text = dreamSlots[_rd.targetScore[1] - 1].transform.Find("Text").GetComponent<Text>().text + " mark:\n" + _t.description.GetString();
            }
            else
            {
                TagData _t = TagManager.Instance.GetTagData(_rd.targetTag[1]);
                dreamMarkTag.transform.Find("Text").GetComponent<Text>().text = _t.name.GetString();
                dreamMarkDescription.transform.Find("Text").GetComponent<Text>().text = "\n" + _t.description.GetString();
            }
        }
        else
        {
            //dreamMarkDescription.SetActive(false);
            dreamMarkDescription.transform.Find("Text").GetComponent<Text>().text = "無特殊效果";
        }

        for (int i = 0; i < idealSlots.Count; i++)
        {
            idealSlots[i].SetActive(i < _rd.capacity[2]);
            idealSlots[i].transform.Find("Text").GetComponent<Text>().text = "";
        }
        if (_rd.targetTag[2] != 0)
        {
            idealMarkDescription.SetActive(true);
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

                TagData _t = TagManager.Instance.GetTagData(_rd.targetTag[2]);
                idealMarkTag.transform.Find("Text").GetComponent<Text>().text = _t.name.GetString();
                ShapeGenerator.GenerateShape(idealMarkTag.transform.Find("Grid").gameObject, null, new List<Tag> { Tag.CreateTag(_t.id, Vector2Int.zero, new List<int>()) }, 0.15f);
                idealMarkDescription.transform.Find("Text").GetComponent<Text>().text = idealSlots[_rd.targetScore[2] - 1].transform.Find("Text").GetComponent<Text>().text + " mark:\n" + _t.description.GetString();

            }
            else
            {
                TagData _t = TagManager.Instance.GetTagData(_rd.targetTag[2]);
                idealMarkTag.transform.Find("Text").GetComponent<Text>().text = _t.name.GetString();
                idealMarkDescription.transform.Find("Text").GetComponent<Text>().text = "\n" + _t.description.GetString();
            }
        }
        else
        {
            //idealMarkDescription.SetActive(false);
            idealMarkDescription.transform.Find("Text").GetComponent<Text>().text = "無特殊效果";
        }

        OnShow();
    }

    public void Hide()
    {
        OnHide();
    }
}
