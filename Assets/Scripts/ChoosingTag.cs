using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ClassHelper;

public class ChoosingTag : MonoBehaviour
{
    public Tag tagContent = new Tag();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetUp(Tag _tag)
    {
        tagContent = _tag;
    }

    public void SetTagColor(Color _c)
    {
        Image[] images = GetComponentsInChildren<Image>();
        foreach (Image _i in images)
        {
            //Material mat = Instantiate(_i.GetComponent<Image>().material);
            //mat.SetColor("TagColor", _c);
            //_i.GetComponent<Image>().material = mat;
            _i.GetComponent<Image>().material.SetColor("TagColor", _c);
        }
    }

    public void SetColor(Color _c)
    {
        Image[] images = GetComponentsInChildren<Image>();
        foreach (Image _i in images)
        {
            _i.color = _c;
        }
    }
}
