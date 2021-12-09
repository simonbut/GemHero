using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace ClassHelper
{
    //General
    public enum Language
    {
        en,
        zh,
        cn,
        jp,
        de,
    }

    public class LocalizedString
    {
        public string zh = "";
        public string en = "";
        public string cn = "";
        public string jp = "";

        public LocalizedString(string _zh, string _en, string _cn, string _jp)
        {
            zh = _zh;
            en = _en;
            cn = _cn;
            jp = _jp;
        }

        public string GetString(Language language = new Language())
        {
            if (language == new Language())
            {
                language = Database.globalData.language;
            }

            string result = "";
            switch (language)
            {
                case Language.cn:
                    result = cn;
                    break;
                case Language.jp:
                    result = jp;
                    break;
                //case Language.de:
                //    return de;
                //    break;
                case Language.zh:
                    result = zh;
                    break;
                case Language.en:
                default:
                    result = en;
                    break;
            }
            //if (JoyStickManager.Instance.IsJoyStickEnable())
            //{
            //    result = result.Replace("「W」, 「A」, 「S」, and 「D」", "「D-Pad」");
            //    result = result.Replace("「W」、「A」、「S」、「D」", "「D-Pad」");
            //    result = result.Replace("「Q」 and 「E」", "「L1」 and 「R1」");
            //    result = result.Replace("「Q」、「E」", "「L1」、「R1」");
            //}
            if (result.Contains("「") && result.Contains("」"))
            {
                result = result.Replace("「", "<b> ");
                result = result.Replace("」", " </b>");
            }
            result = result.Replace("，", "， ");
            result = result.Replace("、", "、 ");
            result = result.Replace("。", "。 ");
            return result;
        }
    }


    public class Achievement
    {

        //get from database
        public int achievement_id;
        public LocalizedString name;
        public LocalizedString description;
        public int rewardOrder;
        public bool isValid;

        public bool IsAchievementGet()
        {
            if (achievement_id == 0)
            {
                return true;
            }
            return Database.globalData.completed_achievement_id.Contains((int)achievement_id);
        }
    }

    public class Memory
    {
        public int memory_id;
        public string oice_uuid;
        public int stage;
        public bool isBefore;

        public bool isGet = false;
    }

    public enum KeyBoardInput
    {
        Input_1 = 1,
        Input_2,
        Input_3,
        Input_4,
        Input_5,
        Input_6,
        Input_7,
        Input_8,
        Input_9,
        Input_0,
        Input_Enter,
        Input_Left,
        Input_Right,
        Input_Up,
        Input_Down,
        //1-15

        Input_sW,
        Input_Z,
        Input_A,
        Input_E,
        Input_R,
        Input_T,
        Input_Y,
        Input_U,
        Input_I,
        Input_O,
        Input_P,
        Input_Q,
        Input_S,
        Input_D,
        Input_F,
        Input_G,
        Input_H,
        Input_J,
        Input_K,
        Input_L,
        Input_M,
        Input_W,
        Input_X,
        Input_C,
        Input_Space,

        Input_Left_Click,
        Input_Right_Click,
        Input_Red,

        Input_sP,
        Input_V,
        Input_B,
        Input_N,

        Input_Green,
        Input_Blue,
        Input_Gear,
        Input_Money,
        Input_EnergyTank,

        Input_HOPE,
        Input_Shop,
        Input_Tech,
        Input_Action,
        Input_Star,

        F1,
        F2,
        F3,
        F4,
        F5,
        F6,
        F7,
        F8,
        F9,

        Up,
        Down,

        joystick_direction,
        joystick_l1,
        joystick_l2,
        joystick_r1,
        joystick_r2,
        joystick_circle,
        joystick_square,
        joystick_cross,
        joystick_triangle,
        joystick_R3Up,
        joystick_R3Down,

    }
    //


    public delegate void Callback();
    public delegate void ListItemCallback(int _id, ListItem gi);
    public delegate void GridItemCallback(int _id, GridItem gi);

    public class Tag
    {
        //public int uid;
        public TagData tagData;
        public List<int> affectList = new List<int>();
        public Vector2Int offset = new Vector2Int();

        public List<Vector2Int> GetGrids()
        {
            List<Vector2Int> result = new List<Vector2Int>();
            foreach (Vector2Int _v in tagData.grids)
            {
                Vector2Int _resultV = _v + offset;
                //TODO affectList
                result.Add(_resultV);
            }
            return result;
        }

        static public Tag CreateTag(int _tagid, Vector2Int _offset, List<int> _affectList)
        {
            Tag result = new Tag();
            result.tagData = TagManager.Instance.GetTag(_tagid);
            result.offset = _offset;
            result.affectList = _affectList;

            return result;
        }

        static public Tag CreateTag(TagData _tagData, Vector2Int _offset, List<int> _affectList)
        {
            Tag result = new Tag();
            result.tagData = _tagData;
            result.offset = _offset;
            result.affectList = _affectList;

            return result;
        }
    }

    public class TagData
    {
        //id	groupId	subId	name	description	grids	compound_type_list	score
        public int id;
        public int groupId;
        public int subId;
        public LocalizedString name;
        public LocalizedString description;
        public List<Vector2Int> grids;
        public List<CompoundType> compoundTypeList = new List<CompoundType>();
        public int score;
        public bool isBadTag;
        public TagType tagType;
        public int RequireAchievementsCount = 0;

        public int GetMaxX()
        {
            int result = 0;
            foreach (Vector2Int _g in grids)
            {
                if (_g.x > result)
                {
                    result = _g.x;
                }
            }
            return result;
        }

        public int GetMinX()
        {
            int result = 0;
            foreach (Vector2Int _g in grids)
            {
                if (_g.x < result)
                {
                    result = _g.x;
                }
            }
            return result;
        }

        public int GetMaxY()
        {
            int result = 0;
            foreach (Vector2Int _g in grids)
            {
                if (_g.y > result)
                {
                    result = _g.y;
                }
            }
            return result;
        }

        public int GetMinY()
        {
            int result = 0;
            foreach (Vector2Int _g in grids)
            {
                if (_g.y < result)
                {
                    result = _g.y;
                }
            }
            return result;
        }
    }

    public enum Force
    {
        player = 0,
        enemy
    }

    public class CharacterAttribute
    {
        float hpTotalPt = 500f;
        float atkPt = 100f;
        float atsPt = 1000f;

        public static CharacterAttribute SetUpCharacterAttribute(float _hpTotalPt, float _atkPt, float _atsPt)
        {
            CharacterAttribute result = new CharacterAttribute();
            result.hpTotalPt = _hpTotalPt;
            result.atkPt = _atkPt;
            result.atsPt = _atsPt;

            return result;
        }

        public float GetHpTotal()
        {
            return hpTotalPt;
        }

        public float GetAtk()
        {
            return atkPt;
        }

        public float GetAts()
        {
            return atsPt;
        }
    }

    public enum ReactType
    {
        Collect = 0,
        Talk = 1
    }

    public enum CompoundType
    {
        weapon = 0,
        accessory,
        consumable,
        compound,
        asset
    }

    public enum StatType
    {
        atk = 0,
        ats,
        hp,
        def
    }

    public enum Rank
    {
        F = 0,
        E,
        D,
        C,
        B,
        A,
        S
    }

    public enum TagType
    {
        AssetTag = 0,
        CharacterTag,
        GemTag,
        FixedTag
    }

    public class ResourcePointData
    {
        //id	resource_point_id	asset_id	must_have_tag_list	tag_pool	score_min	score_max
        public int id;
        public int resourcePointId;
        public int assetId;
        public List<int> mustHaveTagList = new List<int>();
        public List<int> tagPool = new List<int>();
        public List<int> rareTagPool = new List<int>();
        public int scoreMin;
        public int scoreMax;
    }

    public class AssetData
    {
        //id	name	asset_type_list	fire_point	water_point	earth_point
        public int id;
        public LocalizedString name;
        public List<int> assetTypeList = new List<int>();
        public int realityPoint;
        public int dreamPoint;
        public int idealPoint;

        public CompoundType compoundType;
        public List<StatType> basicStatTypeList;
        public List<int> basicStatList;

        public List<AssetTypeData> GetAssetTypeList()
        {
            List<AssetTypeData> result = new List<AssetTypeData>();
            foreach (int _at in assetTypeList)
            {
                result.Add(AssetManager.Instance.GetAssetTypeData(_at));
            }
            return result;
        }


        public bool IsAssetType(int assetTypeId)
        {
            foreach (int _at in assetTypeList)
            {
                if (_at == assetTypeId)
                {
                    return true;
                }
            }
            return false;
        }
    }

    public class Asset
    {
        public int assetUid;
        public int assetId;
        public int qualityAffect;
        public List<int> tagList;

        public int GetQuality()
        {
            return AssetManager.Instance.CalculateQuality(tagList, qualityAffect);
        }

        public AssetData GetAssetData()
        {
            return AssetManager.Instance.GetAssetData(assetId);
        }

        public float GetAttr1()
        {
            if (GetAssetData().basicStatTypeList.Count < 1)
            {
                return 0;
            }
            else
            {
                return GetAssetData().basicStatList[0];
            }
        }

        public float GetAttr2()
        {
            if (GetAssetData().basicStatTypeList.Count < 2)
            {
                return 0;
            }
            else
            {
                return GetAssetData().basicStatList[1];
            }
        }

        public int GetRealityPoint()
        {
            return GetAssetData().realityPoint;
        }

        public int GetDreamPoint()
        {
            return GetAssetData().dreamPoint;
        }

        public int GetIdealPoint()
        {
            return GetAssetData().idealPoint;
        }

        public int GetScore()
        {
            int result = 0;
            foreach (int _t in tagList)
            {
                result += Mathf.FloorToInt(TagManager.Instance.GetTag(_t).score / 10f);
            }
            result += qualityAffect;

            return result;
        }

        public Rank GetRank()
        {
            int score = GetScore();
            if (score < 30)
            {
                return Rank.F;
            }
            else
            {
                if (score < 40)
                {
                    return Rank.E;
                }
                else
                {

                    if (score < 50)
                    {
                        return Rank.D;
                    }
                    else
                    {

                        if (score < 60)
                        {
                            return Rank.C;
                        }
                        else
                        {

                            if (score < 70)
                            {
                                return Rank.B;
                            }
                            else
                            {

                                if (score < 80)
                                {
                                    return Rank.A;
                                }
                                else
                                {
                                    return Rank.S;
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    public class AssetTypeData
    {
        //id	name
        public int id;
        public LocalizedString name;
    }

    public class RecipeData
    {
        //id	shape	assets_type_id	target_tool_id
        public int id;
        public List<Vector2Int> shape;
        public List<int> assetTypeList;
        public int targetCompoundId;
        public List<int> targetScore;
        public List<int> targetTag;
        public List<Vector2Int> targetPos;
        public List<int> capacity;
        public int RequireAchievementsCount;
    }

    public class EnemyData
    {
        //id	name	hp	atk ats
        public int id;
        public LocalizedString name;
        public int hp;
        public int atk;
        public int ats;

        public CharacterAttribute ConvertToCharacterAttribute()
        {
            return CharacterAttribute.SetUpCharacterAttribute(hp,atk,ats);
        }
    }
}