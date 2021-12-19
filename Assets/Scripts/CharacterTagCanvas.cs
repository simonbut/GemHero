using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ClassHelper;

public class CharacterTagCanvas : TagBaseCanvas
{

    // Update is called once per frame
    public new void Update()
    {
        base.Update();
    }

    public override void ControlScheme()
    {
        if (choosingTag == null)
        {
            DetermineSelectingTagPosition();

            if (ControlView.Instance.controls.Map1.Cancel.triggered)
            {
                UIManager.Instance.OnBackPressed();
                Hide();
            }
        }
        else
        {
            DetermineChoosingTagPosition();

            DetermineIsPutChoosingTagValid();

            DetermineChoosingTagColor();

            if (ControlView.Instance.controls.Map1.React.triggered)
            {
                PutChoosingTag();
            }
        }
    }

    public Vector2Int cursorOffset;
    public int selectingTagId;
    //public GameObject cursor;
    public void DetermineSelectingTagPosition()
    {
        cursorOffset = CalculateGridOffset(Input.mousePosition);
        //cursor.GetComponent<TagBaseCursor>().offset = cursorOffset;
        //cursor.transform.localPosition = new Vector3(cursorOffset.x, cursorOffset.y, 0) * 100f * gridSize;

        foreach (Tag _tg in GetExistingTagList())
        {
            foreach (Vector2Int _v in _tg.GetGrids())
            {
                if (cursorOffset == _v)
                {
                    selectingTagId = _tg.tagDataId;
                    return;
                }
            }
        }
    }

}