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
        public int id;
        public LocalizedString name;
        public LocalizedString description;

        public bool IsAchievementGet()
        {
            if (id == 0)
            {
                return true;
            }
            return Database.globalData.completedAchievementId.Contains((int)id);
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
        public int localIndex;
        public int tagDataId;
        public List<int> affectList = new List<int>();
        public Vector2Int offset = new Vector2Int();

        public TagData GetTagData()
        {
            return TagManager.Instance.GetTagData(tagDataId);
        }

        public List<Vector2Int> GetGrids()
        {
            List<Vector2Int> result = new List<Vector2Int>();
            foreach (Vector2Int _v in GetTagData().grids)
            {
                Vector2Int _resultV = _v + offset;
                //TODO affectList
                result.Add(_resultV);
            }
            return result;
        }

        static public Tag CreateTag(TagData _tagData, Vector2Int _offset, List<int> _affectList)
        {
            return CreateTag(_tagData.id, _offset, _affectList);
        }

        static public Tag CreateTag(int _tagDataId, Vector2Int _offset, List<int> _affectList)
        {
            Tag result = new Tag();
            result.tagDataId = _tagDataId;
            result.offset = _offset;
            result.affectList = _affectList;

            return result;
        }
    }

    public class DestinyShareData
    {
        //id	destinyShareId	tagId
        public int id;
        public int characterId;
        public int tagId;
    }

    public class TagData
    {
        //id	groupId	subId	name	description	grids	compound_type_list	score
        public int id;
        public LocalizedString name;
        public LocalizedString description;
        public List<Vector2Int> grids;
        public int score;
        public bool isBadTag;
        public TagType tagType;

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

    public class Player
    {
        public static int GetTotalHp()
        {
            float result = ParameterManager.Instance.GetParameter("basicPlayerHp");

            return Mathf.FloorToInt(result);
        }

        public static float GetBasicAtk()
        {
            float result = ParameterManager.Instance.GetParameter("basicPlayerAtk");

            foreach (int _e in Database.userDataJson.equipment)
            {
                if (_e == 0)
                {
                    continue;
                }
                AssetData _a = AssetManager.Instance.GetAssetByUid(_e).GetAssetData();
                for (int i = 0; i < _a.basicStatTypeList.Count; i++)
                {
                    if (_a.basicStatTypeList[i] == StatType.atk)
                    {
                        result += _a.basicStatList[i];
                    }
                }
            }

            return result * 100f;
        }

        public static float GetBasicAts()
        {
            float result = ParameterManager.Instance.GetParameter("basicPlayerAts");

            foreach (int _e in Database.userDataJson.equipment)
            {
                if (_e == 0)
                {
                    continue;
                }
                AssetData _a = AssetManager.Instance.GetAssetByUid(_e).GetAssetData();
                for (int i = 0; i < _a.basicStatTypeList.Count; i++)
                {
                    if (_a.basicStatTypeList[i] == StatType.ats)
                    {
                        result += _a.basicStatList[i];
                    }
                }
            }

            return result * 1000f;
        }

        public static float GetBasicDef()
        {
            float result = ParameterManager.Instance.GetParameter("basicPlayerDef");

            foreach (int _e in Database.userDataJson.equipment)
            {
                if (_e == 0)
                {
                    continue;
                }
                AssetData _a = AssetManager.Instance.GetAssetByUid(_e).GetAssetData();
                for (int i = 0; i < _a.basicStatTypeList.Count; i++)
                {
                    if (_a.basicStatTypeList[i] == StatType.def)
                    {
                        result += _a.basicStatList[i];
                    }
                }
            }

            return result * 100f;
        }

        public static List<int> GetVirtueGemList()
        {
            return Database.userDataJson.virtueGem;
        }
    }

    public class CharacterAttribute
    {
        float hpTotalPt = 500f;
        float defPt = 50f;
        float atkPt = 100f;
        float atsPt = 1000f;
        int ammoTotal = 10;
        int ammoReloadTier = 1;

        List<int> skillAffect = new List<int>();
        List<int> tagAffect = new List<int>();
        List<int> gemAffect = new List<int>();

        float criRate = 0f;
        float criDamIncrease = 0f;
        float blood = 0f;
        float allDamage = 0f;
        float directDamage = 0f;
        float dodgeRate = 0f;
        float lastHitDamgeIncrease = 0f;
        float lastHitCriRate = 0f;
        float lastHitDirectDamage = 0f;
        float countRate = 0f;

        public static CharacterAttribute SetUpCharacterAttribute(float _hpTotalPt,float _defPt, float _atkPt, float _atsPt,List<int> _skillAffect = null, List<int> _tagAffect = null, List<int> _gemAffect = null,int _ammoTotal = -1,int _ammoReloadTier = 1)
        {
            CharacterAttribute result = new CharacterAttribute();
            result.hpTotalPt = _hpTotalPt;
            result.defPt = _defPt;
            result.atkPt = _atkPt;
            result.atsPt = _atsPt;
            result.ammoTotal = _ammoTotal;
            result.ammoReloadTier = _ammoReloadTier;

            result.skillAffect = _skillAffect;
            result.tagAffect = _tagAffect;
            result.gemAffect = _gemAffect;

            return result;
        }

        public static CharacterAttribute SetUpCharacterAttributeByEnemyId(int _enemyId)
        {
            EnemyData _e = EnemyManager.Instance.GetEnemyData(_enemyId);
            return SetUpCharacterAttribute(_e.hp, _e.def, _e.atk, _e.ats, _e.skillList, null, null, _e.ammoCount, _e.ammoReloadTier);
        }

        public int GetAmmoReloadTier()
        {
            return ammoReloadTier;
        }

        public int GetAmmoTotal()
        {
            return ammoTotal;
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

        public float GetDef()
        {
            return defPt;
        }
    }

    public enum QuestType
    {
        talk,
        item,
        battle
    }

    public enum DialogType
    {
        afterDestinyShare,
        afterItemQuest,
        afterBattleQuest,
        normal,
        beforeMainBattleQuest,
        afterMainBattleQuest,
        talkQuest,
    }

    public enum ResourceType
    {
        collect,
        talk,
        mainQuest,
        special
    }

    public enum CompoundType
    {
        weapon = 0,
        accessory,
        consumable,
        compound,
        asset,
        clothing
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
        FixedTag
    }

    public class TalkDialogList
    {
        public int characterId;
        public ResourcePointData afterDestinyShare;
        public ResourcePointData afterItemQuest;
        public ResourcePointData afterBattleQuest;
        public ResourcePointData normal;
    }

    public class MainQuestDialogList
    {
        public int characterId;
        public ResourcePointData beforeMainBattleQuest;
        public ResourcePointData afterMainBattleQuest;
        public ResourcePointData talkQuest;
    }

    public class VirtueGemData
    {
        //id	name	description	RequireAchievementsCount	IsCriticalGem
        public int id;
        public LocalizedString name;
        public LocalizedString description;
        public int RequireAchievementsCount;
        public bool IsCriticalGem;
    }

    public class ResourcePointData
    {
        //id	resource_point_id	asset_id	must_have_tag_list	tag_pool	score_min	score_max
        public int id;
        public ResourceType resourceType;
        public int resourcePointId;
        public int assetId;
        public List<int> mustHaveTagList = new List<int>();
        public List<int> tagPool = new List<int>();
        public List<int> rareTagPool = new List<int>();
        public int scoreMin;
        public int scoreMax;
        public int characterId;
        public DialogType dialogType;
        public int targetDialogId;
        public int questId;
        public int afterQuestId;
    }

    public class CharacterData
    {
        public int id;
        public LocalizedString name;

        public List<int> tagList = new List<int>();
        public List<Vector2Int> tagPos = new List<Vector2Int>();
    }

    public class DialogData
    {
        //id	dialogId	step	content
        public int id;
        public int dialogId;
        public int step;
        public int characterId;
        public LocalizedString content;
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

        //public CompoundType compoundType;
        public List<StatType> basicStatTypeList;
        public List<int> basicStatList;

        public int ammoCount;
        public int ammoReloadTier;

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

        public CompoundType GetCompoundType()
        {
            foreach (int _id in assetTypeList)
            {
                if (_id == 2)
                {
                    return CompoundType.weapon;
                }
                if (_id == 4)
                {
                    return CompoundType.accessory;
                }
                if (_id == 5)
                {
                    return CompoundType.consumable;
                }
                if (_id == 6)
                {
                    return CompoundType.clothing;
                }
            }
            return CompoundType.compound;
        }
    }

    public class Asset
    {
        public int assetUid;
        public int assetId;
        public int qualityAffect;
        public List<int> tagList;
        public bool isConsumed = false;

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
                result += Mathf.FloorToInt(TagManager.Instance.GetTagData(_t).score);
            }
            result += qualityAffect;

            return result;
        }

        public int CalculateQualityAffectByQuality(int quality)
        {
            int originalQuality = 0;
            foreach (int _t in tagList)
            {
                originalQuality += Mathf.FloorToInt(TagManager.Instance.GetTagData(_t).score);
            }
            return (quality - originalQuality);
        }

        public Rank GetRank()
        {
            int rankScore = GetScore() - qualityAffect;
            if (rankScore < 30)
            {
                return Rank.F;
            }
            else
            {
                if (rankScore < 40)
                {
                    return Rank.E;
                }
                else
                {

                    if (rankScore < 50)
                    {
                        return Rank.D;
                    }
                    else
                    {

                        if (rankScore < 60)
                        {
                            return Rank.C;
                        }
                        else
                        {

                            if (rankScore < 70)
                            {
                                return Rank.B;
                            }
                            else
                            {

                                if (rankScore < 80)
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
        public int hpLoss;
        public int requireAchievementsCount;
    }

    public class EnemyData
    {
        //id	name	hp	atk ats
        public int id;
        public LocalizedString name;
        public int hp;
        public int def;
        public int atk;
        public int ats;
        public List<int> skillList;
        public int ammoCount;
        public int ammoReloadTier;

        public CharacterAttribute ConvertToCharacterAttribute()
        {
            return CharacterAttribute.SetUpCharacterAttribute(hp, def, atk, ats, skillList, null, null);
        }
    }

    public class EnemySkillData
    {
        public int id;
        public LocalizedString description;
    }

    public class QuestData
    {
        //id	description	timeLimit (Hour)
        public int id;
        public LocalizedString description;
        public int timeLimit;
        public QuestType questType;
        public int characterId;
        public int itemId;
        public int itemQuality;
        public int itemTag;
        public List<int> enemyList;

        public string GetDescription()
        {
            string result = Database.GetLocalizedText(description.GetString());
            if (characterId > 0)
            {
                result = result.Replace("$1", CharacterManager.Instance.GetCharacterData(characterId).name.GetString());
            }
            if (itemId > 0)
            {
                result = result.Replace("$2", AssetManager.Instance.GetAssetData(itemId).name.GetString());
            }
            if (itemQuality > 0)
            {
                result = result.Replace("$3", itemQuality.ToString("0"));
            }
            if (itemTag > 0)
            {
                result = result.Replace("$4", TagManager.Instance.GetTagData(itemTag).name.GetString());
            }
            if (id > 100 && id < 200)
            {
                result = "Side Mission " + (id % 100).ToString("0") + "A: " + result;
            }
            if (id > 200 && id < 300)
            {
                result = "Side Mission " + (id % 100).ToString("0") + "B: " + result;
            }
            return result;
        }
    }

    public class Quest
    {
        public int questId = 0;
        public int startTime;
    }
}