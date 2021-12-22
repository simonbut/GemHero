using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ClassHelper;

public class QuestCanvas : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ShowQuest();
    }

    public GameObject mainQuestObject;
    public List<GameObject> sideQuestObject;
    void ShowQuest()
    {
        if (Database.userDataJson.mainQuest.questId > 0)
        {
            mainQuestObject.SetActive(true);
            QuestData _q = QuestManager.Instance.GetQuestData(Database.userDataJson.mainQuest.questId);
            mainQuestObject.transform.Find("Quest").Find("Text").GetComponent<Text>().text = _q.description.GetString();
            int remainTime = _q.timeLimit * 60 - (Database.userDataJson.time - Database.userDataJson.mainQuest.startTime);
            mainQuestObject.transform.Find("Quest").Find("TimeLine").Find("Text").GetComponent<Text>().text = Database.TimeToString(remainTime);
            mainQuestObject.transform.Find("Quest").Find("TimeLine").Find("Bar").GetComponent<Image>().fillAmount = remainTime * 1f / (_q.timeLimit * 60);
        }
        else
        {
            mainQuestObject.SetActive(false);
        }
        for (int i = 0; i < 3; i++)
        {
            sideQuestObject[i].SetActive(false);
        }
        for (int i = 0; i < Database.userDataJson.sideQuest.Count; i++)
        {
            sideQuestObject[i].SetActive(true);
            QuestData _q = QuestManager.Instance.GetQuestData(Database.userDataJson.sideQuest[i].questId);
            sideQuestObject[i].transform.Find("Quest").Find("Text").GetComponent<Text>().text = "Side Mission" + (_q.id % 100).ToString("0") + ": " + _q.description.GetString();
            int remainTime = _q.timeLimit * 60 - (Database.userDataJson.time - Database.userDataJson.sideQuest[i].startTime);
            sideQuestObject[i].transform.Find("Quest").Find("TimeLine").Find("Text").GetComponent<Text>().text = Database.TimeToString(remainTime);
            sideQuestObject[i].transform.Find("Quest").Find("TimeLine").Find("Bar").GetComponent<Image>().fillAmount = remainTime * 1f / (_q.timeLimit * 60);
        }
    }
}
