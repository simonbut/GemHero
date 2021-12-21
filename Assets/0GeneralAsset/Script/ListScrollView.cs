using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ClassHelper;

public class ListScrollView : MonoBehaviour
{
    public Scrollbar scrollbar;
    public ScrollRect scrollRect;
    public GameObject scrollViewContent;
    public GameObject listItem;
    public Text title;
    
    public ListItemCallback clickCallback;
    public ListItemCallback selectingCallback;
    public ListItemCallback disselectingCallback;

    public List<GameObject> listItemList;

    ControlableUI controlableUI;

    //public int scrollViewId = 0;

    // Update is called once per frame
    public void Update()
    {
        //print("Mouse ScrollWheel" + Input.GetAxis("Mouse ScrollWheel"));
        scrollRect.verticalNormalizedPosition = Mathf.Clamp(scrollRect.verticalNormalizedPosition + Input.GetAxis("Mouse ScrollWheel"), 0, 1);

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

    public void Setup(string _title,ControlableUI _controlableUI, ListItemCallback _click, ListItemCallback _selecting, ListItemCallback _disselecting)
    {
        foreach (Transform _child in scrollViewContent.transform)
        {
            Destroy(_child.gameObject);
        }
        controlableUI = _controlableUI;
        clickCallback = _click;
        selectingCallback = _selecting;
        disselectingCallback = _disselecting;
        title.text = Database.GetLocalizedText(_title);
        GlobalCommunicateManager.selectingId = 0;
        GlobalCommunicateManager.selectingScrollViewId = 0;
        listItemList = new List<GameObject>();
    }

    public GameObject GenerateItem(string _name, int _id)
    {
        GameObject listItemInstance = Instantiate(listItem);
        listItemInstance.transform.SetParent(scrollViewContent.transform);
        listItemInstance.GetComponent<ListItem>().SetListItem(_name, _id , listItemList.Count, Click, Selecting, DisSelecting);

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
        clickCallback?.Invoke(id, gi);
    }

    void Selecting(int id, ListItem gi)
    {
        selectingCallback?.Invoke(id, gi);
    }

    void DisSelecting(int id, ListItem gi)
    {
        disselectingCallback?.Invoke(id, gi);
    }

}
