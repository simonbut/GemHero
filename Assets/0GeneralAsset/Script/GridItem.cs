using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ClassHelper;

public class GridItem : MonoBehaviour
{
    GridItemCallback clickCallBack;
    GridItemCallback selectingCallBack;
    GridItemCallback disSelectingCallBack;
    GridItemCallback dragCallBack;
    public Image displayGraphic;
    public Image displayGraphicBg;
    public Image displayGraphicFront;
    public Image gray;
    public Text text;
    public Sprite empty;
    public int id;
    public bool followMouse = false;
    int selectingId;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    public void Update()
    {
        if (followMouse)
        {
            transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - new Vector2(0, GetComponent<RectTransform>().sizeDelta.y / 90f * Screen.height / 2f + 10f);
        }
        //if (Input.GetMouseButtonUp(0))
        //{
        //    followMouse = false;
        //}
    }

    public void SetGridItem(string graphPath, int _id, int _selectingId, GridItemCallback _clickCallBack, GridItemCallback _selectingCallBack, GridItemCallback _disSelectingCallBack, string graphPath2 = "",string _text = "",bool isEnabled = true, GridItemCallback _dragCallBack =null, string graphPath3 = "")
    {
        print(graphPath);
        id = _id;
        selectingId = _selectingId;
        //_g.AddComponent<GridItem>();
        //_g.displayGraphic = _g.transform.Find("Image").GetComponent<Image>();
        if (graphPath != "")
        {
            displayGraphic.sprite = Resources.Load<Sprite>(graphPath);
        }
        else
        {
            displayGraphic.sprite = empty;
        }
        if (graphPath2 != "")
        {
            displayGraphicBg.sprite = Resources.Load<Sprite>(graphPath2);
        }
        else
        {
            displayGraphicBg.sprite = empty;
        }
        if (graphPath3 != "")
        {
            displayGraphicFront.sprite = Resources.Load<Sprite>(graphPath3);
        }
        else
        {
            displayGraphicFront.sprite = empty;
        }
        text.text = _text;
        clickCallBack = _clickCallBack;
        selectingCallBack = _selectingCallBack;
        disSelectingCallBack = _disSelectingCallBack;
        if (_dragCallBack != null)
        {
            dragCallBack = _dragCallBack;
        }
        if (!isEnabled)
        {
            GetComponent<Button>().enabled = false;
            gray.enabled = true;
        }

        //transform.localScale = new Vector2(transform.localScale.x / 1600f * Screen.width, transform.localScale.x / 1600f * Screen.width);
    }

    public void Clicked()
    {
        clickCallBack?.Invoke(id, this);
    }

    public void Selecting()
    {
        //print("Selecting");

        GlobalCommunicateManager.selectingId = selectingId;
        selectingCallBack?.Invoke(id, this);
    }

    public void DisSelecting()
    {
        disSelectingCallBack?.Invoke(id, this);
    }

    public void Drag()
    {
        //if (dragCallBack != null)
        //{
        //    dragCallBack(id,this);
        //    followMouse = true;
        //}
        dragCallBack?.Invoke(id, this);
    }

    //public void DisAbleClicked()
    //{
    //    bg.color = new Color(0.5f, 0.5f, 0.5f, 1f);
    //    displayName.color = new Color(0.5f, 0.5f, 0.5f, 1f);
    //    text2.color = new Color(0.5f, 0.5f, 0.5f, 1f);
    //    isAllowInteract = false;
    //}
}
