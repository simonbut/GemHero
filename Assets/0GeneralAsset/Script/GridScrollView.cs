using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ClassHelper;
using TMPro;

public class GridScrollView : MonoBehaviour
{

    public int gridCountInLine = 3;
    public Scrollbar scrollbar;
    public ScrollRect scrollRect;
    public GameObject scrollViewContent;
    public GameObject gridItem;
    //public GameObject gridStyleBoxItem;
    public TextMeshProUGUI title;

    public GridItemCallback clickCallback;
    public GridItemCallback selectingCallback;
    public GridItemCallback disselectingCallback;
    public GridItemCallback dragCallBack;

    public List<GameObject> listItemList;
    public Sprite highlightedSprite;
    public Sprite normalSprite;

    ControlableUI controlableUI;

    public int scrollViewId = 0;
    int preGlobalScrollViewId = 0;
    public int scrollViewCount = 3;
    public bool isMultipleScrollView = false;

    public bool isFocus = true; //for controller

    // Update is called once per frame
    void Update()
    {
        if (isFocus)
        {


            //print("Mouse ScrollWheel" + Input.GetAxis("Mouse ScrollWheel"));
            scrollRect.verticalNormalizedPosition = Mathf.Clamp(scrollRect.verticalNormalizedPosition + Input.GetAxis("Mouse ScrollWheel"), 0, 1);

            if (JoyStickManager.Instance.IsJoyStickEnable())
            {
                for (int i = 0; i < listItemList.Count; i++)
                {
                    listItemList[i].GetComponent<Image>().sprite = normalSprite;
                }
                if (scrollViewId == GlobalCommunicateManager.selectingScrollViewId)
                {
                    listItemList[GlobalCommunicateManager.selectingId].GetComponent<Image>().sprite = highlightedSprite;
                }
            }

            if (scrollViewId == GlobalCommunicateManager.selectingScrollViewId && JoyStickManager.Instance.IsJoyStickEnable() && UIManager.Instance.IsCurrentUI(controlableUI))
            {
                if (preGlobalScrollViewId != scrollViewId)
                {
                    GlobalCommunicateManager.selectingId = 0;
                    if (listItemList[GlobalCommunicateManager.selectingId].GetComponent<GridItem>() != null)
                    {
                        listItemList[GlobalCommunicateManager.selectingId].GetComponent<GridItem>().Selecting();
                    }
                }
                else
                {
                    if (JoyStickManager.Instance.IsInputDown("Circle"))
                    {
                        if (listItemList[GlobalCommunicateManager.selectingId].GetComponent<GridItem>() != null)
                        {
                            listItemList[GlobalCommunicateManager.selectingId].GetComponent<GridItem>().Clicked();
                        }
                    }

                    if (JoyStickManager.Instance.IsInputDown("Right"))
                    {
                        if (GlobalCommunicateManager.selectingId + 1 < listItemList.Count)
                        {
                            GlobalCommunicateManager.selectingId++;
                        }
                        else
                        {
                            GlobalCommunicateManager.selectingId = 0;
                        }
                        if (listItemList[GlobalCommunicateManager.selectingId].GetComponent<GridItem>() != null)
                        {
                            listItemList[GlobalCommunicateManager.selectingId].GetComponent<GridItem>().Selecting();
                        }
                    }
                    if (JoyStickManager.Instance.IsInputDown("Left"))
                    {
                        if (GlobalCommunicateManager.selectingId - 1 >= 0)
                        {
                            GlobalCommunicateManager.selectingId--;
                        }
                        else
                        {
                            GlobalCommunicateManager.selectingId = listItemList.Count - 1;
                        }
                        if (listItemList[GlobalCommunicateManager.selectingId].GetComponent<GridItem>() != null)
                        {
                            listItemList[GlobalCommunicateManager.selectingId].GetComponent<GridItem>().Selecting();
                        }
                    }
                    if (JoyStickManager.Instance.IsInputDown("Up"))
                    {
                        if (GlobalCommunicateManager.selectingId - gridCountInLine >= 0)
                        {
                            GlobalCommunicateManager.selectingId -= gridCountInLine;
                            if (listItemList[GlobalCommunicateManager.selectingId].GetComponent<GridItem>() != null)
                            {
                                listItemList[GlobalCommunicateManager.selectingId].GetComponent<GridItem>().Selecting();
                            }
                        }
                        else
                        {
                            if (isMultipleScrollView)
                            {
                                GlobalCommunicateManager.selectingId = 0;
                                GlobalCommunicateManager.selectingScrollViewId--;
                                if (GlobalCommunicateManager.selectingScrollViewId < 0)
                                {
                                    GlobalCommunicateManager.selectingScrollViewId = scrollViewCount - 1;
                                }
                            }
                            else
                            {
                                GlobalCommunicateManager.selectingId = listItemList.Count - 1;
                                if (listItemList[GlobalCommunicateManager.selectingId].GetComponent<GridItem>() != null)
                                {
                                    listItemList[GlobalCommunicateManager.selectingId].GetComponent<GridItem>().Selecting();
                                }
                            }
                        }
                    }
                    if (JoyStickManager.Instance.IsInputDown("Down"))
                    {
                        if (GlobalCommunicateManager.selectingId + gridCountInLine < listItemList.Count)
                        {
                            GlobalCommunicateManager.selectingId += gridCountInLine;
                            if (listItemList[GlobalCommunicateManager.selectingId].GetComponent<GridItem>() != null)
                            {
                                listItemList[GlobalCommunicateManager.selectingId].GetComponent<GridItem>().Selecting();
                            }
                        }
                        else
                        {
                            if (isMultipleScrollView)
                            {
                                GlobalCommunicateManager.selectingId = 0;
                                GlobalCommunicateManager.selectingScrollViewId++;
                                if (GlobalCommunicateManager.selectingScrollViewId > scrollViewCount - 1)
                                {
                                    GlobalCommunicateManager.selectingScrollViewId = 0;
                                }
                            }
                            else
                            {
                                GlobalCommunicateManager.selectingId = 0;
                                if (listItemList[GlobalCommunicateManager.selectingId].GetComponent<GridItem>() != null)
                                {
                                    listItemList[GlobalCommunicateManager.selectingId].GetComponent<GridItem>().Selecting();
                                }
                            }
                        }
                    }
                }


                scrollbar.value = (scrollbar.value * 3f + 1f - Mathf.CeilToInt((GlobalCommunicateManager.selectingId + 1) * 1f / gridCountInLine - 1f) * 1f / (Mathf.CeilToInt((listItemList.Count) * 1f / gridCountInLine - 1f))) / 4f;

                //scrollbar.value = 1f - Mathf.CeilToInt((GlobalCommunicateManager.selectingId + 1) * 1f / gridCountInLine - 1f) * 1f / (Mathf.CeilToInt((listItemList.Count) * 1f / gridCountInLine - 1f));
            }
        }
        preGlobalScrollViewId = GlobalCommunicateManager.selectingScrollViewId;
    }

