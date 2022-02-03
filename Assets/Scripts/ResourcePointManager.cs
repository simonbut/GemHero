using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClassHelper;

public class ResourcePointManager : MonoBehaviour
{
    #region instancetag
    private static ResourcePointManager m_instance;

    public static ResourcePointManager Instance
    {
        get
        {
            return m_instance;
        }
    }

    void Awake()
    {
        if (ResourcePointManager.Instance == null)
        {
            m_instance = this;
        }
    }
    #endregion

    [HideInInspector]
    public List<ResourcePointData> resourcePointdata = new List<ResourcePointData>();
    public List<ResourcePointData> GetResourcePointDataFullList()
    {
        return resourcePointdata;
    }

    public void LoadResourcePointData()
    {
        //id	resource_point_id	asset_id	must_have_tag_list	tag_pool	score_min	score_max
        resourcePointdata = new List<ResourcePointData>();
        string data = Database.ReadDatabaseWithoutLanguage("ResourcePoint");
        if (data.Length > 0)
        {
            string[] _a = data.Split('\n');
            for (int i = 1; i < _a.Length; i++)
            {
                ResourcePointData _b = new ResourcePointData();
                string[] _c = _a[i].Split('\t');
                int.TryParse(_c[0], out _b.id);
                int.TryParse(_c[1], out _b.resourcePointId);
                ResourceType.TryParse(_c[2], out _b.resourceType);
                bool.TryParse(_c[18], out _b.disappearAfter);
                switch (_b.resourceType)
                {
                    case ResourceType.collect:
                        int.TryParse(_c[3], out _b.assetId);

                        _b.mustHaveTagList = new List<int>();
                        string[] _c3 = _c[4].Split(';');
                        for (int j = 0; j < _c3.Length; j++)
                        {
                            int _c3b;
                            int.TryParse(_c3[j], out _c3b);

                            if (_c3b > 0)
                            {
                                _b.mustHaveTagList.Add(_c3b);
                            }
                        }

                        _b.tagPool = new List<int>();
                        string[] _c4 = _c[5].Split(';');
                        for (int j = 0; j < _c4.Length; j++)
                        {
                            int _c4b;
                            int.TryParse(_c4[j], out _c4b);

                            if (_c4b > 0)
                            {
                                _b.tagPool.Add(_c4b);
                            }
                        }

                        _b.rareTagPool = new List<int>();
                        string[] _c5 = _c[6].Split(';');
                        for (int j = 0; j < _c5.Length; j++)
                        {
                            int _c5b;
                            int.TryParse(_c5[j], out _c5b);

                            if (_c5b > 0)
                            {
                                _b.rareTagPool.Add(_c5b);
                            }
                        }

                        int.TryParse(_c[7], out _b.scoreMin);
                        int.TryParse(_c[8], out _b.scoreMax);
                        break;
                    case ResourceType.talk:
                    case ResourceType.mainQuest:
                        int.TryParse(_c[9], out _b.characterId);
                        DialogType.TryParse(_c[10], out _b.dialogType);
                        int.TryParse(_c[11], out _b.targetDialogId);
                        int.TryParse(_c[12], out _b.questId);
                        int.TryParse(_c[13], out _b.afterQuestId);
                        break;
                    case ResourceType.changePos:
                        int.TryParse(_c[14], out _b.mapId);

                        _b.pos = new Vector2();
                        string[] _c15 = _c[15].Split(',');
                        int _c15x;
                        int.TryParse(_c15[0], out _c15x);
                        int _c15y;
                        int.TryParse(_c15[1], out _c15y);
                        _b.pos = new Vector2(_c15x, _c15y);

                        break;
                    case ResourceType.library:
                        _b.bookId = new List<int>();
                        string[] _c16 = _c[16].Split(';');
                        for (int j = 0; j < _c16.Length; j++)
                        {
                            int _c16b;
                            int.TryParse(_c16[j], out _c16b);

                            if (_c16b > 0)
                            {
                                _b.bookId.Add(_c16b);
                            }
                        }
                        break;
                    case ResourceType.monster:

                        _b.enemyList = new List<int>();
                        string[] _c17 = _c[17].Split(';');
                        for (int j = 0; j < _c17.Length; j++)
                        {
                            int _c17b;
                            int.TryParse(_c17[j], out _c17b);

                            if (_c17b > 0)
                            {
                                _b.enemyList.Add(_c17b);
                            }
                        }
                        goto case ResourceType.collect;


                }

                resourcePointdata.Add(_b);
            }
        }
        else
        {
            Debug.Log("data is null");
        }
    }

