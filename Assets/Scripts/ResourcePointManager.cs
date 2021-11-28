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

                int.TryParse(_c[5], out _b.scoreMin);
                int.TryParse(_c[6], out _b.scoreMax);

                resourcePointdata.Add(_b);
            }
        }
        else
        {
            Debug.Log("data is null");
        }
    }

}
