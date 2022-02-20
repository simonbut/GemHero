using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ClassHelper;

public class RewardAfterBattleCanvas : ControlableUI
{
    public List<GameObject> rewardGemList;
    public List<int> gemList;

    bool isCriticalGem = false;

    public void AddUI(bool _isCriticalGem)
    {
        isCriticalGem = _isCriticalGem;

        AddUI();
    }

    public override void AddUI()
    {
        gemList = VirtueGemManager.Instance.GenerateRandomRewardGemList(isCriticalGem);

        for (int i = 0; i < gemList.Count; i++)
        {
            VirtueGemData _vg = VirtueGemManager.Instance.GetVirtueGemData(gemList[i]);
            rewardGemList[i].transform.Find("Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("VirtueGem/" + _vg.id.ToString("000"));
            rewardGemList[i].transform.Find("Name").GetComponent<Text>().text = _vg.name.GetString();
            rewardGemList[i].transform.Find("Text").GetComponent<Text>().text = _vg.description.GetString();
        }

        base.AddUI();
    }

    public void ChooseGem(int _index)
    {
        Database.AddGem(gemList[_index]);

        if (isCriticalGem)
        {
            MainGameView.Instance.CheckQuestAfterMainBattleQuest();
        }
        else
        {
            MainGameView.Instance.CheckQuestAfterBattleQuest();
        }

        MainGameView.Instance.playerStatusCanvas.Refresh();

        OnBackPressed();
    }
}
