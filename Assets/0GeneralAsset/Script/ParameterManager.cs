using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParameterManager : MonoBehaviour
{
    #region instance
    private static ParameterManager m_instance;

    public static ParameterManager Instance
    {
        get
        {
            return m_instance;
        }
    }

    void Awake()
    {
        if (ParameterManager.Instance == null)
        {
            m_instance = this;
        }
    }
    #endregion

    Dictionary<string, float> parameterList;
    public void LoadParameterData()
    {
        parameterList = new Dictionary<string,float>();
        //TextAsset achievementData = Resources.Load("database/Database - " + globalData.language.ToString() + " - achievement") as TextAsset;
        string facilityData = Database.ReadDatabaseWithoutLanguage("Parameter");
        if (facilityData.Length > 0)
        {
            string[] _a = facilityData.Split('\n');
            for (int i = 1; i < _a.Length; i++)
            {
                string[] _c = _a[i].Split('\t');
                int _f = 0;
                int.TryParse(_c[1], out _f);
                parameterList.Add(_c[0], _f);
            }
        }
        else
        {
            Debug.Log("parameter Data is null");
        }
    }

    public float GetParameter(string key)
    {
        //if (ParameterManager.Instance == null)
        //{
        //    print("key " + key);
        //    GameObject objToSpawn = new GameObject("parameterManager");
        //    objToSpawn.AddComponent<ParameterManager>();
        //    //Instantiate(objToSpawn);
        //    ParameterManager.Instance.LoadParameterData();
        //}

        float result = 0;
        ParameterManager.Instance.parameterList.TryGetValue(key,out result);
        return result;
    }
}
