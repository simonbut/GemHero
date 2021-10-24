using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClassHelper;
using TMPro;

public class NewAchievementUI : ControlableUI
{
    public AchievementDataUI achievementDataUI;
    //public CardDataUI cardDataUI;
    public TextMeshProUGUI confirmText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (JoyStickManager.Instance.IsJoyStickEnable() && UIManager.Instance.IsCurrentUI(this))
        {
            if (JoyStickManager.Instance.IsInputDown("Cross"))
            {
                OnBackPressed();
            }
        }
    }

    int cardId;
    //public override void AddUI()
    //{
    //    if (UIManager.Instance.newCardStack.Count > 0)
    //    {
    //        ShowNextCard();

    //        base.AddUI();
    //    }
    //}

    //public void ShowNextCard()
    //{
    //    if (UIManager.Instance.newCardStack.Count > 0)
    //    {
    //        Card _c = CardManager.Instance.GetCard(UIManager.Instance.newCardStack[0]);

    //        achievementDataUI.Show(_c.requiredAchievement);
    //        cardDataUI.Show(_c.id);

    //        UIManager.Instance.newCardStack.RemoveAt(0);
    //    }
    //}

    //public override void OnBackPressed()
    //{
    //    if (UIManager.Instance.newCardStack.Count > 0)
    //    {
    //        ShowNextCard();
    //    }
    //    else
    //    {
    //       base.OnBackPressed();
    //    }
    //}
}
