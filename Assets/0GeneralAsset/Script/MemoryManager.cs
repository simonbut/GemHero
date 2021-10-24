using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClassHelper;

public class MemoryManager : MonoBehaviour
{
    #region instance
    private static MemoryManager m_instance;

    public static MemoryManager Instance
    {
        get
        {
            return m_instance;
        }
    }

    void Awake()
    {
        if (MemoryManager.Instance == null)
        {
            m_instance = this;
        }
    }
    #endregion

    [HideInInspector]
    private static List<Memory> memory = new List<Memory>();

    public List<Memory> GetAllMemory()
    {
        return memory;
    }

    public Memory GetMemory(int _stage,bool _isBefore)
    {
        //Memory result = new Memory();
        foreach (Memory _m in GetAllMemory())
        {
            if ((_m.stage == _stage) && (_m.isBefore == _isBefore))
            {
                return _m;
            }
        }
        return new Memory();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadMemoryData()
    {
        memory = new List<Memory>();
        //TextAsset data = Resources.Load("database/Database - " + globalData.language.ToString() + " - memory") as TextAsset;
        string data = Database.ReadDatabaseWithoutLanguage("memory");
        if (data.Length > 0)
        {
            string[] _a = data.Split('\n');
            for (int i = 1; i < _a.Length; i++)
            {
                Memory _b = new Memory();
                string[] _c = _a[i].Split('\t');
                int.TryParse(_c[0], out _b.memory_id);
                _b.oice_uuid = _c[1];
                int.TryParse(_c[2], out _b.stage);
                bool.TryParse(_c[3], out _b.isBefore);
                memory.Add(_b);

            }
        }
    }

    public Memory GetMemoryByUuid(string uuid)
    {
        Memory result = new Memory();
        foreach (Memory _a in memory)
        {
            if (_a.oice_uuid == uuid)
            {
                result = _a;
                return result;
            }
        }
        return result;
    }

}
