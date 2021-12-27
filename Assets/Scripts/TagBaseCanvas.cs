using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ClassHelper;

public class TagBaseCanvas : ShapeGenerator
{
    //#region instance
    //private static TagBaseCanvas m_instance;

    //public static TagBaseCanvas Instance
    //{
    //    get
    //    {
    //        return m_instance;
    //    }
    //}

    //void Awake()
    //{
    //    if (TagBaseCanvas.Instance == null)
    //    {
    //        m_instance = this;
    //    }
    //}
    //#endregion

    public GameObject choosingTagParent;
    public ChoosingTag choosingTag;
    public float canvasSize = 1f;
    public int gridMapBoundary = 3;
    bool isPutChoosingTagValid = true;

    public List<Tag> GetExistingTagList()
    {
        List<Tag> result = new List<Tag>();
        foreach (TagGameObject _tg in existingTagGameObjectList)
        {
            result.Add(_tg.tagContent);
        }
        return result;
    }

    public List<int> GetExistingTagIdList()
    {
        List<int> result = new List<int>();
        foreach (TagGameObject _tg in existingTagGameObjectList)
        {
            result.Add(_tg.tagContent.GetTagData().id);
        }
        return result;
    }
    
    public void Hide()
    {
        ResetTagBase();
        if (choosingTag != null)
        {
            Destroy(choosingTag.gameObject);
        }

        gameObject.SetActive(false);
    }

    // Update is called once per frame
    public void Update()
    {
        gridParent.transform.localScale = Vector3.one * canvasSize;

        ControlScheme();
    }

    public virtual void ControlScheme()
    {
        if (choosingTag != null)
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

    public virtual void PutChoosingTag()
    {
        if (isPutChoosingTagValid)
        {
            GenerateTagGrid(choosingTag.GetComponent<ChoosingTag>().tagContent);
            //TODO Set up tag type visual
            DisselectChoosingTag();
            UIManager.Instance.OnBackPressed();
        }
    }

    public void DisselectChoosingTag()
    {
        if (choosingTag != null)
        {
            Destroy(choosingTag.gameObject);
        }
    }

    public void DetermineIsPutChoosingTagValid()
    {
        isPutChoosingTagValid = true;
        if (CheckIfCollide(choosingTag.GetComponent<ChoosingTag>().tagContent, GetExistingTagList()))
        {
            //print("CheckIfCollide failed");
            isPutChoosingTagValid = false;
        }
        if (!CheckIfInside(choosingTag.GetComponent<ChoosingTag>().tagContent, gridBaseShape))
        {
            //print("CheckIfInside failed");
            isPutChoosingTagValid = false;
        }
    }

    public void DetermineChoosingTagColor()
    {
        if (isPutChoosingTagValid)
        {
            choosingTag.SetColor(Color.white);
        }
        else
        {
            choosingTag.SetColor(Color.red);
        }
    }

    bool CheckIfInside(Tag tag, List<Vector2Int> tagBase)
    {
        foreach (Vector2Int _v in tag.GetGrids())
        {
            if (!tagBase.Contains(_v))
            {
                return false;
            }
        }

        return true;
    }

    bool CheckIfCollide(Tag tag, List<Tag> tagList)
    {
        foreach (Vector2Int _v in tag.GetGrids())
        {
            foreach (Tag _t in tagList)
            {
                foreach (Vector2Int _v2 in _t.GetGrids())
                {
                    if (_v2 == _v)
                    {
                        //print("x = " + (_v2).x + ",y = " + (_v2).y);
                        return true;
                    }
                }
            }
        }

        return false;
    }

    public void DetermineChoosingTagPosition()
    {
        if (choosingTag == null)
        {
            return;
        }

        Vector2Int gridOffset = CalculateGridOffset(Input.mousePosition);
        choosingTag.GetComponent<ChoosingTag>().tagContent.offset = gridOffset;
        choosingTag.transform.localPosition = new Vector3(gridOffset.x, gridOffset.y, 0) * 100f * gridSize;
    }

    public Vector2Int CalculateGridOffset(Vector2 _mousePosition)
    {
        int resultX = Mathf.FloorToInt((_mousePosition.x - gridBaseParent.transform.position.x) / (100f * gridSize) + 0.5f);
        int resultY = Mathf.FloorToInt((_mousePosition.y - gridBaseParent.transform.position.y) / (100f * gridSize) + 0.5f);

        //calculate boundary
        if (choosingTag != null)
        {
            if (resultX + choosingTag.GetComponent<ChoosingTag>().tagContent.GetTagData().GetMaxX() > gridMapBoundary)
            {
                resultX = gridMapBoundary - choosingTag.GetComponent<ChoosingTag>().tagContent.GetTagData().GetMaxX();
            }
            if (resultY + choosingTag.GetComponent<ChoosingTag>().tagContent.GetTagData().GetMaxY() > gridMapBoundary)
            {
                resultY = gridMapBoundary - choosingTag.GetComponent<ChoosingTag>().tagContent.GetTagData().GetMaxY();
            }
            if (resultX + choosingTag.GetComponent<ChoosingTag>().tagContent.GetTagData().GetMinX() < -gridMapBoundary)
            {
                resultX = -gridMapBoundary - choosingTag.GetComponent<ChoosingTag>().tagContent.GetTagData().GetMinX();
            }
            if (resultY + choosingTag.GetComponent<ChoosingTag>().tagContent.GetTagData().GetMinY() < -gridMapBoundary)
            {
                resultY = -gridMapBoundary - choosingTag.GetComponent<ChoosingTag>().tagContent.GetTagData().GetMinY();
            }
        }

        return new Vector2Int(resultX, resultY);
    }

    public void GenerateChoosingTag(Tag _tag)
    {
        for (int i = 0; i < existingTagGameObjectList.Count; i++)
        {
            if (existingTagGameObjectList[i].tagContent.localIndex == _tag.localIndex)
            {
                Destroy(existingTagGameObjectList[i].gameObject);
                existingTagGameObjectList.RemoveAt(i);
                break;
            }
        }

        GameObject _gameObjectInstance = GenerateTagGrid(_tag, false);
        _gameObjectInstance.AddComponent<ChoosingTag>();
        if (choosingTag != null)
        {
            Destroy(choosingTag.gameObject);
        }
        choosingTag = _gameObjectInstance.GetComponent<ChoosingTag>();
        choosingTag.transform.SetParent(choosingTagParent.transform);

        choosingTag.GetComponent<ChoosingTag>().SetUp(_tag);
        choosingTag.GetComponent<ChoosingTag>().SetTagColor(Color.grey);
    }


}
