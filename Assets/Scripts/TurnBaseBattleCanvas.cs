using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnBaseBattleCanvas : ControlableUI
{
    //TODO Refactor from TurnBaseBattleView
    public override void AddUI()
    {
        switch (Database.globalData.gameSpeed)
        {
            case 0:
            default:
                Time.timeScale = 1f;
                break;
            case 1:
                Time.timeScale = 1.5f;
                break;
            case 2:
                Time.timeScale = 2f;
                break;
            case 3:
                Time.timeScale = 4f;
                break;
        }

        base.AddUI();
    }

    public override void OnBackPressed()
    {
        Time.timeScale = 1f;

        base.OnBackPressed();
    }
}
