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
                int.TryParse(_c[3], out _b.atk);
                int.TryParse(_c[4], out _b.ats);

                enemydata.Add(_b);
            }
        }
        else
        {
            Debug.Log("data is null");
        }
    }

}
