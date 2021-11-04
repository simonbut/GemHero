using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlableUIGeneral : ControlableUI
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (JoyStickManager.Instance.IsInputDown("Cross") || Input.GetButtonDown("Escape"))
        //{
        //    if (UIManager.Instance.IsCurrentUI(this))
        //    {
        //        OnBackPressed();
        //    }
        //}
    }

    public override void OnShow()
    {
        base.OnShow();
    }

    public override void OnRemoveUI()
    {
        base.OnRemoveUI();
    }
}
