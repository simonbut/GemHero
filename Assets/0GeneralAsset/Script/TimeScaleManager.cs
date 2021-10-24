using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScaleManager : MonoBehaviour
{
    #region instance
    private static TimeScaleManager m_instance;

    public static TimeScaleManager Instance
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


    public void Init()
    {

    }

    public void Stop(float duration)
    {
    }

    public bool IsStop()
    {
        return false;
    }

    public void Pause()
    {
    }


    // Update is called once per frame
    void Update()
    {

    }

    public void ResumeAll()
    {

    }
}
