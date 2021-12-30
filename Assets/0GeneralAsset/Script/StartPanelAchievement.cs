using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;
using ClassHelper;

public class StartPanelAchievement : ControlableUI
{
    //public GameObject scrollViewContent;
    //public GameObject listItem;
    public ListScrollView listScrollView;
    List<Achievement> achievementList;

    public override void OnShow()
    {
        listScrollView.Setup("Achievement", this, ClickAchievementData, SelectingAchievementData, DisSelectingAchievementData);

        achievementList = AchievementManager.Instance.GetAchievementFullList();
        GenerateAchievementListList();

        base.OnShow();
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

    public override void OnRemoveUI()
    {

        base.OnRemoveUI();
    }

    void GenerateAchievementListList()
    {
        //foreach (Transform _child in scrollViewContent.transform)
        //{
        //    Destroy(_child.gameObject);
        //}
        foreach (Achievement _a in achievementList)
        {
            //GameObject ListItemInstance = Instantiate(listItem);
            //ListItemInstance.transform.SetParent(scrollViewContent.transform);
            GameObject ListItemInstance = listScrollView.GenerateItem(_a.name.GetString(), _a.id);

            //ListItemInstance.GetComponent<ListItem>().SetListItem(_a.name.GetString(), _a.achievement_id, ClickAchievementData, SelectingAchievementData, DisSelectingAchievementData);
        }
    }

    void ClickAchievementData(int id, ListItem gi)
    {
        //
    }

    void SelectingAchievementData(int id, ListItem gi)
    {
        //StartView.Instance?.ShowAchievementData(id);
    }

    void DisSelectingAchievementData(int id, ListItem gi)
    {
        //StartView.Instance?.ShowAchievementData(0);
    }
}
