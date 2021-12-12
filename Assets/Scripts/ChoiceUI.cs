using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ClassHelper;

public class ChoiceUI : ControlableUI
{
    public int kWidth = 900;

    public Transform transformParent;
    public GameObject buttonPrefab;
    public Text sampleText;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //print("Mouse ScrollWheel" + Input.GetAxis("Mouse ScrollWheel"));
        //scrollRect.verticalNormalizedPosition = Mathf.Clamp(scrollRect.verticalNormalizedPosition + Input.GetAxis("Mouse ScrollWheel"), 0, 1);

        //if (JoyStickManager.Instance.IsJoyStickEnable())
        //{
        //    for (int i = 0; i < listItemList.Count; i++)
        //    {
        //        if (GlobalCommunicateManager.selectingId == i)
        //        {
        //            if (listItemList[i].GetComponent<ListItem>() != null)
        //            {
        //                listItemList[i].GetComponent<ListItem>().isSelecting = true;
        //            }
        //        }
        //        else
        //        {
        //            if (listItemList[i].GetComponent<ListItem>() != null)
        //            {
        //                listItemList[i].GetComponent<ListItem>().isSelecting = false;
        //            }
        //        }
        //    }
        //}


        //if (JoyStickManager.Instance.IsJoyStickEnable() && UIManager.Instance.IsCurrentUI(controlableUI))
        //{
        //    if (JoyStickManager.Instance.IsInputDown("Circle"))
        //    {
        //        if (listItemList[GlobalCommunicateManager.selectingId].GetComponent<ListItem>() != null)
        //        {
        //            listItemList[GlobalCommunicateManager.selectingId].GetComponent<ListItem>().Clicked();
        //        }
        //    }

        //    if (JoyStickManager.Instance.IsInputDown("Down"))
        //    {
        //        if (GlobalCommunicateManager.selectingId + 1 < listItemList.Count)
        //        {
        //            GlobalCommunicateManager.selectingId++;
        //        }
        //        else
        //        {
        //            GlobalCommunicateManager.selectingId = 0;
        //        }
        //        if (listItemList[GlobalCommunicateManager.selectingId].GetComponent<ListItem>() != null)
        //        {
        //            listItemList[GlobalCommunicateManager.selectingId].GetComponent<ListItem>().Selecting();
        //        }
        //    }
        //    if (JoyStickManager.Instance.IsInputDown("Up"))
        //    {
        //        if (GlobalCommunicateManager.selectingId - 1 >= 0)
        //        {
        //            GlobalCommunicateManager.selectingId--;
        //        }
        //        else
        //        {
        //            GlobalCommunicateManager.selectingId = listItemList.Count - 1;
        //        }
        //        if (listItemList[GlobalCommunicateManager.selectingId].GetComponent<ListItem>() != null)
        //        {
        //            listItemList[GlobalCommunicateManager.selectingId].GetComponent<ListItem>().Selecting();
        //        }
        //    }

        //    scrollbar.value = (scrollbar.value * 3f + 1f - GlobalCommunicateManager.selectingId * 1f / (listItemList.Count - 1)) / 4f;
        //}
    }

    List<Callback> buttonCallbackList;
    public List<GameObject> listItemList;

    public void Setup(Vector2 _pos, List<string> _buttonTextList, List<Callback> _buttonCallbackList)
    {
        transform.position = _pos;

        buttonCallbackList = _buttonCallbackList;
        GlobalCommunicateManager.selectingId = 0;
        GlobalCommunicateManager.selectingScrollViewId = 0;
        listItemList = new List<GameObject>();

        foreach (Transform _t in transformParent.transform)
        {
            Destroy(_t.gameObject);
        }

        transform.SetAsLastSibling();

        float maxWidth = 0;
        TextGenerator textGen = new TextGenerator();
        TextGenerationSettings generationSettings = sampleText.GetGenerationSettings(sampleText.rectTransform.rect.size);

        foreach (string _t in _buttonTextList)
        {
            float _w = textGen.GetPreferredWidth(_t, generationSettings);
            if (maxWidth < _w)
            {
                maxWidth = _w;
            }
        }
        if (maxWidth > kWidth)
        {
            maxWidth = kWidth;
        }

        //GenerateButton
        for (int i = 0; i < _buttonTextList.Count; i++)
        {
            float height = textGen.GetPreferredHeight(_buttonTextList[i], generationSettings);
            GenerateItem(_buttonTextList[i], i, maxWidth, height);
        }

        base.AddUI();
    }

    public GameObject GenerateItem(string _name, int _id,float width, float height)
    {
        GameObject listItemInstance = Instantiate(buttonPrefab);
        listItemInstance.transform.SetParent(transformParent.transform);
        listItemInstance.GetComponent<ListItem>().SetListItem(_name, _id, listItemList.Count, Click, null, null);

        listItemInstance.GetComponent<RectTransform>().sizeDelta = new Vector3(width + 50, height + 10);
        listItemInstance.GetComponent<RectTransform>().localPosition = new Vector2(0, - (height + 15) * _id);

        listItemList.Add(listItemInstance);

        //if (JoyStickManager.Instance.IsJoyStickEnable() && listItemList.Count == 1)
        //{
        //    if (listItemList[0].GetComponent<ListItem>() != null)
        //    {
        //        listItemList[0].GetComponent<ListItem>().Selecting();
        //    }
        //}

        return listItemInstance;
    }

    void Click(int id, ListItem gi)
    {
        OnBackPressed();
        buttonCallbackList[id]?.Invoke();
    }

    //void Selecting(int id, ListItem gi)
    //{
    //    selectingCallback?.Invoke(id, gi);
    //}

    //void DisSelecting(int id, ListItem gi)
    //{
    //    disselectingCallback?.Invoke(id, gi);
    //}

}
