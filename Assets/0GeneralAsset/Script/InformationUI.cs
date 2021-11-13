using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ClassHelper;

public class InformationUI : ControlableUI
{
    public Text confirmText;
    // Start is called before the first frame update
    void Start()
    {

    }

    public delegate void SimpleCallBack();
    SimpleCallBack yesCallBack;
    public Text text;
    public void AddUI(string _text, SimpleCallBack _yesCallBack = null)
    {
        print("Confirm Add UI");
        text.text = Database.GetLocalizedText(_text);
        yesCallBack = _yesCallBack;
        //if (JoyStickManager.Instance.IsJoyStickEnable())
        //{
        //    confirmText.text = Database.GetInputIcon(KeyBoardInput.joystick_cross) + " " + Database.GetLocalizedText("Confirm");
        //}
        //else
        //{
        //    confirmText.text = Database.GetLocalizedText("Confirm");
        //}

        AddUI();
    }

    // Update is called once per frame
    void Update()
    {
        //if (JoyStickManager.Instance.IsInputDown("Cross"))
        //{
        //    if (UIManager.Instance.IsCurrentUI(this))
        //    {
        //        Yes();
        //    }
        //}
        //if (JoyStickManager.Instance.IsJoyStickEnable() && JoyStickManager.Instance.IsInputDown("Circle"))
        //{
        //    if (UIManager.Instance.IsCurrentUI(this))
        //    {
        //        Yes();
        //    }
        //}
    }


    public void Yes()
    {
        if (UIManager.Instance.IsCurrentUI(this))
        {
            OnBackPressed();
            yesCallBack?.Invoke();
        }
    }

    public override void OnBackPressed()
    {
        if (UIManager.Instance.IsCurrentUI(this))
        {
            OnRemoveUI();
            UIManager.Instance.StopAnimation();
        }
    }
}
