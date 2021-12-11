using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ClassHelper;

public class ShapeGenerator : MonoBehaviour
{
    public List<Vector2Int> gridBaseShape;

    public GameObject gridBaseParent;
    public GameObject gridParent;
    public GameObject tagNamePrefab;

    public float gridSize = 1f;
    public List<TagGameObject> existingTagGameObjectList = new List<TagGameObject>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void GenerateShape(GameObject _g,List<Vector2Int> _gridBaseShape, List<Tag> _tagList, float _gridSize)
    {
        if (_g.GetComponent<ShapeGenerator>()==null)
        {
            _g.AddComponent<ShapeGenerator>();
        }
        _g.GetComponent<ShapeGenerator>().gridSize = _gridSize;
        _g.GetComponent<ShapeGenerator>().ResetTagBase();
        _g.GetComponent<ShapeGenerator>().Show(_gridBaseShape, _tagList);
    }

    public void Show(List<Vector2Int> _gridBaseShape, List<Tag> _tagList)
    {
        if (gridBaseParent == null)
        {
            gridBaseParent = gameObject;
        }
        if (gridParent == null)
        {
            gridParent = gameObject;
        }

        ResetTagBase();

        gameObject.SetActive(true);

        if (_gridBaseShape != null)
        {
            DefineTagBase(_gridBaseShape);
        }

        if (_tagList != null)
        {
            for (int i = 0; i < _tagList.Count; i++)
            {
                GenerateTagGrid(_tagList[i]);
            }
        }
    }

    public void ResetTagBase()
    {
        if (gridBaseParent != null)
        {
            foreach (Transform _c in gridBaseParent.transform)
            {
                Destroy(_c.gameObject);
            }
        }
        if (gridBaseParent != null)
        {
            foreach (Transform _c in gridParent.transform)
            {
                Destroy(_c.gameObject);
            }
        }
    }

    public void DefineTagBase(List<Vector2Int> _baseShape)
    {
        gridBaseShape = _baseShape;

        //get content size
        int size = 0;
        foreach (Vector2 _v in gridBaseShape)
        {
            int compare = Mathf.FloorToInt(Mathf.Max(Mathf.Abs(_v.x), Mathf.Abs(_v.y)));
            if (compare > size)
            {
                size = compare;
            }
        }

        GameObject _gameObjectInstance = new GameObject();
        _gameObjectInstance.transform.SetParent(gridBaseParent.transform);
        _gameObjectInstance.transform.localPosition = new Vector2(0, 0);

        //generate content
        for (int i = -size; i < size + 1; i++)
        {
            for (int j = -size; j < size + 1; j++)
            {
                if (gridBaseShape.Contains(new Vector2Int(i, j)))
                {
                    GameObject _gridInstance = Instantiate(MainGameView.Instance.tagBasePrefab);
                    _gridInstance.transform.SetParent(_gameObjectInstance.transform);
                    _gridInstance.transform.localPosition = new Vector2((i) * 100f, (j) * 100f) * gridSize;
                    _gridInstance.transform.localScale *= gridSize;

                    Material mat = Instantiate(_gridInstance.GetComponent<Image>().material);
                    //Material mat = new Material(Shader.Find("Shader Graphs/tag"));
                    //mat.name = Random.Range(0, 10000).ToString();
                    //mat.color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));

                    //generate edge and vertice
                    if (!gridBaseShape.Contains(new Vector2Int(i - 1, j)))
                    {
                        mat.SetFloat("Left", 1);
                        if (!gridBaseShape.Contains(new Vector2Int(i, j - 1)))
                        {
                            mat.SetFloat("LeftDownReverse", 1);
                        }
                        if (!gridBaseShape.Contains(new Vector2Int(i, j + 1)))
                        {
                            mat.SetFloat("LeftUpReverse", 1);
                        }
                    }
                    if (!gridBaseShape.Contains(new Vector2Int(i, j - 1)))
                    {
                        mat.SetFloat("Down", 1);
                    }
                    if (!gridBaseShape.Contains(new Vector2Int(i - 1, j - 1)))
                    {
                        mat.SetFloat("LeftDown", 1);
                    }
                    if (!gridBaseShape.Contains(new Vector2Int(i + 1, j - 1)))
                    {
                        mat.SetFloat("RightDown", 1);
                    }
                    if (!gridBaseShape.Contains(new Vector2Int(i + 1, j)))
                    {
                        mat.SetFloat("Right", 1);
                        if (!gridBaseShape.Contains(new Vector2Int(i, j - 1)))
                        {
                            mat.SetFloat("RightDownReverse", 1);
                        }
                        if (!gridBaseShape.Contains(new Vector2Int(i, j + 1)))
                        {
                            mat.SetFloat("RightUpReverse", 1);
                        }
                    }
                    if (!gridBaseShape.Contains(new Vector2Int(i, j + 1)))
                    {
                        mat.SetFloat("Up", 1);
                    }
                    if (!gridBaseShape.Contains(new Vector2Int(i + 1, j + 1)))
                    {
                        mat.SetFloat("RightUp", 1);
                    }
                    if (!gridBaseShape.Contains(new Vector2Int(i - 1, j + 1)))
                    {
                        mat.SetFloat("LeftUp", 1);
                    }

                    _gridInstance.GetComponent<Image>().material = mat;
                }
            }
        }
    }


