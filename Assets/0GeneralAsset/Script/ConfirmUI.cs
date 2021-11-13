using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ClassHelper;

public class ConfirmUI : ControlableUI
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public delegate void SimpleCallBack();
    SimpleCallBack yesCallBack;
    SimpleCallBack noCallBack;
    public Text text;

    public Text yesText;
    public Text noText;

    public void AddUI(string _text, SimpleCallBack _yesCallBack, SimpleCallBack _noCallBack = null)
    {
        //print("Confirm Add UI");
        text.text = Database.GetLocalizedText(_text);
        yesCallBack = _yesCallBack;
        noCallBack = _noCallBack;
        yesText.text = Database.GetLocalizedText("Yes");
        noText.text = Database.GetLocalizedText("No");

        AddUI();
    }

    // Update is called once per frame
    void Update()
    {
        //if (JoyStickManager.Instance.IsInputDown("Cross"))
        //{
        //    if (UIManager.Instance.IsCurrentUI(this))
        //    {
        //        No();
        //    }
        //}

        //if (JoyStickManager.Instance.IsJoyStickEnable())
        //{
        //    if (JoyStickManager.Instance.IsInputDown("Circle"))
        //    {
        //        if (UIManager.Instance.IsCurrentUI(this))
        //        {
        //            Yes();
        //        }
        //    }
        //}
    }


    public void Yes()
    {
        if (UIManager.Instance.IsCurrentUI(this))
        {
            yesCallBack?.Invoke();
            OnBackPressed();
        }
    }

    public void No()
    {
        if (UIManager.Instance.IsCurrentUI(this))
        {
            noCallBack?.Invoke();
            OnBackPressed();
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
