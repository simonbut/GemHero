using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlMeaningUI : HintUI
{
    #region instance
    private static ControlMeaningUI m_instance;

    public static ControlMeaningUI Instance
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
}
