using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ClassHelper;

public class DialogCanvas : ControlableUI
{
    public int kWidth = 600;

    public GameObject dialogObject;
    public GameObject nameObject;
    public Text sampleText;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (ControlView.Instance.controls.Map1.Cancel.triggered)
        {
            OnBackPressed();
        }
    }

    public void Setup(Vector2 _pos, string _name, string _content)
    {
        transform.position = _pos;

        transform.SetAsLastSibling();

        float maxWidth = 0;
        TextGenerator textGen = new TextGenerator();
        TextGenerationSettings generationSettings = sampleText.GetGenerationSettings(sampleText.rectTransform.rect.size);

        float _wDialog = textGen.GetPreferredWidth(_content, generationSettings);
        if (maxWidth < _wDialog)
        {
            maxWidth = _wDialog;
        }
        if (maxWidth > kWidth)
        {
            maxWidth = kWidth;
        }
        float _hDialog = textGen.GetPreferredHeight(_content, generationSettings);

        float _wName = textGen.GetPreferredWidth(_name, generationSettings);
        float _hName = textGen.GetPreferredHeight(_name, generationSettings);

        dialogObject.transform.Find("Text").GetComponent<Text>().text = _content;
        dialogObject.GetComponent<RectTransform>().sizeDelta = new Vector3(maxWidth + 50, _hDialog + 10);
        //TODO Assign dialogObject position

        nameObject.transform.Find("Text").GetComponent<Text>().text = _name;
        nameObject.GetComponent<RectTransform>().sizeDelta = new Vector3(_wName + 50, _hName + 10);
        nameObject.transform.position = dialogObject.transform.position + new Vector3(-maxWidth / 2f + _wName / 2f, -nameObject.GetComponent<RectTransform>().sizeDelta.y - _hName / 2f);

        base.AddUI();
    }
}