    public void Setup(string _title, ControlableUI _controlableUI, GridItemCallback _click, GridItemCallback _selecting, GridItemCallback _disselecting, GridItemCallback _dragCallBack = null)
    {
        foreach (Transform _child in scrollViewContent.transform)
        {
            Destroy(_child.gameObject);
        }
        controlableUI = _controlableUI;
        clickCallback = _click;
        selectingCallback = _selecting;
        disselectingCallback = _disselecting;
        dragCallBack = _dragCallBack;
        title.text = Database.GetLocalizedText(_title);
        GlobalCommunicateManager.selectingId = 0;
        GlobalCommunicateManager.selectingScrollViewId = 0;
        listItemList = new List<GameObject>();
    }

    //public void ShowFirstItem()
    //{
    //    if (scrollViewContent.transform.childCount == 1)
    //    {
    //        listItemList[0].GetComponent<Button>().Select();
    //    }
    //}

    public GameObject GenerateItem(string graphPath, int _id, string graphPath2 = "", string _text = "", bool isEnabled = true, string graphPath3 = "")
    {
        //List<Facility> _fl = FacilityManager.Instance.GetFacilityList();
        //foreach (FacilityComponent _c in componentList)
        //{
        GameObject listItemInstance = Instantiate(gridItem);
        listItemInstance.transform.SetParent(scrollViewContent.transform);
        listItemInstance.transform.localScale = Vector3.one;
        listItemInstance.GetComponent<GridItem>().SetGridItem(graphPath, _id, listItemList.Count, Click, Selecting, DisSelecting, graphPath2, _text, isEnabled, Drag, graphPath3);
        listItemList.Add(listItemInstance);

        if (JoyStickManager.Instance.IsJoyStickEnable() && listItemList.Count == 1 && scrollViewId == GlobalCommunicateManager.selectingScrollViewId)
        {
            if (listItemList[0].GetComponent<GridItem>() != null)
            {
                listItemList[0].GetComponent<GridItem>().Selecting();
            }
        }

        //if (scrollViewContent.transform.childCount == 1)
        //{
        //    listItemInstance.GetComponent<Button>().Select();
        //}

        //ListItemInstance.transform.Find("Name").GetComponent<Text>()
        //}
        return listItemInstance;
    }

    //public GameObject GenerateStyleBox(int[] styleBoxArray, int _id)
    //{
    //    //List<Facility> _fl = FacilityManager.Instance.GetFacilityList();
    //    //foreach (FacilityComponent _c in componentList)
    //    //{
    //    GameObject listItemInstance = Instantiate(gridStyleBoxItem);
    //    listItemInstance.transform.SetParent(scrollViewContent.transform);
    //    listItemInstance.transform.localScale = Vector3.one;
    //    listItemInstance.GetComponent<GridItemStyleBox>().SetGridItem(styleBoxArray, _id, listItemList.Count, Click, Selecting, DisSelecting);
    //    listItemList.Add(listItemInstance);

    //    //ListItemInstance.transform.Find("Name").GetComponent<Text>()
    //    //}

    //    //if (scrollViewContent.transform.childCount == 1)
    //    //{
    //    //    listItemInstance.GetComponent<Button>().Select();
    //    //}

    //    return listItemInstance;
    //}

    void Click(int id, GridItem gi)
    {
        clickCallback?.Invoke(id, gi);
    }

    void Selecting(int id, GridItem gi)
    {
        //foreach (GameObject _g in listItemList)
        //{
        //    if (_g.GetComponent<GridItem>() != null && id == _g.GetComponent<GridItem>().id)
        //    {
        //        UIManager.Instance.ShowItemChoosingIndicator(_g.transform.position + new Vector3(-25f, 25f, 0f));
        //    }
        //}
        selectingCallback?.Invoke(id, gi);
    }

    void DisSelecting(int id, GridItem gi)
    {
        disselectingCallback?.Invoke(id, gi);
    }

    void Drag(int id, GridItem gi)
    {
        dragCallBack?.Invoke(id, gi);
    }

}
