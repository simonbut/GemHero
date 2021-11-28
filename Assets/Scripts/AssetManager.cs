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

                assetdata.Add(_b);
            }
        }
        else
        {
            Debug.Log("data is null");
        }
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



    [HideInInspector]
    public List<CompoundData> compounddata = new List<CompoundData>();
    public List<CompoundData> GetCompoundDataFullList()
    {
        return compounddata;
    }


    public void LoadCompoundData()
    {
        compounddata = new List<CompoundData>();
        string data = Database.ReadDatabaseWithoutLanguage("Compound");
        if (data.Length > 0)
        {
            string[] _a = data.Split('\n');
            for (int i = 1; i < _a.Length; i++)
            {
                CompoundData _b = new CompoundData();
                string[] _c = _a[i].Split('\t');
                int.TryParse(_c[0], out _b.id);
                _b.name = new LocalizedString(_c[1], _c[1], _c[1], "");
                CompoundType.TryParse(_c[2], out _b.compoundType);


                _b.assetTypeList = new List<int>();
                string[] _c3 = _c[3].Split(';');
                for (int j = 0; j < _c3.Length; j++)
                {
                    int _c3b;
                    int.TryParse(_c3[j], out _c3b);

                    _b.assetTypeList.Add(_c3b);
                }

                _b.basicStatTypeList = new List<StatType>();
                string[] _c4 = _c[4].Split(';');
                for (int j = 0; j < _c4.Length; j++)
                {
                    StatType _c4b;
                    StatType.TryParse(_c4[j], out _c4b);

                    _b.basicStatTypeList.Add(_c4b);
                }

                _b.basicStatList = new List<int>();
                string[] _c5 = _c[5].Split(';');
                for (int j = 0; j < _c5.Length; j++)
                {
                    int _c5b;
                    int.TryParse(_c5[j], out _c5b);

                    _b.basicStatList.Add(_c5b);
                }

                compounddata.Add(_b);
            }
        }
        else
        {
            Debug.Log("data is null");
        }
    }
}
