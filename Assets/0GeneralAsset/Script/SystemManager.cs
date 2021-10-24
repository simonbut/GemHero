using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using ClassHelper;
using TMPro;

public class SystemManager : MonoBehaviour
{
    #region instance
    private static SystemManager m_instance;

    public static SystemManager Instance
    {
        get
        {
            return m_instance;
        }
    }

    void Awake()
    {
        m_instance = this;
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        //LoadLocalizationTable();
        enFont.material.mainTexture.filterMode = FilterMode.Point;
        simpFont.material.mainTexture.filterMode = FilterMode.Point;
        enFont.material.mainTexture.filterMode = FilterMode.Point;
    }

    public Font tradFont;
    public Font simpFont;
    public Font enFont;

    public TMP_FontAsset tradFontPro;
    public TMP_FontAsset simpFontPro;
    public TMP_FontAsset enFontPro;

    public Vector2 screenMousePos;

    //public EventSystem eventSystem;
    //public GameObject fruit;
    public ExtendedStandaloneInputModule extendedStandaloneInputModule;
    // Update is called once per frame

    Dictionary<string, string> localizationTable=new Dictionary<string, string>();

    float timer = 0f;
    Button previousOverButton = null;

    public Material edgeMaterial;

    void Update()
    {
        //print("CR_running " + Database.CR_running);

        //print("isTimePassing " + Database.isTimePassing);
        //print("timePassing " + Database.timePassing);



        //Text[] texts = GameObject.FindObjectsOfType<Text>();
        //foreach (Text text in texts)
        //{
        //    if (text != null)
        //    {
        //        switch (Database.globalData.language)
        //        {
        //            case Language.zh:
        //            case Language.cn:
        //                text.font = tradFont;
        //                break;
        //            case Language.en:
        //            case Language.de:
        //                text.font = enFont;
        //                //text.font.material.mainTexture.filterMode = FilterMode.Point;
        //                break;
        //        }
        //    }
        //}

        PointerEventData pointerEventData = extendedStandaloneInputModule.GetPointerEventData();
        if (pointerEventData.hovered.Count>0)
        {
            foreach (GameObject obj in pointerEventData.hovered)
            {
                if (obj != null && obj.GetComponent<Button>() != null)
                {
                    if (previousOverButton != obj.GetComponent<Button>())
                    {
                        //AudioManager.instance.PlaySFX(AudioManager.instance.uI_Select);//PlayVoiceMouseOver
                    }
                    previousOverButton = obj.GetComponent<Button>();
                    break;
                }
            }
        }

        if (EventSystem.current.IsPointerOverGameObject() && JoyStickManager.Instance.IsInputDown("Circle"))
        {
            //PointerEventData pointerEventData = extendedStandaloneInputModule.GetPointerEventData();
            //print("over something" + test.hovered.Count);
            //foreach (GameObject obj in pointerEventData.hovered)
            //{
            //    Debug.Log(obj.name);
            //}

            //if (eventSystem.currentSelectedGameObject.GetComponent<Collider>().tag.Equals("Minimap"))//eventSystem.gameObject.GetComponent<Collider>().tag.Equals("Minimap"))
            //{
            //    Debug.Log("To minimap");
            //}
            //inventory, stats or minimap

            screenMousePos = pointerEventData.position;

            if (pointerEventData.hovered.Count > 0)
            {
                bool playClickAudio = false;
                bool playDisableAudio = false;
                foreach (GameObject obj in pointerEventData.hovered)
                {
                    if (obj.GetComponent<Button>() != null && obj.GetComponent<Button>().enabled)
                    {
                        if (obj.GetComponent<Button>().interactable)
                        {
                            playClickAudio = true;
                            if (obj.GetComponent<ListItem>()!=null && !obj.GetComponent<ListItem>().isAllowInteract)
                            {
                                playClickAudio = false;
                                playDisableAudio = true;
                            }
                        }
                        else
                        {
                            playDisableAudio = true;
                        }
                    }
                }
                if (playClickAudio)
                {
                    print("play click audio");
                    //AudioManager.instance.PlaySFX(AudioManager.instance.uI_Signle_Click);//click
                }
                if (playDisableAudio)
                {
                    //AudioManager.instance.PlaySFX(AudioManager.instance.uI_Error);//click failed
                }
            }
        }


    }

    //void LoadLocalizationTable()
    //{
    //    //TextAsset localizationData = Resources.Load("database/localization") as TextAsset;
    //    string localizationData = System.IO.File.ReadAllText(UnityEngine.Application.persistentDataPath + "/localization.csv");
    //    if (localizationData != null)
    //    {
    //        string[] _a = localizationData.Split('\n');
    //        for (int i = 1; i < _a.Length; i++)
    //        {
    //            //Card _b = new Card();
    //            string[] _c = _a[i].Split(',');
    //            //int.TryParse(_c[0], out _b.card_id);
    //            if (_c.Length>1)
    //            {
    //                localizationTable.Add(_c[0], _c[1]);
    //            }
    //            //card.Add(_b);

    //        }
    //    }
    //}

    string DictionaryToCsv(Dictionary<string, string> _dictionary)
    {
        string result = "";
        foreach (KeyValuePair<string, string> e in _dictionary)
        {
            result += e.Key;
            result += ",";
            result += e.Value;
            result += "\n";
        }
        return result;
    }

    //void WriteLocalizationTable()
    //{
    //    System.IO.File.WriteAllText(UnityEngine.Application.persistentDataPath + "/localization.csv", DictionaryToCsv(localizationTable));
    //}

}