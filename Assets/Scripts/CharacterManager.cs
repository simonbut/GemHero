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
