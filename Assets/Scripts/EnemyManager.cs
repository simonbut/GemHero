using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClassHelper;

public class EnemyManager : MonoBehaviour
{
    #region instancetag
    private static EnemyManager m_instance;

    public static EnemyManager Instance
    {
        get
        {
            return m_instance;
        }
    }

    void Awake()
    {
        if (EnemyManager.Instance == null)
        {
            m_instance = this;
        }
    }
    #endregion

    [HideInInspector]
    public List<EnemyData> enemydata = new List<EnemyData>();
    public List<EnemyData> GetEnemyDataFullList()
    {
        return enemydata;
    }

    public void LoadEnemyData()
    {
        enemydata = new List<EnemyData>();
        string data = Database.ReadDatabaseWithoutLanguage("Enemy");
        if (data.Length > 0)
        {
            string[] _a = data.Split('\n');
            for (int i = 1; i < _a.Length; i++)
            {
                EnemyData _b = new EnemyData();
                string[] _c = _a[i].Split('\t');
                int.TryParse(_c[0], out _b.id);

                _b.name = new LocalizedString(_c[1], _c[1], _c[1], "");

                int.TryParse(_c[2], out _b.hp);
                int.TryParse(_c[3], out _b.def);
                int.TryParse(_c[4], out _b.atk);
                int.TryParse(_c[5], out _b.ats);

                _b.skillList = new List<int>();
                string[] _c6 = _c[6].Split(';');
                for (int j = 0; j < _c6.Length; j++)
                {
                    int _c6b;
                    int.TryParse(_c6[j], out _c6b);

                    _b.skillList.Add(_c6b);
                }

                enemydata.Add(_b);
            }
        }
        else
        {
            Debug.Log("data is null");
        }
    }


    [HideInInspector]
    public List<EnemySkillData> enemySkilldata = new List<EnemySkillData>();
    public List<EnemySkillData> GetEnemySkillDataFullList()
    {
        return enemySkilldata;
    }

    public void LoadEnemySkillData()
    {
        enemySkilldata = new List<EnemySkillData>();
        string data = Database.ReadDatabaseWithoutLanguage("Enemy");
        if (data.Length > 0)
        {
            string[] _a = data.Split('\n');
            for (int i = 1; i < _a.Length; i++)
            {
                EnemySkillData _b = new EnemySkillData();
                string[] _c = _a[i].Split('\t');
                int.TryParse(_c[0], out _b.id);

                _b.description = new LocalizedString(_c[1], _c[1], _c[1], "");

                enemySkilldata.Add(_b);
            }
        }
        else
        {
            Debug.Log("data is null");
        }
    }
}
