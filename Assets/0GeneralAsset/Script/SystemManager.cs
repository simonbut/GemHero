using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using ClassHelper;

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


    public Font tradFont;
    public Font simpFont;
    public Font enFont;

    // Start is called before the first frame update
    void Start()
    {
        //LoadLocalizationTable();
        enFont.material.mainTexture.filterMode = FilterMode.Point;
        simpFont.material.mainTexture.filterMode = FilterMode.Point;
        enFont.material.mainTexture.filterMode = FilterMode.Point;
    }

    public Vector2 screenMousePos;

}