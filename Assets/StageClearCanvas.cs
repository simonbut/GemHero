using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageClearCanvas : ControlableUI
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void BackToMenu()
    {
        MainGameView.Instance.Quit();
    }
}
