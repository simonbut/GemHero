using ClassHelper;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeneralPanelFilter : ControlableUI
{
    public Image background;

    DropdownUI sortingDropDownItem; // Sorting
    public void SortingDropDownClicked()
    {
        sortingDropDownItem.AddUI();
    }

    DropdownUI filterDropDownItem1; //Mininum Star
    public void FilterDropDown1Clicked()
    {
        filterDropDownItem1.AddUI();
    }

    DropdownUI filterDropDownItem2; //Property
    public void FilterDropDown2Clicked()
    {
        filterDropDownItem2.AddUI();
    }

    //DropdownUI filterDropDownItem3; //Type
    //public void FilterDropDown3Clicked()
    //{
    //    filterDropDownItem3.AddUI();
    //}

    public GameObject selectingSlot;
    public List<GameObject> titleList;
    public List<Button> buttonList;

    //bool isDropDownShowing = false;


    public void UpdateDropDownItems()
    {
        sortingDropDownItem = UIManager.Instance.GenerateDropdownUI(sortingDropDownItem, Database.GetLocalizedText("Sorting"), FilterManager.Instance.sortingItemNames, buttonList[0], buttonList[0].transform.position);
        filterDropDownItem1 = UIManager.Instance.GenerateDropdownUI(filterDropDownItem1, Database.GetLocalizedText("Filter"), FilterManager.Instance.filterItemNames1, buttonList[1], buttonList[1].transform.position);
        filterDropDownItem2 = UIManager.Instance.GenerateDropdownUI(filterDropDownItem2, Database.GetLocalizedText("Filter"), FilterManager.Instance.filterItemNames2, buttonList[2], buttonList[2].transform.position);
        //filterDropDownItem3 = UIManager.Instance.GenerateDropdownUI(filterDropDownItem3, Database.GetLocalizedText("Filter"), FilterManager.Instance.filterItemNames3, buttonList[3], buttonList[3].transform.position);


        //buttonList[0].GetComponentInChildren<Text>().text = sortingDropDownItem.optionList[0];
        //buttonList[1].GetComponentInChildren<Text>().text = filterDropDownItem1.optionList[0];
        //buttonList[2].GetComponentInChildren<Text>().text = filterDropDownItem2.optionList[0];
        //buttonList[3].GetComponentInChildren<Text>().text = filterDropDownItem3.optionList[0];

        //UpdateDropDownItem(sortingDropDownItem, FilterManager.Instance.sortingItemNames);
        //UpdateDropDownItem(filterDropDownItem1, FilterManager.Instance.filterItemNames1);
        //UpdateDropDownItem(filterDropDownItem2, FilterManager.Instance.filterItemNames2);
        //UpdateDropDownItem(filterDropDownItem3, FilterManager.Instance.filterItemNames3);
    }

    void Start()
    {
        buttonList[0].onClick.AddListener(SortingDropDownClicked);
        buttonList[1].onClick.AddListener(FilterDropDown1Clicked);
        buttonList[2].onClick.AddListener(FilterDropDown2Clicked);
        //buttonList[3].onClick.AddListener(FilterDropDown3Clicked);

    }

    void UpdateDropDownItem(Dropdown dropDownItem, List<string> showNames)
    {
        dropDownItem.options.Clear();
        Dropdown.OptionData temoData;
        for (int i = 0; i < showNames.Count; i++)
        {
            temoData = new Dropdown.OptionData();
            temoData.text = showNames[i];
            //temoData.image = sprite_list[i];
            dropDownItem.options.Add(temoData);
        }
        if (showNames.Count > 0)
        {
            dropDownItem.captionText.text = showNames[dropDownItem.value];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!UIManager.Instance.IsCurrentUI(this))
        {
            background.color = new Color(0, 0, 0, 0.7f);
            return;
        }
        else
        {
            background.color = new Color(0, 0, 0, 0.01f);
        }

        if (JoyStickManager.Instance.IsInputDown("Cross") || Input.GetButtonDown("Escape"))
        {
            OnBackPressed();
        }

        if (JoyStickManager.Instance.IsJoyStickEnable())
        {
            selectingSlot.gameObject.SetActive(true);
            selectingSlot.transform.position = titleList[GlobalCommunicateManager.selectingScrollViewId].transform.position;

            if (JoyStickManager.Instance.IsInputDown("Down"))
            {
                if (GlobalCommunicateManager.selectingScrollViewId + 1 < titleList.Count)
                {
                    GlobalCommunicateManager.selectingScrollViewId++;
                }
                else
                {
                    GlobalCommunicateManager.selectingScrollViewId = 0;
                }
            }
            if (JoyStickManager.Instance.IsInputDown("Up"))
            {
                if (GlobalCommunicateManager.selectingScrollViewId - 1 >= 0)
                {
                    GlobalCommunicateManager.selectingScrollViewId--;
                }
                else
                {
                    GlobalCommunicateManager.selectingScrollViewId = titleList.Count - 1;
                }
            }

            switch (GlobalCommunicateManager.selectingScrollViewId)
            {
                case 0://Sorting
                    if (JoyStickManager.Instance.IsInputDown("Circle"))
                    {
                        SortingDropDownClicked();
                    }
                    break;
                case 1://Name
                    if (JoyStickManager.Instance.IsInputDown("Circle"))
                    {
                        FilterDropDown1Clicked();
                    }
                    break;
                case 2://Property
                    if (JoyStickManager.Instance.IsInputDown("Circle"))
                    {
                        FilterDropDown2Clicked();
                    }
                    break;
                //case 3://Type
                //    if (JoyStickManager.Instance.IsInputDown("Circle"))
                //    {
                //        FilterDropDown3Clicked();
                //    }
                //    break;
                case 4://Reset
                    if (JoyStickManager.Instance.IsInputDown("Circle"))
                    {
                        ResetButtonClicked();
                    }
                    break;
                case 5://Confirm
                    if (JoyStickManager.Instance.IsInputDown("Circle"))
                    {
                        ConfirmButtonClicked();
                    }
                    break;
            }
        }

    }

    public void ResetButtonClicked()
    {
        ResetFilteringSetting();
        OnBackPressed();
    }

    public void ConfirmButtonClicked()
    {
        ApplyFilteringSetting();
        OnBackPressed();
    }

    public void ResetFilteringSetting()
    {
        GlobalCommunicateManager.selectingScrollViewId = 0;
        sortingDropDownItem.value = 0;
        filterDropDownItem1.value = 0;
        filterDropDownItem2.value = 0;
        //filterDropDownItem3.value = 0;

        buttonList[0].GetComponentInChildren<Text>().text = sortingDropDownItem.optionList[0];
        buttonList[1].GetComponentInChildren<Text>().text = filterDropDownItem1.optionList[0];
        buttonList[2].GetComponentInChildren<Text>().text = filterDropDownItem2.optionList[0];
        //buttonList[3].GetComponentInChildren<Text>().text = filterDropDownItem3.optionList[0];

        ApplyFilteringSetting();
    }

    public void ApplyFilteringSetting()
    {
        FilterManager.Instance.sortingItemValue = sortingDropDownItem.value;
        FilterManager.Instance.filterItemValue1 = filterDropDownItem1.value;
        FilterManager.Instance.filterItemValue2 = filterDropDownItem2.value;
        //FilterManager.Instance.filterItemValue3 = filterDropDownItem3.value;
    }
}
