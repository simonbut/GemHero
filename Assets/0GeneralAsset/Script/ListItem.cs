using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using ClassHelper;

public class ListItem : MonoBehaviour
{
    public ListItemCallback clickCallBack;
    public ListItemCallback selectingCallBack;
    public ListItemCallback disSelectingCallBack;
    public Text displayName;
    public int id;
    int selectingId = 0;

    public Image selectingImage;
    public float selectingTimer;
    public bool isSelecting;

    public Image bg;
    public bool isAllowInteract = true;

    public int type = 1; //1 = vertical, 2 = horizontal

    // Start is called before the first frame update
    void Start()
    {

    }

    //public void OnSelect(BaseEventData eventData)
    //{
    //    // Do something.
    //    isSelecting = true;
    //}

    //public void on

    // Update is called once per frame
    void Update()
    {
        //print("EventSystem.current.currentSelectedGameObject " + EventSystem.current.currentSelectedGameObject.name);
        //if (isSelecting || EventSystem.current.currentSelectedGameObject == gameObject)
        if (isSelecting)
        {
            selectingTimer += Time.deltaTime;
            selectingImage.color = new Color(1, 1, 1, selectingTimer * 3f);
            selectingImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(Mathf.Min(20f * (-1 + selectingTimer * 7f), 0), 0);
        }
        else
        {
            selectingImage.color = new Color(1, 1, 1, 0);
            selectingTimer = 0f;
        }
    }

    public void SetListItem(string _name,int _id,int _selectingId, ListItemCallback _clickCallBack, ListItemCallback _selectingCallBack, ListItemCallback _disSelectingCallBack)
    {
        //_g.AddComponent<ListItem>();
        displayName.text = _name;
        id = _id;
        selectingId = _selectingId;
        clickCallBack = _clickCallBack;
        selectingCallBack = _selectingCallBack;
        disSelectingCallBack = _disSelectingCallBack;

        transform.localScale = new Vector2(transform.localScale.x / 160f * Screen.width, transform.localScale.y / 90f * Screen.height);
    }

    public void Clicked()
    {
        if (isAllowInteract)
        {
            clickCallBack?.Invoke(id, this);
        }
    }

    public void Selecting()
    {
        //if (isAllowInteract)
        //{
            selectingCallBack?.Invoke(id, this);
        //}

        isSelecting = true;

        //GlobalCommunicateManager.selectingId = selectingId;
    }

    public void DisSelecting()
    {
        //if (isAllowInteract)
        //{
            disSelectingCallBack?.Invoke(id, this);
        //}

        isSelecting = false;
    }

    public void DisAbleClicked()
    {
        bg.color = new Color(0.5f, 0.5f, 0.5f, 1f);
        displayName.color = new Color(0.5f, 0.5f, 0.5f, 1f);
        isAllowInteract = false;
    }
}
