using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClassHelper;

public class InGameMainMenuUI : ControlableUI
{
    public ListMenuView listMenuView;

    public override void OnShow()
    {
        listMenuView.SetUp(new List<string> { "Composite"}, this, new List<Callback> { OnCompositeButtonClick });

        base.OnShow();
    }


    public override void AddUI()
    {

        base.AddUI();
    }

    public override void OnRemoveUI()
    {

        base.OnRemoveUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (!UIManager.Instance.IsCurrentUI(this))
        {
            return;
        }

        //if (JoyStickManager.Instance.IsInputDown("Cross") || Input.GetButtonDown("Escape"))
        //{
        //    OnBackPressed();
        //}
    }

    void OnCompositeButtonClick()
    {
        MainGameView.Instance.OpenRecipeMenu();
    }
}

