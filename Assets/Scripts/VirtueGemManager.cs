using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClassHelper;
using System;
using System.Linq;
using System.Security.Cryptography;

public class VirtueGemManager : MonoBehaviour
{
    #region instancetag
    private static VirtueGemManager m_instance;

    public static VirtueGemManager Instance
    {
        get
        {
            return m_instance;
        }
    }

    void Awake()
    {
        if (VirtueGemManager.Instance == null)
        {
            m_instance = this;
        }
    }
    #endregion

    [HideInInspector]
    public List<VirtueGemData> virtueGemData = new List<VirtueGemData>();
    public List<VirtueGemData> GetVirtueGemDataFullList()
    {
        return virtueGemData;
    }

    public void LoadVirtueGemData()
    {
        virtueGemData = new List<VirtueGemData>();
        string data = Database.ReadDatabaseWithoutLanguage("VirtueGem");
        if (data.Length > 0)
        {
            string[] _a = data.Split('\n');
            for (int i = 1; i < _a.Length; i++)
            {
                VirtueGemData _b = new VirtueGemData();
                string[] _c = _a[i].Split('\t');
                int.TryParse(_c[0], out _b.id);
                _b.name = new LocalizedString(_c[1], _c[1], _c[1], "");
                _b.description = new LocalizedString(_c[2], _c[2], _c[2], "");

                _b.appearStage = new List<int>();
                string[] _c3 = _c[3].Split(';');
                for (int j = 0; j < _c3.Length; j++)
                {
                    int _c3b;
                    int.TryParse(_c3[j], out _c3b);

                    if (_c3b > 0)
                    {
                        _b.appearStage.Add(_c3b);
                    }
                }

                int.TryParse(_c[4], out _b.RequireAchievementsCount);
                bool.TryParse(_c[5], out _b.IsCriticalGem);

                virtueGemData.Add(_b);
            }
        }
        else
        {
            Debug.Log("data is null");
        }
    }

    public VirtueGemData GetVirtueGemData(int _chId)
    {
        foreach (VirtueGemData _cd in virtueGemData)
        {
            if (_cd.id == _chId)
            {
                return _cd;
            }
        }
        return null;
    }

    public List<int> GenerateRandomRewardGemList(bool _isCri)
    {
        List<int> result = new List<int>();

        System.Random rnd = new System.Random();
        List<VirtueGemData> randomList = virtueGemData.OrderBy(x => rnd.Next()).ToList();

        foreach (VirtueGemData _vg in randomList)
        {
            if (_isCri == _vg.IsCriticalGem)
            {
                if (!Database.userDataJson.virtueGem.Contains(_vg.id) && _vg.appearStage.Contains(Database.userDataJson.chapter))
                {
                    result.Add(_vg.id);
                }
            }

            if (result.Count >= 3)
            {
                return result;
            }
        }

        return result;
    }
}
