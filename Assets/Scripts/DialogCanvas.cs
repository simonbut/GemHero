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
    public Text sampleText2;

    GameObject targetGameObject;
    int dialogId;
    int step;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (ControlView.Instance.controls.Map1.Cancel.triggered)
        {
            GoNextStep();
        }
        if (ControlView.Instance.controls.Map1.React.triggered)
        {
            GoNextStep();
        }

        if (targetGameObject == null)
        {
            transform.position = new Vector2(Screen.width / 2f, Screen.height / 2f);
        }
        else
        {
            transform.position = MathManager.WorldPosToCanvasPos(targetGameObject.transform.position) - Vector3.up * 100f;
        }
    }

    public void GoNextStep()
    {
        DialogData _dd = DialogManager.Instance.GetDialogData(dialogId,step + 1);
        OnBackPressed();
        if (_dd != null)
        {
            Setup(dialogId, step + 1, callbackAfterDialog);
        }
        else
        {
            if (callbackAfterDialog != null)
            {
                callbackAfterDialog();
            }
        }
    }

    Callback callbackAfterDialog;
    public void Setup(int _dialogId, Callback _callbackAfterDialog)
    {
        Setup(_dialogId, 1, _callbackAfterDialog);
    }

    public void Setup(int _dialogId, int _step,Callback _callbackAfterDialog)
    {
        Database.ReadDialog(_dialogId);
        callbackAfterDialog = _callbackAfterDialog;
        dialogId = _dialogId;
        step = _step;
        DialogData _dd = DialogManager.Instance.GetDialogData(dialogId, step);
        if (_dd.characterId == 1)//Player
        {
            Setup(ControlView.Instance.player, CharacterManager.Instance.GetCharacterData(_dd.characterId).name.GetString(), _dd.content.GetString());
        }
        else
        {
            if (_dd.characterId == 0)//special case
            {
                _dd.characterId = ResourcePointManager.Instance.GetResourcePointDataList(MainGameView.Instance.reactingObject.resourcePointId)[0].characterId;
            }
            ResourcePoint _rp = MainGameView.Instance.FindResourcePointByCharacterId(_dd.characterId);
            if (_rp == null)
            {
                if (CharacterManager.Instance.GetCharacterData(_dd.characterId) != null)
                {
                    Setup(null, CharacterManager.Instance.GetCharacterData(_dd.characterId).name.GetString(), _dd.content.GetString());
                }
                else
                {
                    Setup(null, "", _dd.content.GetString());
                }
            }
            else
            {
                Setup(_rp.gameObject, CharacterManager.Instance.GetCharacterData(_dd.characterId).name.GetString(), _dd.content.GetString());
            }
        }
    }

    public void Setup(GameObject _targetGameObject, string _name, string _content)
    {
        targetGameObject = _targetGameObject;

        transform.SetAsLastSibling();

        float maxWidth = 0;
        TextGenerator textGen = new TextGenerator();
        TextGenerationSettings generationSettings = sampleText.GetGenerationSettings(sampleText.rectTransform.rect.size);
        TextGenerationSettings generationSettings2 = sampleText2.GetGenerationSettings(sampleText2.rectTransform.rect.size);
        
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

        float _wName = textGen.GetPreferredWidth(_name, generationSettings2);
        float _hName = textGen.GetPreferredHeight(_name, generationSettings2);

        dialogObject.transform.Find("Text").GetComponent<Text>().text = _content;
        dialogObject.GetComponent<RectTransform>().sizeDelta = new Vector3(maxWidth + 50, _hDialog + 10);
        //TODO Assign dialogObject position

        nameObject.transform.Find("Text").GetComponent<Text>().text = _name;
        nameObject.GetComponent<RectTransform>().sizeDelta = new Vector3(_wName + 50, _hName + 10);
        nameObject.transform.position = dialogObject.transform.position + new Vector3(-maxWidth / 2f + _wName / 2f, - nameObject.GetComponent<RectTransform>().sizeDelta.y);

        base.AddUI();
    }
}
