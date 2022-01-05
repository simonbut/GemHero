using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ClassHelper;

public class TurnBaseBattleCanvas : ControlableUI
{
    public GameObject playerInformation;
    public GameObject selectingClue;
    public GameObject aimTarget;
    public GameObject enemyInformation;

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

    void Update()
    {
        List<TurnBaseBattleCharacter> _cl = TurnBaseBattleView.Instance.GetCharacterList(Force.player);
        if (_cl.Count <= 0)
        {
            return;
        }

        //update player information UI
        playerInformation.transform.Find("Text").GetComponent<Text>().text = _cl[0].characterAttribute.GetPlayerInformation().Replace("$1", _cl[0].hpPt.ToString()).Replace("$2", _cl[0].ammoCount.ToString());

        //update aim UI
        if (_cl[0].target == null)
        {
            aimTarget.SetActive(false);
        }
        else
        {
            aimTarget.SetActive(true);
            aimTarget.transform.position = _cl[0].target.gameObject.transform.position;
        }
    }

    public override void OnBackPressed()
    {
        Time.timeScale = 1f;

        base.OnBackPressed();
    }

    public void ShowEnemyInformation(TurnBaseBattleCharacter _character)
    {
        if (_character.force == Force.player)
        {
            return;
        }
        enemyInformation.SetActive(true);
        selectingClue.SetActive(true);
        selectingClue.transform.position = _character.gameObject.transform.position;

        //update enemy information UI
        //enemyInformation.transform.Find("Icon").GetComponent<Image>().sprite =//TODO graphic
        enemyInformation.transform.Find("Text").GetComponent<Text>().text = _character.characterAttribute.GetEnemyInformation().Replace("$1",_character.hpPt.ToString());
    }

    public void HideEnemyInformation()
    {
        enemyInformation.SetActive(false);
        selectingClue.SetActive(false);
    }
}
