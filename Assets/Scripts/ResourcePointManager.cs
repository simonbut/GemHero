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
                int.TryParse(_c[2], out _b.assetId);

                _b.mustHaveTagList = new List<int>();
                string[] _c3 = _c[3].Split(';');
                for (int j = 0; j < _c3.Length; j++)
                {
                    int _c3b;
                    int.TryParse(_c3[j], out _c3b);

                    _b.mustHaveTagList.Add(_c3b);
                }

                _b.tagPool = new List<int>();
                string[] _c4 = _c[4].Split(';');
                for (int j = 0; j < _c4.Length; j++)
                {
                    int _c4b;
                    int.TryParse(_c4[j], out _c4b);

                    _b.tagPool.Add(_c4b);
                }

                _b.rareTagPool = new List<int>();
                string[] _c5 = _c[5].Split(';');
                for (int j = 0; j < _c5.Length; j++)
                {
                    int _c5b;
                    int.TryParse(_c5[j], out _c5b);

                    _b.rareTagPool.Add(_c5b);
                }

                int.TryParse(_c[6], out _b.scoreMin);
                int.TryParse(_c[7], out _b.scoreMax);

                resourcePointdata.Add(_b);
            }
        }
        else
        {
            Debug.Log("data is null");
        }
    }

    public ResourcePointData GetResourcePointData(int _resourcePointId)
    {
        foreach (ResourcePointData _rt in resourcePointdata)
        {
            if (_rt.id == _resourcePointId)
            {
                return _rt;
            }
        }
        return new ResourcePointData();
    }

    public Asset DrawAsset(int _resourcePointId)
    {
        ResourcePointData _rp = GetResourcePointData(_resourcePointId);
        return DrawAsset(_rp.assetId, _rp.mustHaveTagList, _rp.tagPool, _rp.rareTagPool, _rp.scoreMin, _rp.scoreMin);
    }

    public Asset DrawAsset(int _assetId, List<int> _mustHaveTagList, List<int> _tagPool, List<int> _rareTagList, int _scoreMin, int _scoreMax)
    {
        //random draw at most 2 tags and quality, then check score TODO
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
            List<int> _mht = new List<int>(_rareTagList);

            if (_mht.Count > 0)
            {
                int _index = Random.Range(0, _rtp.Count);
                tag1 = _mht[_index];
                score += TagManager.Instance.GetTag(_mht[_index]).score;
                _mht.RemoveAt(_index);
            }
            if (_mht.Count > 0)
            {
                int _index = Random.Range(0, _rtp.Count);
                tag2 = _mht[_index];
                score += TagManager.Instance.GetTag(_mht[_index]).score;
                _mht.RemoveAt(_index);
            }

            if (tag1 == 0 && _rtp.Count > 0)
            {
                if (Random.Range(0, 1000) < 200)
                {
                    int _index = Random.Range(0, _rtp.Count);
                    tag1 = _rtp[_index];
                    score += TagManager.Instance.GetTag(_rtp[_index]).score;
                    _rtp.RemoveAt(_index);
                }
            }
            if (tag1 == 0 && _tp.Count > 0)
            {
                if (Random.Range(0, 1000) < 700)
                {
                    int _index = Random.Range(0, _tp.Count);
                    tag1 = _tp[_index];
                    score += TagManager.Instance.GetTag(_rtp[_index]).score;
                    _tp.RemoveAt(_index);
                }
            }

            if (tag2 == 0 && _rtp.Count > 0)
            {
                if (Random.Range(0, 1000) < 200)
                {
                    int _index = Random.Range(0, _rtp.Count);
                    tag2 = _rtp[_index];
                    score += TagManager.Instance.GetTag(_rtp[_index]).score;
                    _rtp.RemoveAt(_index);
                }
            }
            if (tag2 == 0 && _tp.Count > 0)
            {
                if (Random.Range(0, 1000) < 700)
                {
                    int _index = Random.Range(0, _tp.Count);
                    tag2 = _tp[_index];
                    score += TagManager.Instance.GetTag(_rtp[_index]).score;
                    _tp.RemoveAt(_index);
                }
            }

            qualityAffect = Random.Range(-10, 10);
            score += qualityAffect * 10;

            if (score >= _scoreMin && score <= _scoreMax)
            {
                passScoreCheck = true;
            }
        }

        List<int> tagList = new List<int> { tag1, tag2 };
        Asset result = new Asset();
        result.assetId = _assetId;
        result.qualityAffect = qualityAffect;
        result.tagList = tagList;

        return result;
    }
}