    public List<ResourcePointData> GetResourcePointDataList(int _resourcePointId)
    {
        List<ResourcePointData> result = new List<ResourcePointData>();
        foreach (ResourcePointData _rt in resourcePointdata)
        {
            if (_rt.resourcePointId == _resourcePointId)
            {
                result.Add(_rt);
            }
        }
        return result;
    }

    public ResourcePointData GetResourcePointDataByDialogType(int _resourcePointId, DialogType _dialogType)
    {
        foreach (ResourcePointData _rt in resourcePointdata)
        {
            if (_rt.resourcePointId == _resourcePointId && _rt.dialogType == _dialogType)
            {
                return _rt;
            }
        }
        return null;
    }



    [HideInInspector]
    public List<BookData> bookData = new List<BookData>();
    public List<BookData> GetBookDataFullList()
    {
        return bookData;
    }


    public void LoadBookData()
    {
        //id	resource_point_id	asset_id	must_have_tag_list	tag_pool	score_min	score_max
        bookData = new List<BookData>();
        string data = Database.ReadDatabaseWithoutLanguage("Book");
        if (data.Length > 0)
        {
            string[] _a = data.Split('\n');
            for (int i = 1; i < _a.Length; i++)
            {
                BookData _b = new BookData();
                string[] _c = _a[i].Split('\t');
                int.TryParse(_c[0], out _b.id);
                _b.name = new LocalizedString(_c[1], _c[1], _c[1], "");
                _b.description = new LocalizedString(_c[2], _c[2], _c[2], "");

                _b.recipeList = new List<int>();
                string[] _c3 = _c[3].Split(';');
                for (int j = 0; j < _c3.Length; j++)
                {
                    int _c3b;
                    int.TryParse(_c3[j], out _c3b);

                    if (_c3b > 0)
                    {
                        _b.recipeList.Add(_c3b);
                    }
                }

                int.TryParse(_c[4], out _b.time);

                bookData.Add(_b);
            }
        }
        else
        {
            Debug.Log("data is null");
        }
    }

    public BookData GetBookData(int _bookDataId)
    {
        foreach (BookData _bd in bookData)
        {
            if (_bd.id == _bookDataId)
            {
                return _bd;
            }
        }
        return null;
    }

    public Asset DrawAsset(int _resourcePointId, bool _isAddToDatabase = true)
    {
        List<ResourcePointData> _rpl = GetResourcePointDataList(_resourcePointId);
        ResourcePointData _rp = _rpl[Random.Range(0, _rpl.Count)];
        return DrawAsset(_rp.assetId, _rp.mustHaveTagList, _rp.tagPool, _rp.rareTagPool, _rp.scoreMin, _rp.scoreMin, _isAddToDatabase);
    }

