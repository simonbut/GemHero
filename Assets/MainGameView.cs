using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ClassHelper;

public class MainGameView : MonoBehaviour
{
    #region instance
    private static MainGameView m_instance;

    public static MainGameView Instance
    {
        get
        {
            return m_instance;
        }
    }

    void Awake()
    {
        if (MainGameView.Instance == null)
        {
            m_instance = this;
        }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        CompositeView.Instance.StartComposite();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
