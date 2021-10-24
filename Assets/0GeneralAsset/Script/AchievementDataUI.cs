using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ClassHelper;
using TMPro;

public class AchievementDataUI : DataUI
{
    public Text descriptionText;
    //public Image image;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Show(int _achievementId)
    {
        Achievement _a = AchievementManager.Instance.GetAchievement(_achievementId);
        descriptionText.text = _a.name.GetString() + "\n" + _a.description.GetString();
        //descriptionText.text += "\n" + "<color=#AAAAAA>" + Database.GetLocalizedText("Command reward") + ":" + "</color>"+ OrderManager.Instance.GetOrder(_a.rewardOrder).name.GetString()
        //    + ":" + OrderManager.Instance.GetOrder(_a.rewardOrder).description.GetString();

        //if (_a.IsAchievementGet())
        //{
        //    //descriptionText.text += "<Color=#AAFFAA>" + Database.GetLocalizedText("You have completed this Achievement!") + "</Color>";
        //}
        //else
        //{
        //    descriptionText.text += "\n" + "\n" + "<color=#FFAAAA>" + Database.GetLocalizedText("You have not completed this Achievement yet.") + "</color>";
        //}

        //image.sprite = Resources.Load<Sprite>("Graphic/Order/" + _a.rewardOrder.ToString("000"));
        
        gameObject.SetActive(true);

        OnShow();
    }
    
    public void Hide()
    {
        OnHide();
    }
}