    public Asset DrawAsset(int _assetId, List<int> _mustHaveTagList, List<int> _tagPool, List<int> _rareTagList, int _scoreMin, int _scoreMax, bool _isAddToDatabase = true)
    {
        int tag1 = 0;
        int tag2 = 0;
        int qualityAffect = 0;

        bool passScoreCheck = false;
        int tryCount = 0;

        while (!passScoreCheck && tryCount < 100)
        {
            int score = 0;
            tryCount++;
            List<int> _tp = new List<int>(_tagPool);
            List<int> _rtp = new List<int>(_rareTagList);
            List<int> _mht = new List<int>(_mustHaveTagList);

            if (_mht.Count > 0)
            {
                int _index = Random.Range(0, _mht.Count);
                tag1 = _mht[_index];
                score += TagManager.Instance.GetTagData(_mht[_index]).score;
                _mht.RemoveAt(_index);
            }
            if (_mht.Count > 0)
            {
                int _index = Random.Range(0, _mht.Count);
                tag2 = _mht[_index];
                score += TagManager.Instance.GetTagData(_mht[_index]).score;
                _mht.RemoveAt(_index);
            }

            if (tag1 == 0 && _rtp.Count > 0)
            {
                if (Random.Range(0, 1000) < 200)
                {
                    int _index = Random.Range(0, _rtp.Count);
                    tag1 = _rtp[_index];
                    score += TagManager.Instance.GetTagData(_rtp[_index]).score;
                    _rtp.RemoveAt(_index);
                }
            }
            if (tag1 == 0 && _tp.Count > 0)
            {
                if (Random.Range(0, 1000) < 700)
                {
                    int _index = Random.Range(0, _tp.Count);
                    tag1 = _tp[_index];
                    score += TagManager.Instance.GetTagData(_tp[_index]).score;
                    _tp.RemoveAt(_index);
                }
            }

            if (tag2 == 0 && _rtp.Count > 0)
            {
                if (Random.Range(0, 1000) < 200)
                {
                    int _index = Random.Range(0, _rtp.Count);
                    tag2 = _rtp[_index];
                    score += TagManager.Instance.GetTagData(_rtp[_index]).score;
                    _rtp.RemoveAt(_index);
                }
            }
            if (tag2 == 0 && _tp.Count > 0)
            {
                if (Random.Range(0, 1000) < 700)
                {
                    int _index = Random.Range(0, _tp.Count);
                    tag2 = _tp[_index];
                    score += TagManager.Instance.GetTagData(_tp[_index]).score;
                    _tp.RemoveAt(_index);
                }
            }

            qualityAffect = Random.Range(-20, 30);
            score += qualityAffect;

            if (score >= _scoreMin && score <= _scoreMax)
            {
                passScoreCheck = true;
            }
        }

        List<int> tagList = new List<int>();
        if (tag1 > 0)
        {
            tagList.Add(tag1);
        }
        if (tag2 > 0)
        {
            tagList.Add(tag2);
        }
        Asset result = new Asset();
        result.assetId = _assetId;
        result.qualityAffect = qualityAffect;
        result.tagList = tagList;

        if (_isAddToDatabase)
        {
            Database.AddAsset(result);
        }

        return result;
    }

    public TalkDialogList GetTalkData(int _resourcePointId)
    {
        TalkDialogList result = new TalkDialogList();
        List<ResourcePointData> _rpdl = GetResourcePointDataList(_resourcePointId);
        result.characterId = _rpdl[0].characterId;
        foreach (ResourcePointData _rpd in _rpdl)
        {
            switch (_rpd.dialogType)
            {
                case DialogType.afterDestinyShare:
                    result.afterDestinyShare = _rpd;
                    break;
                case DialogType.afterItemQuest:
                    result.afterItemQuest = _rpd;
                    break;
                case DialogType.afterBattleQuest:
                    result.afterBattleQuest = _rpd;
                    break;
                case DialogType.normal:
                    result.normal = _rpd;
                    break;
            }
        }
        return result;
    }

    public MainQuestDialogList GetMainQuestData(int _resourcePointId)
    {
        MainQuestDialogList result = new MainQuestDialogList();
        List<ResourcePointData> _rpdl = GetResourcePointDataList(_resourcePointId);
        result.characterId = _rpdl[0].characterId;
        foreach (ResourcePointData _rpd in _rpdl)
        {
            switch (_rpd.dialogType)
            {
                case DialogType.beforeMainBattleQuest:
                    result.beforeMainBattleQuest = _rpd;
                    break;
                case DialogType.afterMainBattleQuest:
                    result.afterMainBattleQuest = _rpd;
                    break;
                case DialogType.talkQuest:
                    result.talkQuest = _rpd;
                    break;
                case DialogType.beforeMainReleaseQuest:
                    result.beforeMainReleaseQuest = _rpd;
                    break;
                case DialogType.afterMainReleaseQuest:
                    result.afterMainReleaseQuest = _rpd;
                    break;
            }
        }
        return result;
    }
}
