using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropdownUI : ControlableUI
{
    public Button triggerButton;
    public GameObject listPositionIndicator;
    public List<string> optionList;
    public int value = 0;
    public ListScrollView listScrollView;

    public string title;

    public override void OnShow()
    {
        listScrollView.Setup(title, this, ClickDropDownData, SelectingDropDownData, DisSelectingDropDownData);

        GenerateTradeList();

        base.OnShow();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (JoyStickManager.Instance.IsInputDown("Cross") || Input.GetButtonDown("Escape"))
        //{
        //    if (UIManager.Instance.IsCurrentUI(this))
        //    {
        //        OnBackPressed();
        //    }
        //}
    }


    void GenerateTradeList()
    {
        for (int i = 0; i < optionList.Count; i++)
        {
            GameObject ListItemInstance = listScrollView.GenerateItem(optionList[i],i);
            ListItemInstance.transform.localScale = Vector3.one * 15f;
        }
    }

    void ClickDropDownData(int id, ListItem gi)
    {
        value = id;
        if (triggerButton.GetComponentInChildren<Text>() != null)
        {
            triggerButton.GetComponentInChildren<Text>().text = optionList[id];
        }
        OnBackPressed();
    }

    void SelectingDropDownData(int id, ListItem gi)
    {

    }

    void DisSelectingDropDownData(int id, ListItem gi)
    {

    }
}
