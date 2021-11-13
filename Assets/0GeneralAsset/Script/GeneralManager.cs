using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralManager : MonoBehaviour
{
    void Awake()
    {
        if (Database.isInit)
        {
            gameObject.SetActive(false);
        }
    }

    private void Start()
    {
        if (!Database.isInit)
        {
            Database.Init();
        }
    }
}
