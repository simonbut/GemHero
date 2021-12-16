using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClassHelper;

public class DialogManager : MonoBehaviour
{
    #region instancetag
    private static DialogManager m_instance;

    public static DialogManager Instance
    {
        get
        {
            return m_instance;
        }
    }

    void Awake()
    {
        if (DialogManager.Instance == null)
        {
            m_instance = this;
        }
    }
    #endregion

    [HideInInspector]
    public List<DialogData> dialogData = new List<DialogData>();
    public List<DialogData> GetDialogDataFullList()
    {
        return dialogData;
    }

    public void LoadDialogData()
    {
        dialogData = new List<DialogData>();
        string data = Database.ReadDatabaseWithoutLanguage("Dialog");
        if (data.Length > 0)
        {
            string[] _a = data.Split('\n');
            for (int i = 1; i < _a.Length; i++)
            {
                DialogData _b = new DialogData();
                string[] _c = _a[i].Split('\t');
                int.TryParse(_c[0], out _b.id);
                int.TryParse(_c[1], out _b.dialogId);
                int.TryParse(_c[2], out _b.step);
                int.TryParse(_c[3], out _b.characterId);
                
                _b.content = new LocalizedString(_c[4], _c[4], _c[4], "");

                dialogData.Add(_b);
            }
        }
        else
        {
            Debug.Log("data is null");
        }
    }

    public DialogData FindDialogData(int _dialogId,int _step)
    {
        foreach (DialogData _dd in dialogData)
        {
            if (_dd.dialogId == _dialogId)
            {
                if (_dd.step == _step)
                {
                    return _dd;
                }
            }
        }
        return null;
    }
}
