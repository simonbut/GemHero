using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClassHelper;

public class QuestManager : MonoBehaviour
{
    #region instancetag
    private static QuestManager m_instance;

    public static QuestManager Instance
    {
        get
        {
            return m_instance;
        }
    }

    void Awake()
    {
        if (QuestManager.Instance == null)
        {
            m_instance = this;
        }
    }
    #endregion

    [HideInInspector]
    public List<QuestData> questData = new List<QuestData>();
    public List<QuestData> GetQuestDataFullList()
    {
        return questData;
    }

    public void LoadQuestData()
    {
        questData = new List<QuestData>();
        string data = Database.ReadDatabaseWithoutLanguage("Quest");
        if (data.Length > 0)
        {
            string[] _a = data.Split('\n');
            for (int i = 1; i < _a.Length; i++)
            {
                QuestData _b = new QuestData();
                string[] _c = _a[i].Split('\t');
                int.TryParse(_c[0], out _b.id);
                _b.description = new LocalizedString(_c[1], _c[1], _c[1], "");
                int.TryParse(_c[2], out _b.timeLimit);
                QuestType.TryParse(_c[3], out _b.questType);
                int.TryParse(_c[4], out _b.characterId);
                int.TryParse(_c[5], out _b.itemId);
                int.TryParse(_c[6], out _b.itemQuality);
                int.TryParse(_c[7], out _b.itemTag);

                _b.enemyList = new List<int>();
                string[] _c8 = _c[8].Split(';');
                for (int j = 0; j < _c8.Length; j++)
                {
                    int _c8b;
                    int.TryParse(_c8[j], out _c8b);

                    _b.enemyList.Add(_c8b);
                }

                int.TryParse(_c[9], out _b.targetRelease);
                
                questData.Add(_b);
            }
        }
        else
        {
            Debug.Log("data is null");
        }
    }

    public QuestData GetQuestData(int _questId)
    {
        foreach (QuestData _qd in questData)
        {
            if (_qd.id == _questId)
            {
                return _qd;
            }
        }
        return null;
    }
}
