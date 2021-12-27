using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ClassHelper;

public class InformationUI : DataUI
{
    public Text text;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void Show(string _text)
    {
        OnShow();
        text.text = Database.GetLocalizedText(_text);
    }
}
