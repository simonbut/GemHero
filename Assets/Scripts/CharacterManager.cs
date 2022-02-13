using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClassHelper;

public class CharacterManager : MonoBehaviour
{
    #region instancetag
    private static CharacterManager m_instance;

    public static CharacterManager Instance
    {
        get
        {
            return m_instance;
        }
    }

    void Awake()
    {
        if (CharacterManager.Instance == null)
        {
            m_instance = this;
        }
    }
    #endregion

    [HideInInspector]
    public List<CharacterData> characterData = new List<CharacterData>();
    public List<CharacterData> GetCharacterDataFullList()
    {
        return characterData;
    }

    public void LoadCharacterData()
    {
        characterData = new List<CharacterData>();
        string data = Database.ReadDatabaseWithoutLanguage("Character");
        if (data.Length > 0)
        {
            string[] _a = data.Split('\n');
            for (int i = 1; i < _a.Length; i++)
            {
                CharacterData _b = new CharacterData();
                string[] _c = _a[i].Split('\t');
                int.TryParse(_c[0], out _b.id);
                _b.name = new LocalizedString(_c[1], _c[1], _c[1], "");

                //_b.tagList = new List<int>();
                //string[] _c2 = _c[2].Split(';');
                //for (int j = 0; j < _c2.Length; j++)
                //{
                //    int _c2b;
                //    int.TryParse(_c2[j], out _c2b);

                //    _b.tagList.Add(_c2b);
                //}

                //_b.tagPos = new List<Vector2Int>();
                //string[] _c3 = _c[3].Split(';');
                //for (int j = 0; j < _c3.Length; j++)
                //{
                //    int x3 = 0;
                //    int y3 = 0;
                //    string[] _c6b = _c3[j].Split(',');
                //    int.TryParse(_c6b[0], out x3);
                //    int.TryParse(_c6b[1], out y3);

                //    _b.tagPos.Add(new Vector2Int(x3, y3));

                //}

                characterData.Add(_b);
            }
        }
        else
        {
            Debug.Log("data is null");
        }
    }

    public CharacterData GetCharacterData(int _chId)
    {
        foreach (CharacterData _cd in characterData)
        {
            if (_cd.id == _chId)
            {
                return _cd;
            }
        }
        return null;
    }
}
