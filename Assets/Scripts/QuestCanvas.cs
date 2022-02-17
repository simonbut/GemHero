using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ClassHelper;

public class QuestCanvas : MonoBehaviour
{
    float time;

    // Start is called before the first frame update
    void Start()
    {
        time = Database.userDataJson.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (time < Database.userDataJson.time)
        {
            time += Time.deltaTime * Mathf.Max(1,Database.userDataJson.time - time) * 10f;
        }
        if (time > Database.userDataJson.time)
        {
            time = Database.userDataJson.time;
        }
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
            mainQuestObject.transform.Find("Quest").Find("Text").GetComponent<Text>().text = _q.GetDescription();
            float remainTime = _q.timeLimit * 60 - (time - Database.userDataJson.mainQuest.startTime);
            mainQuestObject.transform.Find("Quest").Find("TimeLine").Find("Text").GetComponent<Text>().text = Database.TimeToString(remainTime);
            float fillAmount = remainTime * 1f / (_q.timeLimit * 60);
            mainQuestObject.transform.Find("Quest").Find("TimeLine").Find("Bar").GetComponent<Image>().fillAmount = fillAmount;
            mainQuestObject.transform.Find("Quest").Find("TimeLine").Find("Bar").GetComponent<Image>().color = new Color(1 - fillAmount, fillAmount, 0, 1);

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
            sideQuestObject[i].transform.Find("Quest").Find("Text").GetComponent<Text>().text = _q.GetDescription();
            float remainTime = _q.timeLimit * 60 - (time - Database.userDataJson.sideQuest[i].startTime);
            sideQuestObject[i].transform.Find("Quest").Find("TimeLine").Find("Text").GetComponent<Text>().text = Database.TimeToString(remainTime);
            float fillAmount = remainTime * 1f / (_q.timeLimit * 60);
            sideQuestObject[i].transform.Find("Quest").Find("TimeLine").Find("Bar").GetComponent<Image>().fillAmount = fillAmount;
            sideQuestObject[i].transform.Find("Quest").Find("TimeLine").Find("Bar").GetComponent<Image>().color = new Color(1 - fillAmount, fillAmount, 0, 1);
        }
    }
}
