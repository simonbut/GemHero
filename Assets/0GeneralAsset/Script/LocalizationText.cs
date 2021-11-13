using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ClassHelper;

public class LocalizationText : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        LocalizeText();
        ResizeText();
    }

    void Start()
    {
        //LocalizeText();
    }
    string key = "";

    public void ResizeText()
    {
        if (Database.globalData == null)
        {
            return;
        }

        if (GetComponent<Text>() != null)
        {
            switch (Database.globalData.language)
            {
                case Language.en:
                    //GetComponent<Text>().fontSize = Mathf.FloorToInt(GetComponent<Text>().fontSize * 1.2f);
                    break;
                case Language.zh:
                    //GetComponent<Text>().fontSize = Mathf.FloorToInt(GetComponent<Text>().fontSize * 1.5f);
                    break;
                case Language.jp:
                    //GetComponent<Text>().fontSize = Mathf.FloorToInt(GetComponent<Text>().fontSize * 1.5f);
                    break;
                case Language.cn:
                    //GetComponent<Text>().fontSize = Mathf.FloorToInt(GetComponent<Text>().fontSize * 1.5f);
                    break;
            }
        }
        if (GetComponent<TextMesh>() != null)
        {
            switch (Database.globalData.language)
            {
                case Language.en:
                    break;
                case Language.zh:
                    //GetComponent<TextMesh>().fontSize = Mathf.FloorToInt(GetComponent<TextMesh>().fontSize * 1.5f);
                    break;
                case Language.jp:
                    //GetComponent<TextMesh>().fontSize = Mathf.FloorToInt(GetComponent<TextMesh>().fontSize * 1.5f);
                    break;
                case Language.cn:
                    //GetComponent<TextMesh>().fontSize = Mathf.FloorToInt(GetComponent<TextMesh>().fontSize * 1.5f);
                    break;
            }
        }
        //if (GetComponent<TextMeshProUGUI>() != null)
        //{
        //    switch (Database.globalData.language)
        //    {
        //        case Language.en:
        //            GetComponent<TextMeshProUGUI>().fontSize = GetComponent<TextMeshProUGUI>().fontSize * 1.15f;
        //            break;
        //        case Language.zh:
        //            GetComponent<TextMeshProUGUI>().fontSize = GetComponent<TextMeshProUGUI>().fontSize * 1.35f;
        //            break;
        //        case Language.jp:
        //            GetComponent<TextMeshProUGUI>().fontSize = GetComponent<TextMeshProUGUI>().fontSize * 1.35f;
        //            break;
        //        case Language.cn:
        //            GetComponent<TextMeshProUGUI>().fontSize = GetComponent<TextMeshProUGUI>().fontSize * 1.35f;
        //            break;
        //    }
        //}
    }

    public void LocalizeText()
    {
        if (Database.globalData == null)
        {
            return;
        }
        if (GetComponent<Text>() != null)
        {
            key = GetComponent<Text>().text;
        }
        if (GetComponent<TextMesh>() != null)
        {
            key = GetComponent<TextMesh>().text;
        }
        //if (GetComponent<TextMeshProUGUI>() != null)
        //{
        //    key = GetComponent<TextMeshProUGUI>().text;
        //}

        if (GetComponent<Text>() != null)
        {
            string s = Database.GetLocalizedText(key);
            if (s != "")
            {
                while (s.Contains("「") && s.Contains("」"))
                {
                    s = s.Replace("「", "<b>");
                    s = s.Replace("」", "</b>");
                }
                s = s.Replace("\\n", "\n");
                GetComponent<Text>().text = s;
            }
            switch (Database.globalData.language)
            {
                case Language.en:
                    GetComponent<Text>().font = SystemManager.Instance.enFont;
                    break;
                case Language.zh:
                    GetComponent<Text>().font = SystemManager.Instance.tradFont;
                    break;
                case Language.jp:
                    break;
                case Language.cn:
                    GetComponent<Text>().font = SystemManager.Instance.simpFont;
                    break;
            }
        }
        if (GetComponent<TextMesh>() != null)
        {
            string s = Database.GetLocalizedText(key);
            if (s != "")
            {
                while (s.Contains("「") && s.Contains("」"))
                {
                    s = s.Replace("「", "<b> ");
                    s = s.Replace("」", " </b>");
                }
                s = s.Replace("，", "， ");
                s = s.Replace("、", "、 ");
                s = s.Replace("。", "。 ");
                s = s.Replace("\\n", "\n");
                GetComponent<TextMesh>().text = s;
            }
            switch (Database.globalData.language)
            {
                case Language.en:
                    GetComponent<TextMesh>().font = SystemManager.Instance.enFont;
                    break;
                case Language.zh:
                    GetComponent<TextMesh>().font = SystemManager.Instance.tradFont;
                    break;
                case Language.jp:
                    break;
                case Language.cn:
                    GetComponent<TextMesh>().font = SystemManager.Instance.simpFont;
                    break;
            }
        }
        //if (GetComponent<TextMeshProUGUI>() != null)
        //{
        //    string s = Database.GetLocalizedText(key);
        //    if (s != "")
        //    {
        //        while (s.Contains("「") && s.Contains("」"))
        //        {
        //            s = s.Replace("「", "<b> ");
        //            s = s.Replace("」", " </b>");
        //            //s = s.Replace("「", "<b><i><u> ");
        //            //s = s.Replace("」", " </u></i></b>");
        //        }
        //        s = s.Replace("，", "， ");
        //        s = s.Replace("、", "、 ");
        //        s = s.Replace("。", "。 ");
        //        s = s.Replace("\\n", "\n");
        //        GetComponent<TextMeshProUGUI>().text = s;
        //    }
        //    switch (Database.globalData.language)
        //    {
        //        case Language.en:
        //            GetComponent<TextMeshProUGUI>().font = SystemManager.Instance.enFontPro;
        //            break;
        //        case Language.zh:
        //            GetComponent<TextMeshProUGUI>().font = SystemManager.Instance.tradFontPro;
        //            break;
        //        case Language.jp:
        //            break;
        //        case Language.cn:
        //            GetComponent<TextMeshProUGUI>().font = SystemManager.Instance.simpFontPro;
        //            break;
        //    }
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
