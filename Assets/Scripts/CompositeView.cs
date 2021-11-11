using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ClassHelper;

public class CompositeView : MonoBehaviour
{
    public GameObject gridCanvas;
    public GameObject choosingTagCanvas;
    public GameObject gridPrefab;
    public GameObject tagNamePrefab;
    public ChoosingTag choosingTag;
    public float gridSize = 1f;
    public float canvasSize = 1f;

    public List<Tag> existingTagList = new List<Tag>();

    // Start is called before the first frame update
    void Start()
    {
        //GenerateTagGrid(new List<Vector2Int> { Vector2Int.zero,Vector2Int.up,Vector2Int.down,Vector2Int.left,Vector2Int.right,new Vector2Int(1,1) });
        GenerateTagGrid(new List<Vector2Int> { new Vector2Int(0, 0) }, new Vector2Int(-1, -1), "Text 1");
        GenerateTagGrid(new List<Vector2Int> { new Vector2Int(0, 0), new Vector2Int(0, -1) }, new Vector2Int(1, -1), "Text 2");

        GenerateChoosingTag(new List<Vector2Int> { Vector2Int.zero, Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right, new Vector2Int(1, 1) }, "Text 3");

    }

    // Update is called once per frame
    void Update()
    {
        gridCanvas.transform.localScale = Vector3.one * canvasSize;

        DetermineChoosingTagPosition();


        if (CheckIfCollide(choosingTag.GetComponent<ChoosingTag>().tagContent, existingTagList))
        {
            choosingTag.SetColor(Color.red);
        }
        else
        {
            choosingTag.SetColor(Color.white);
        }
    }

    bool CheckIfCollide(Tag tag, List<Tag> tagList)
    {
        foreach (Vector2Int _v in tag.grids)
        {
            foreach (Tag _t in tagList)
            {
                foreach (Vector2Int _v2 in _t.grids)
                {
                    if (_v2 + _t.offset == _v + tag.offset)
                    {
                        print("x = " + (_v2 + _t.offset).x + ",y = " + (_v2 + _t.offset).y);
                        return true;
                    }
                }
            }
        }

        return false;
    }

    void DetermineChoosingTagPosition()
    {
        if (choosingTag == null)
        {
            return;
        }

        Vector2Int gridOffset = CalculateGridOffset(Input.mousePosition);
        choosingTag.GetComponent<ChoosingTag>().tagContent.offset = gridOffset;
        choosingTag.transform.localPosition = new Vector3(gridOffset.x, gridOffset.y, 0) * 100f * gridSize;
    }

    Vector2Int CalculateGridOffset(Vector2 _mousePosition)
    {
        return new Vector2Int(Mathf.FloorToInt((_mousePosition.x - Screen.width / 2f) / (100f * gridSize) + 0.5f), Mathf.FloorToInt((_mousePosition.y - Screen.height / 2f) / (100f * gridSize) + 0.5f));
    }

    void GenerateChoosingTag(List<Vector2Int> _gridContents, string _tagName = "")
    {
        GameObject _gameObjectInstance = GenerateTagGrid(_gridContents, new Vector2Int(), _tagName, false);
        _gameObjectInstance.AddComponent<ChoosingTag>();
        if (choosingTag != null)
        {
            Destroy(choosingTag.gameObject);
        }
        choosingTag = _gameObjectInstance.GetComponent<ChoosingTag>();
        choosingTag.transform.SetParent(choosingTagCanvas.transform);
        choosingTag.GetComponent<ChoosingTag>().SetUp(_gridContents, new Vector2Int());
        choosingTag.GetComponent<ChoosingTag>().SetTagColor(Color.grey);
    }


