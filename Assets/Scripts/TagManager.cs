using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClassHelper;

public class TagManager : MonoBehaviour
{
    #region instance
    private static TagManager m_instance;

    public static TagManager Instance
    {
        get
        {
            return m_instance;
        }
    }

    void Awake()
    {
        if (TagManager.Instance == null)
        {
            m_instance = this;
        }
    }
    #endregion

    [HideInInspector]
    public List<TagData> tagdata = new List<TagData>();
    public List<TagData> GetTagDataFullList()
    {
        return tagdata;
    }

    public void LoadTagData()
    {
        tagdata = new List<TagData>();
        //TextAsset data = Resources.Load("database/Database - " + globalData.language.ToString() + " - achievement") as TextAsset;
        string data = Database.ReadDatabaseWithoutLanguage("Tag");
        if (data.Length > 0)
        {
            string[] _a = data.Split('\n');
            for (int i = 1; i < _a.Length; i++)
            {
                TagData _b = new TagData();
                string[] _c = _a[i].Split('\t');
                int.TryParse(_c[0], out _b.id);
                int.TryParse(_c[1], out _b.groupId);
                int.TryParse(_c[2], out _b.subId);
                _b.name = new LocalizedString(_c[3], _c[3], _c[3], "");
                _b.description = new LocalizedString(_c[4], _c[4], _c[4], "");

                _b.grids = new List<Vector2Int>();
                string[] _c5 = _c[5].Split(';');
                for (int j = 0; j < _c5.Length; j++)
                {
                    int x5 = 0;
                    int y5 = 0;
                    string[] _c5b = _c5[j].Split(',');
                    int.TryParse(_c5b[0], out x5);
                    int.TryParse(_c5b[1], out y5);

                    _b.grids.Add(new Vector2Int(x5, y5));

                }

                tagdata.Add(_b);
            }
        }
        else
        {
            Debug.Log("data is null");
        }
    }



    public TagData GetTag(int tag_id)
    {
        foreach (TagData _t in tagdata)
        {
            if (_t.id == tag_id)
            {
                return _t;
            }
        }
        return new TagData();
    }

}