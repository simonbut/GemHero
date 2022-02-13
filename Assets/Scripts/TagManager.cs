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
    public List<TagData> tagData = new List<TagData>();
    public List<TagData> GetTagDataFullList()
    {
        return tagData;
    }

    public void LoadTagData()
    {
        tagData = new List<TagData>();
        string data = Database.ReadDatabaseWithoutLanguage("Tag");
        if (data.Length > 0)
        {
            string[] _a = data.Split('\n');
            for (int i = 1; i < _a.Length; i++)
            {
                TagData _b = new TagData();
                string[] _c = _a[i].Split('\t');
                int.TryParse(_c[0], out _b.id);
                _b.name = new LocalizedString(_c[1], _c[1], _c[1], "");
                _b.description = new LocalizedString(_c[2], _c[2], _c[2], "");

                _b.grids = new List<Vector2Int>();
                string[] _c3 = _c[3].Split(';');
                for (int j = 0; j < _c3.Length; j++)
                {
                    string[] _c3b = _c3[j].Split(',');
                    for (int k = 0; k < _c3b.Length; k++)
                    {
                        int x3 = 0;
                        int.TryParse(_c3b[k], out x3);

                        _b.grids.Add(new Vector2Int(x3, Mathf.FloorToInt(_c3.Length / 2) - j));
                    }
                }

                int.TryParse(_c[4], out _b.score);
                int.TryParse(_c[5], out _b.minValue);
                int.TryParse(_c[6], out _b.maxValue);

                bool.TryParse(_c[7], out _b.isBadTag);
                TagType.TryParse(_c[8], out _b.tagType);

                tagData.Add(_b);
            }
        }
        else
        {
            Debug.Log("data is null");
        }
    }

    [HideInInspector]
    public List<DestinyShareData> destinyShareData = new List<DestinyShareData>();
    public List<DestinyShareData> GetDestinyShareDataFullList()
    {
        return destinyShareData;
    }

    public void LoadDestinyShareData()
    {
        destinyShareData = new List<DestinyShareData>();
        string data = Database.ReadDatabaseWithoutLanguage("DestinyShare");
        if (data.Length > 0)
        {
            string[] _a = data.Split('\n');
            for (int i = 1; i < _a.Length; i++)
            {
                DestinyShareData _b = new DestinyShareData();
                string[] _c = _a[i].Split('\t');
                int.TryParse(_c[0], out _b.id);
                int.TryParse(_c[1], out _b.characterId);
                int.TryParse(_c[2], out _b.tagId);

                destinyShareData.Add(_b);
            }
        }
        else
        {
            Debug.Log("data is null");
        }
    }


    public TagData GetTagData(int tagId)
    {
        foreach (TagData _td in tagData)
        {
            if (_td.id == tagId)
            {
                return _td;
            }
        }
        return new TagData();
    }

    public List<DestinyShareData> GetDestinyShareDataByCharacterId(int _characterId)
    {
        List<DestinyShareData> result = new List<DestinyShareData>();
        foreach (DestinyShareData _dsd in destinyShareData)
        {
            if (_dsd.characterId == _characterId)
            {
                result.Add(_dsd);
            }
        }
        return result;
    }

}
