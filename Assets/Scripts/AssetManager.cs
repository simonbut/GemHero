using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClassHelper;

public class AssetManager : MonoBehaviour
{
    #region instancetag
    private static AssetManager m_instance;

    public static AssetManager Instance
    {
        get
        {
            return m_instance;
        }
    }

    void Awake()
    {
        if (AssetManager.Instance == null)
        {
            m_instance = this;
        }
    }
    #endregion



    [HideInInspector]
    public List<AssetData> assetdata = new List<AssetData>();
    public List<AssetData> GetAssetDataFullList()
    {
        return assetdata;
    }

    public void LoadAssetData()
    {
        assetdata = new List<AssetData>();
        string data = Database.ReadDatabaseWithoutLanguage("Asset");
        if (data.Length > 0)
        {
            string[] _a = data.Split('\n');
            for (int i = 1; i < _a.Length; i++)
            {
                AssetData _b = new AssetData();
                string[] _c = _a[i].Split('\t');
                int.TryParse(_c[0], out _b.id);

                _b.name = new LocalizedString(_c[1], _c[1], _c[1], "");

                _b.assetTypeList = new List<int>();
                string[] _c6 = _c[2].Split(';');
                for (int j = 0; j < _c6.Length; j++)
                {
                    int _c6b;
                    int.TryParse(_c6[j], out _c6b);

                    _b.assetTypeList.Add(_c6b);
                }

                int.TryParse(_c[3], out _b.firePoint);
                int.TryParse(_c[4], out _b.waterPoint);
                int.TryParse(_c[5], out _b.earthPoint);

                CompoundType.TryParse(_c[6], out _b.compoundType);

                _b.basicStatTypeList = new List<StatType>();
                string[] _c7 = _c[7].Split(';');
                for (int j = 0; j < _c7.Length; j++)
                {
                    StatType _c7b;
                    StatType.TryParse(_c7[j], out _c7b);

                    _b.basicStatTypeList.Add(_c7b);
                }

                _b.basicStatList = new List<int>();
                string[] _c8 = _c[8].Split(';');
                for (int j = 0; j < _c8.Length; j++)
                {
                    int _c8b;
                    int.TryParse(_c8[j], out _c8b);

                    _b.basicStatList.Add(_c8b);
                }

                assetdata.Add(_b);
            }
        }
        else
        {
            Debug.Log("data is null");
        }
    }

    public AssetData GetAssetData(int assetId)
    {
        foreach (AssetData _a in assetdata)
        {
            if (_a.id == assetId)
            {
                return _a;
            }
        }
        return new AssetData();
    }



    [HideInInspector]
    public List<RecipeData> recipedata = new List<RecipeData>();
    public List<RecipeData> GetRecipeDataFullList()
    {
        return recipedata;
    }

    public void LoadRecipeData()
    {
        recipedata = new List<RecipeData>();
        string data = Database.ReadDatabaseWithoutLanguage("Recipe");
        if (data.Length > 0)
        {
            string[] _a = data.Split('\n');
            for (int i = 1; i < _a.Length; i++)
            {
                RecipeData _b = new RecipeData();
                string[] _c = _a[i].Split('\t');
                int.TryParse(_c[0], out _b.id);

                _b.shape = new List<Vector2Int>();
                string[] _c5 = _c[1].Split(';');
                for (int j = 0; j < _c5.Length; j++)
                {
                    int x5 = 0;
                    int y5 = 0;
                    string[] _c5b = _c5[j].Split(',');
                    int.TryParse(_c5b[0], out x5);
                    int.TryParse(_c5b[1], out y5);

                    _b.shape.Add(new Vector2Int(x5, y5));

                }

                _b.assetTypeList = new List<int>();
                string[] _c6 = _c[2].Split(';');
                for (int j = 0; j < _c6.Length; j++)
                {
                    int _c6b;
                    int.TryParse(_c6[j], out _c6b);

                    _b.assetTypeList.Add(_c6b);
                }

                int.TryParse(_c[3], out _b.targetCompoundId);

                recipedata.Add(_b);
            }
        }
        else
        {
            Debug.Log("data is null");
        }
    }

    public RecipeData GetRecipeData(int recipeId)
    {
        foreach (RecipeData _a in recipedata)
        {
            if (_a.id == recipeId)
            {
                return _a;
            }
        }
        return new RecipeData();
    }



    [HideInInspector]
    public List<AssetTypeData> assetTypedata = new List<AssetTypeData>();
    public List<AssetTypeData> GetAssetTypeDataFullList()
    {
        return assetTypedata;
    }

    public void LoadAssetTypeData()
    {
        assetTypedata = new List<AssetTypeData>();
        string data = Database.ReadDatabaseWithoutLanguage("AssetType");
        if (data.Length > 0)
        {
            string[] _a = data.Split('\n');
            for (int i = 1; i < _a.Length; i++)
            {
                AssetTypeData _b = new AssetTypeData();
                string[] _c = _a[i].Split('\t');
                int.TryParse(_c[0], out _b.id);

                _b.name = new LocalizedString(_c[1], _c[1], _c[1], "");

                assetTypedata.Add(_b);
            }
        }
        else
        {
            Debug.Log("data is null");
        }
    }

    public AssetTypeData GetAssetTypeData(int assetTypeId)
    {
        foreach (AssetTypeData _at in assetTypedata)
        {
            if (_at.id == assetTypeId)
            {
                return _at;
            }
        }
        return new AssetTypeData();
    }

    public int CalculateQuality(List<int> _tagList,int _qualityAffect)
    {
        int result = 0;
        foreach (int _t in _tagList)
        {
            result += Mathf.FloorToInt(TagManager.Instance.GetTag(_t).score / 10f);
        }
        result = Mathf.FloorToInt(result * (1 + _qualityAffect / 100f));

        return result;
    }

    public string AssetTypeListToString(List<AssetTypeData> assetTypeDataList)
    {
        string result = "";
        foreach (AssetTypeData _atd in assetTypeDataList)
        {
            result += _atd.name.GetString();
            result += "、";
        }
        result = result.Substring(0, result.Length - 1);
        return result;
    }
}
