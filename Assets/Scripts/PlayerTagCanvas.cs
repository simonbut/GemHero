using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClassHelper;

public class PlayerTagCanvas : CharacterTagCanvas
{
    public Tag selectingTagRecoverData;

    public void ShowPlayerTag()
    {
        List<Vector2Int> _baseShape = new List<Vector2Int>();
        for (int i = -3; i < 3; i++)
        {
            for (int j = -3; j < 3; j++)
            {
                _baseShape.Add(new Vector2Int(i, j));
            }
        }

        List<Tag> _playerTag = new List<Tag>();
        for (int i = 0; i < Database.userDataJson.playerTags.Count; i++)
        {
            Tag _t = Database.userDataJson.playerTags[i];
            _t.localIndex = i;
            _playerTag.Add(_t);
        }

        print("_playerTag " + _playerTag.Count);

        Show(_baseShape, _playerTag);
    }

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

            if (ControlView.Instance.controls.Map1.React.triggered)
            {
                SelectChoosingTag();
            }

            if (ControlView.Instance.controls.Map1.Cancel.triggered)
            {
                Database.userDataJson.playerTags = GetExistingTagList();
                Database.Save();

                MainGameView.Instance.playerTagChoosingCanvas.OnBackPressed();
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

    public void SelectChoosingTag()
    {
        print("cursorOffset: " + "x:" + cursorOffset.x + " y:" + cursorOffset.y);
        selectingTagRecoverData = null;
        foreach (Tag _tg in GetExistingTagList())
        {
            foreach (Vector2Int _v in _tg.GetGrids())
            {
                if (cursorOffset == _v)
                {
                    selectingTagRecoverData = Tag.CreateTag(_tg.GetTagData().id, _tg.offset, _tg.affectList);
                    selectingTagRecoverData.localIndex = _tg.localIndex;

                    _tg.offset = Vector2Int.zero;
                    GenerateChoosingTag(_tg);
                    UIManager.Instance.AddEmptyUI();
                    return;
                }
            }
        }
    }
}
