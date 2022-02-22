using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using CharacterAttributeClass;

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
        public bool isReady = true;

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

        public int GetSize()
        {
            int result = GetGrids().Count;

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
        public int minValue;
        public int maxValue;
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
        public static float GetWireLengthLimit()
        {
            float result = ParameterManager.Instance.GetParameter("basicWireLength");
            foreach (Tag _t in Database.userDataJson.playerTags)
            {
                if (_t.isReady)
                {
                    result += _t.GetSize();
                }
            }

            return result;
        }

        public static int GetHp()
        {
            return Database.userDataJson.hp;
        }

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
                Asset _a = AssetManager.Instance.GetAssetByUid(_e);
                for (int i = 0; i < _a.GetAssetData().basicStatTypeList.Count; i++)
                {
                    if (_a.GetAssetData().basicStatTypeList[i] == StatType.atk)
                    {
                        result += _a.GetAttr()[i];
                    }
                }
            }

            return result;
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
                Asset _a = AssetManager.Instance.GetAssetByUid(_e);
                for (int i = 0; i < _a.GetAssetData().basicStatTypeList.Count; i++)
                {
                    if (_a.GetAssetData().basicStatTypeList[i] == StatType.ats)
                    {
                        result += _a.GetAttr()[i];
                    }
                }
            }

            return result;
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
                Asset _a = AssetManager.Instance.GetAssetByUid(_e);
                for (int i = 0; i < _a.GetAssetData().basicStatTypeList.Count; i++)
                {
                    if (_a.GetAssetData().basicStatTypeList[i] == StatType.def)
                    {
                        result += _a.GetAttr()[i];
                    }
                }
            }

            return result;
        }

        public static List<int> GetVirtueGemList()
        {
            return Database.userDataJson.virtueGem;
        }
    }
    public enum QuestType
    {
        talk,
        item,
        battle,
        mainBattle,
        mainRelease,
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
        beforeMainReleaseQuest,
        afterMainReleaseQuest,
    }

    public enum ResourceType
    {
        collect,
        talk,
        mainQuest,
        special,
        changePos,
        library,
        monster,
    }

    public enum CompoundType
    {
        weapon = 0,
        accessory,
        consumable,
        compound,
        asset,
        clothing,
    }

    public enum StatType
    {
        atk = 0,
        ats,
        hp,
        def,
    }

    public enum Rank
    {
        F = 0,
        E,
        D,
        C,
        B,
        A,
        S,
    }

    public enum TagType
    {
        AssetTag = 0,
        CharacterTag,
        FixedTag,
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
        public ResourcePointData beforeMainReleaseQuest;
        public ResourcePointData afterMainReleaseQuest;
    }

    public class VirtueGemData
    {
        //id	name	description	RequireAchievementsCount	IsCriticalGem
        public int id;
        public LocalizedString name;
        public LocalizedString description;
        public List<int> appearStage;
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
        public int mapId;
        public Vector2 pos;
        public List<int> bookId;
        public List<int> enemyList;
        public bool disappearAfter;
    }

    public class BookData
    {
        //id	name	description	recipeList	time(hr)
        public int id;
        public LocalizedString name;
        public LocalizedString description;
        public List<int> recipeList = new List<int>();
        public int time;

        public string GetBookDescription()
        {
            string result = "";

            for (int i = 0; i < recipeList.Count; i++)
            {
                if (i > 0)
                {
                    result += "\n";
                }
                RecipeData _rd = AssetManager.Instance.GetRecipeData(recipeList[i]);
                result += (i + 1).ToString("0") + ". ";
                result += AssetManager.Instance.GetAssetData(_rd.targetCompoundId).name.GetString();
                result += " = ";
                for (int j = 0; j < _rd.assetTypeList.Count; j++)
                {
                    if (j > 0)
                    {
                        result += " + ";
                    }
                    result += AssetManager.Instance.GetRecipeAssetName(_rd.assetTypeList[j]);
                }
            }

            return result;
        }
    }

    public class CharacterData
    {
        public int id;
        public LocalizedString name;

        //public List<int> tagList = new List<int>();
        //public List<Vector2Int> tagPos = new List<Vector2Int>();
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
        public List<int> basicStatListMin;
        public List<int> basicStatListMax;

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
                if (_id == 10002)
                {
                    return CompoundType.weapon;
                }
                if (_id == 10004)
                {
                    return CompoundType.accessory;
                }
                if (_id == 10005)
                {
                    return CompoundType.consumable;
                }
                if (_id == 10006)
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
            return Mathf.Min(AssetManager.Instance.CalculateQuality(tagList, qualityAffect), 100);
        }

        public AssetData GetAssetData()
        {
            return AssetManager.Instance.GetAssetData(assetId);
        }

        public List<float> GetAttr()
        {
            List<float> result = new List<float>();
            result.Add(GetAttr1());
            result.Add(GetAttr2());

            return result;
        }

        public float GetAttr1()
        {
            if (GetAssetData().basicStatTypeList.Count < 1)
            {
                return 0;
            }
            else
            {
                return GetAssetData().basicStatListMin[0] + (GetAssetData().basicStatListMax[0] - GetAssetData().basicStatListMin[0]) * GetQuality() / 100f;
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
                return GetAssetData().basicStatListMin[1] + (GetAssetData().basicStatListMax[1] - GetAssetData().basicStatListMin[1]) * GetQuality() / 100f;
            }
        }

        public int GetRealityPoint()
        {
            int result = GetAssetData().realityPoint;
            if (GetAssetData().realityPoint <= 0)
            {
                result = -GetAssetData().dreamPoint;
            }
            foreach (int _t in tagList)
            {
                switch (_t)
                {
                    case 2://空虛
                        return 0;
                        break;
                    case 5://夢想 (小)
                        result--;
                        break;
                    case 9://現實 (小)
                        result++;
                        break;
                }
            }
            return result;
        }

        //public int GetDreamPoint()
        //{
        //    int result = GetAssetData().dreamPoint;
        //    foreach (int _t in tagList)
        //    {
        //        switch (_t)
        //        {
        //            case 2://空虛
        //                return 0;
        //                break;
        //            case 5://夢想 (小)
        //                result++;
        //                break;
        //        }
        //    }
        //    return result;
        //}

        public int GetIdealPoint()
        {
            int result = GetAssetData().idealPoint;
            foreach (int _t in tagList)
            {
                switch (_t)
                {
                    case 2://空虛
                        return 0;
                        break;
                    case 10://理想 (小)
                        result++;
                        break;
                    case 11://理想抑制 (小)
                        result--;
                        break;
                }
            }
            return Mathf.Max(0, result);
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
        public int lockUntilStage;
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
            return CharacterAttribute.SetUpCharacterAttribute(id, hp, def, atk, ats, skillList, null, null, null);
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
        public int targetRelease;
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
            if (targetRelease > 0)
            {
                result = result.Replace("$5", targetRelease.ToString("0"));
            }
            if (id > 100 && id < 200)
            {
                result = "Side Mission " + (id % 100).ToString("0") + " (1/2): " + result;
            }
            if (id > 200 && id < 300)
            {
                result = "Side Mission " + (id % 100).ToString("0") + " (2/2): " + result;
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