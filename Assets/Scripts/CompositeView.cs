using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompositeView : MonoBehaviour
{
    public GameObject gridCanvas;
    public GameObject choosingTagCanvas;
    public GameObject gridPrefab;
    public ChoosingTag choosingTag;

    // Start is called before the first frame update
    void Start()
    {
        //GenerateTagGrid(new List<Vector2Int> { Vector2Int.zero,Vector2Int.up,Vector2Int.down,Vector2Int.left,Vector2Int.right,new Vector2Int(1,1) });
        GenerateTagGrid(new List<Vector2Int> { new Vector2Int(-1, -1)});
        GenerateTagGrid(new List<Vector2Int> { new Vector2Int(1, -1), new Vector2Int(1, -2) });

        GenerateChoosingTag(new List<Vector2Int> { Vector2Int.zero, Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right, new Vector2Int(1, 1) });

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateChoosingTag(List<Vector2Int> _gridContent)
    {
        GameObject _gameObjectInstance = GenerateTagGrid(_gridContent);
        _gameObjectInstance.AddComponent<ChoosingTag>();
        if (choosingTag != null)
        {
            Destroy(choosingTag.gameObject);
        }
        choosingTag = _gameObjectInstance.GetComponent<ChoosingTag>();
        choosingTag.transform.SetParent(choosingTagCanvas.transform);
    }


    GameObject GenerateTagGrid(List<Vector2Int> _gridContent)
    {
        GameObject _gameObjectInstance = new GameObject();
        _gameObjectInstance.name = "tag";
        _gameObjectInstance.transform.SetParent(gridCanvas.transform);
        _gameObjectInstance.transform.localPosition = new Vector2(0, 0);

        //get content size
        int size = 0;
        foreach (Vector2 _v in _gridContent)
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
                if (_gridContent.Contains(new Vector2Int(i,j)))
                {
                    GameObject _gridInstance = Instantiate(gridPrefab);
                    _gridInstance.transform.SetParent(_gameObjectInstance.transform);
                    _gridInstance.transform.localPosition = new Vector2(i * 100f, j * 100f);

                    Material mat = Instantiate(_gridInstance.GetComponent<Image>().material);
                    //Material mat = new Material(Shader.Find("Shader Graphs/tag"));
                    //mat.name = Random.Range(0, 10000).ToString();
                    //mat.color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));

                    //generate edge and vertice
                    if (!_gridContent.Contains(new Vector2Int(i - 1, j)))
                    {
                        mat.SetFloat("Left", 1);
                        if (!_gridContent.Contains(new Vector2Int(i, j - 1)))
                        {
                            mat.SetFloat("LeftDownReverse", 1);
                        }
                        if (!_gridContent.Contains(new Vector2Int(i, j + 1)))
                        {
                            mat.SetFloat("LeftUpReverse", 1);
                        }
                    }
                    if (!_gridContent.Contains(new Vector2Int(i, j - 1)))
                    {
                        mat.SetFloat("Down", 1);
                    }
                    if (!_gridContent.Contains(new Vector2Int(i - 1, j - 1)))
                    {
                        mat.SetFloat("LeftDown", 1);
                    }
                    if (!_gridContent.Contains(new Vector2Int(i + 1, j - 1)))
                    {
                        mat.SetFloat("RightDown", 1);
                    }
                    if (!_gridContent.Contains(new Vector2Int(i + 1, j)))
                    {
                        mat.SetFloat("Right", 1);
                        if (!_gridContent.Contains(new Vector2Int(i, j - 1)))
                        {
                            mat.SetFloat("RightDownReverse", 1);
                        }
                        if (!_gridContent.Contains(new Vector2Int(i, j + 1)))
                        {
                            mat.SetFloat("RightUpReverse", 1);
                        }
                    }
                    if (!_gridContent.Contains(new Vector2Int(i, j + 1)))
                    {
                        mat.SetFloat("Up", 1);
                    }
                    if (!_gridContent.Contains(new Vector2Int(i + 1, j + 1)))
                    {
                        mat.SetFloat("RightUp", 1);
                    }
                    if (!_gridContent.Contains(new Vector2Int(i - 1, j + 1)))
                    {
                        mat.SetFloat("LeftUp", 1);
                    }

                    _gridInstance.GetComponent<Image>().material = mat;
                }
            }
        }

        return _gameObjectInstance;
    }
}