    public GameObject GenerateTagGrid(Tag _tag, bool isExisting = true)
    {
        TagData _td = _tag.tagData;
        Vector2Int _tagOffset = _tag.offset;
        List<Vector2Int> _gridContents = _tag.GetGrids();

        GameObject _gameObjectInstance = new GameObject();
        _gameObjectInstance.name = _td.name.GetString();
        _gameObjectInstance.transform.SetParent(gridParent.transform);
        _gameObjectInstance.transform.localPosition = new Vector2(0, 0);
        _gameObjectInstance.AddComponent<TagGameObject>();
        _gameObjectInstance.GetComponent<TagGameObject>().tagContent = _tag;

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
                    GameObject _gridInstance = Instantiate(MainGameView.Instance.gridPrefab);
                    _gridInstance.transform.SetParent(_gameObjectInstance.transform);
                    _gridInstance.transform.localPosition = new Vector2((i) * 100f, (j) * 100f) * gridSize;
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
        if (tagNamePrefab != null)
        {

            GameObject _tagNameInstance = Instantiate(tagNamePrefab);
            _tagNameInstance.name = _td.name.GetString();
            _tagNameInstance.GetComponent<Text>().text = _td.name.GetString();
            _tagNameInstance.transform.SetParent(_gameObjectInstance.transform);
            _tagNameInstance.transform.localPosition = new Vector2(0, 0);
            _tagNameInstance.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);
            if (_gridContents.Contains(new Vector2Int(-1 + _tagOffset.x, 0)))
            {
                _tagNameInstance.GetComponent<RectTransform>().sizeDelta += new Vector2(100, 0);
                _tagNameInstance.transform.localPosition += new Vector3(-50, 0, 0);
            }
            if (_gridContents.Contains(new Vector2Int(1 + _tagOffset.x, 0)))
            {
                _tagNameInstance.GetComponent<RectTransform>().sizeDelta += new Vector2(100, 0);
                _tagNameInstance.transform.localPosition += new Vector3(50, 0, 0);
            }
            if (!_gridContents.Contains(new Vector2Int(1 + _tagOffset.x, 0)) && !_gridContents.Contains(new Vector2Int(-1 + _tagOffset.x, 0)))
            {
                if (_gridContents.Contains(new Vector2Int(0, -1 + _tagOffset.y)))
                {
                    _tagNameInstance.GetComponent<RectTransform>().sizeDelta += new Vector2(0, 100);
                    _tagNameInstance.transform.localPosition = new Vector2(0, -50);
                }
                if (_gridContents.Contains(new Vector2Int(0, 1 + _tagOffset.y)))
                {
                    _tagNameInstance.GetComponent<RectTransform>().sizeDelta += new Vector2(0, 100);
                    _tagNameInstance.transform.localPosition = new Vector2(0, 50);
                }
            }
            _tagNameInstance.transform.localPosition = new Vector2(_tagNameInstance.transform.localPosition.x + _tagOffset.x * 100f, _tagNameInstance.transform.localPosition.y + _tagOffset.y * 100f);
            _tagNameInstance.GetComponent<RectTransform>().sizeDelta *= gridSize;
            _tagNameInstance.transform.localPosition *= gridSize;
            _tagNameInstance.transform.localScale *= gridSize;

        }

        if (isExisting)
        {
            existingTagGameObjectList.Add(_gameObjectInstance.GetComponent<TagGameObject>());
        }

        //foreach (Vector2Int _v in _gridContents)
        //{
        //    print("x = " + (_v.x + _tagOffset.x) + ",y = " + (_v.y + _tagOffset.y));
        //}

        return _gameObjectInstance;
    }
}