    GameObject GenerateTagGrid(List<Vector2Int> _gridContents, Vector2Int _tagOffset, string _tagName = "", bool isExisting = true)
    {
        GameObject _gameObjectInstance = new GameObject();
        _gameObjectInstance.name = _tagName;
        _gameObjectInstance.transform.SetParent(gridCanvas.transform);
        _gameObjectInstance.transform.localPosition = new Vector2(0, 0);

        //get content size
        int size = 0;
        foreach (Vector2 _v in _gridContents)
        {
            int compare = Mathf.FloorToInt(Mathf.Max(Mathf.Abs(_v.x), Mathf.Abs(_v.y)));
            if (compare > size)
            {
                size = compare;
            }
        }

        //generate content
        for (int i = -size; i < size + 1; i++)
        {
            for (int j = -size; j < size + 1; j++)
            {
                if (_gridContents.Contains(new Vector2Int(i, j)))
                {
                    GameObject _gridInstance = Instantiate(gridPrefab);
                    _gridInstance.transform.SetParent(_gameObjectInstance.transform);
                    _gridInstance.transform.localPosition = new Vector2((i + _tagOffset.x) * 100f, (j + _tagOffset.y) * 100f) * gridSize;
                    _gridInstance.transform.localScale *= gridSize;

                    Material mat = Instantiate(_gridInstance.GetComponent<Image>().material);
                    //Material mat = new Material(Shader.Find("Shader Graphs/tag"));
                    //mat.name = Random.Range(0, 10000).ToString();
                    //mat.color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));

                    //generate edge and vertice
                    if (!_gridContents.Contains(new Vector2Int(i - 1, j)))
                    {
                        mat.SetFloat("Left", 1);
                        if (!_gridContents.Contains(new Vector2Int(i, j - 1)))
                        {
                            mat.SetFloat("LeftDownReverse", 1);
                        }
                        if (!_gridContents.Contains(new Vector2Int(i, j + 1)))
                        {
                            mat.SetFloat("LeftUpReverse", 1);
                        }
                    }
                    if (!_gridContents.Contains(new Vector2Int(i, j - 1)))
                    {
                        mat.SetFloat("Down", 1);
                    }
                    if (!_gridContents.Contains(new Vector2Int(i - 1, j - 1)))
                    {
                        mat.SetFloat("LeftDown", 1);
                    }
                    if (!_gridContents.Contains(new Vector2Int(i + 1, j - 1)))
                    {
                        mat.SetFloat("RightDown", 1);
                    }
                    if (!_gridContents.Contains(new Vector2Int(i + 1, j)))
                    {
                        mat.SetFloat("Right", 1);
                        if (!_gridContents.Contains(new Vector2Int(i, j - 1)))
                        {
                            mat.SetFloat("RightDownReverse", 1);
                        }
                        if (!_gridContents.Contains(new Vector2Int(i, j + 1)))
                        {
                            mat.SetFloat("RightUpReverse", 1);
                        }
                    }
                    if (!_gridContents.Contains(new Vector2Int(i, j + 1)))
                    {
                        mat.SetFloat("Up", 1);
                    }
                    if (!_gridContents.Contains(new Vector2Int(i + 1, j + 1)))
                    {
                        mat.SetFloat("RightUp", 1);
                    }
                    if (!_gridContents.Contains(new Vector2Int(i - 1, j + 1)))
                    {
                        mat.SetFloat("LeftUp", 1);
                    }

                    _gridInstance.GetComponent<Image>().material = mat;
                }
            }
        }

        //generate name
        GameObject _tagNameInstance = Instantiate(tagNamePrefab);
        _tagNameInstance.name = _tagName;
        _tagNameInstance.GetComponent<Text>().text = _tagName;
        _tagNameInstance.transform.SetParent(_gameObjectInstance.transform);
        _tagNameInstance.transform.localPosition = new Vector2(0, 0);
        _tagNameInstance.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);
        if (_gridContents.Contains(new Vector2Int(-1, 0)))
        {
            _tagNameInstance.GetComponent<RectTransform>().sizeDelta += new Vector2(100, 0);
            _tagNameInstance.transform.localPosition += new Vector3(-50, 0, 0);
        }
        if (_gridContents.Contains(new Vector2Int(1, 0)))
        {
            _tagNameInstance.GetComponent<RectTransform>().sizeDelta += new Vector2(100, 0);
            _tagNameInstance.transform.localPosition += new Vector3(50, 0, 0);
        }
        if (!_gridContents.Contains(new Vector2Int(1, 0)) && !_gridContents.Contains(new Vector2Int(-1, 0)))
        {
            if (_gridContents.Contains(new Vector2Int(0, -1)))
            {
                _tagNameInstance.GetComponent<RectTransform>().sizeDelta += new Vector2(0, 100);
                _tagNameInstance.transform.localPosition = new Vector2(0, -50);
            }
            if (_gridContents.Contains(new Vector2Int(0, 1)))
            {
                _tagNameInstance.GetComponent<RectTransform>().sizeDelta += new Vector2(0, 100);
                _tagNameInstance.transform.localPosition = new Vector2(0, 50);
            }
        }
        _tagNameInstance.transform.localPosition = new Vector2(_tagNameInstance.transform.localPosition.x + _tagOffset.x * 100f, _tagNameInstance.transform.localPosition.y + _tagOffset.y * 100f);
        _tagNameInstance.GetComponent<RectTransform>().sizeDelta *= gridSize;
        _tagNameInstance.transform.localPosition *= gridSize;
        _tagNameInstance.transform.localScale *= gridSize;

        if (isExisting)
        {
            existingTagList.Add(Tag.CreateTag(_gridContents, _tagOffset));
        }

        //foreach (Vector2Int _v in _gridContents)
        //{
        //    print("x = " + (_v.x + _tagOffset.x) + ",y = " + (_v.y + _tagOffset.y));
        //}

        return _gameObjectInstance;
    }
}
