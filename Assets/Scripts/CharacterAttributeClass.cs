using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClassHelper;

namespace CharacterAttributeClass
{
    public class CharacterAttribute
    {
        public int enemyId = 0;

        float hpTotalPt = 500f;
        float defPt = 50f;
        float atkPt = 100f;
        float atsPt = 1000f;
        int ammoTotal = 10;
        int ammoReloadTier = 1;

        public List<int> skillAffect = new List<int>();
        public List<Tag> tagAffect = new List<Tag>();
        public List<int> gemAffect = new List<int>();
        public List<int> equipmentUidList = new List<int>();

        float initialCritRate = 0f;
        float initialCritDamMultiplier = 2f;
        float initialBlood = 0f;
        float allDamage = 0f;
        float initialDirectDamage = 0f;
        float dodgeRate = 0f;
        float initialLastHitDamgeIncrease = 0f;
        float lastHitCriRate = 0f;
        float countRate = 0f;
        float initialAmmoReloadSpeedAdd = 0f;

        public float GetLastHitDamgeIncrease()
        {
            float result = initialLastHitDamgeIncrease;

            if (gemAffect != null)
            {
                foreach (int _g in gemAffect)
                {
                    switch (_g)
                    {
                        case 5://關鍵一發
                            result += 50f;
                            break;
                    }
                }
            }

            return result;
        }

        public float GetBlood()
        {
            float result = initialBlood;

            if (skillAffect != null)
            {
                foreach (int _s in skillAffect)
                {
                    switch (_s)
                    {
                        case 4://吸血 +20%
                            result += 0.2f;
                            break;
                    }
                }
            }

            if (gemAffect != null)
            {
                foreach (int _g in gemAffect)
                {
                    switch (_g)
                    {
                        case 4://吸血 +10%
                            result += 0.1f;
                            break;
                    }
                }
            }

            if (equipmentUidList != null)
            {
                foreach (int _e in equipmentUidList)
                {
                    if (_e == 0)
                    {
                        continue;
                    }
                    foreach (int _t in AssetManager.Instance.GetAssetByUid(_e).tagList)
                    {
                        switch (_t)
                        {
                            case 31://吸血 +10%
                                result += 0.1f;
                                break;
                        }
                    }
                }
            }

            return result;
        }

        public float GetAmmoReloadSpeedAdd()
        {
            float result = initialAmmoReloadSpeedAdd;

            if (tagAffect != null)
            {
                foreach (Tag _t in tagAffect)
                {
                    switch (_t.tagDataId)
                    {
                        case 21://集中
                            result += 0.5f;
                            break;
                    }
                }
            }

            if (equipmentUidList != null)
            {
                foreach (int _e in equipmentUidList)
                {
                    if (_e == 0)
                    {
                        continue;
                    }
                    foreach (int _t in AssetManager.Instance.GetAssetByUid(_e).tagList)
                    {
                        switch (_t)
                        {
                            case 31://上彈速度 +50%
                                result += 0.5f;
                                break;
                        }
                    }
                }
            }

            return result;
        }

        public float GetDirectDamage()
        {
            float result = initialDirectDamage;

            if (tagAffect != null)
            {
                foreach (Tag _t in tagAffect)
                {
                    switch (_t.tagDataId)
                    {
                        case 18://人渣
                            result += 5f;
                            break;
                        case 24://有病
                            result += 5f;
                            break;
                    }
                }
            }

            if (gemAffect != null)
            {
                foreach (int _g in gemAffect)
                {
                    switch (_g)
                    {
                        case 7://穿透傷害 +15
                            result += 15f;
                            break;
                    }
                }
            }

            return result;
        }

        public float GetCriRate()
        {
            float result = initialCritRate;

            if (tagAffect != null)
            {
                foreach (Tag _t in tagAffect)
                {
                    switch (_t.tagDataId)
                    {
                        case 20://一事無成
                            result += 0.25f;
                            break;
                    }
                }
            }

            if (equipmentUidList != null)
            {
                foreach (int _e in equipmentUidList)
                {
                    if (_e == 0)
                    {
                        continue;
                    }
                    foreach (int _t in AssetManager.Instance.GetAssetByUid(_e).tagList)
                    {
                        switch (_t)
                        {
                            case 31://裝飾3A
                                result += 0.25f;
                                break;
                            case 3://會心上升 (小)
                            case 4://會心上升 (中)
                                if (AssetManager.Instance.GetAssetByUid(_e).GetAssetData().assetTypeList.Contains(10002))
                                {
                                    result += AssetManager.Instance.GetAssetByUid(_e).GetQuality() / 100f * (TagManager.Instance.GetTagData(_t).maxValue - TagManager.Instance.GetTagData(_t).minValue) + TagManager.Instance.GetTagData(_t).minValue;
                                }
                                break;
                        }
                    }
                }
            }

            return result;
        }

        public float GetCriDamMultiplier()
        {
            float result = initialCritDamMultiplier;

            if (tagAffect != null)
            {
                foreach (Tag _t in tagAffect)
                {
                    switch (_t.tagDataId)
                    {
                    }
                }
            }

            if (gemAffect != null)
            {
                foreach (int _g in gemAffect)
                {
                    switch (_g)
                    {
                        case 3://暴擊傷害 +40%
                            result += 0.4f;
                            break;
                    }
                }
            }

            if (equipmentUidList != null)
            {
                foreach (int _e in equipmentUidList)
                {
                    if (_e == 0)
                    {
                        continue;
                    }
                    foreach (int _t in AssetManager.Instance.GetAssetByUid(_e).tagList)
                    {
                        switch (_t)
                        {
                            case 31://裝飾3A
                                result += 0.4f;
                                break;
                        }
                    }
                }
            }

            return result;
        }

        public string GetEnemyInformation()
        {
            //$1 is hp
            string result = "";
            result += "HP: " + "$1" + " / " + GetHpTotal().ToString("0") + "\n";
            result += "Def: " + GetDef().ToString("0") + "\n";
            result += "Atk: " + GetAtk().ToString("0.0") + "\n";
            result += "Ats: " + GetAts().ToString("0.0") + "\n";
            result += "\n";
            result += "\n";

            if (skillAffect != null)
            {
                foreach (int _skill in skillAffect)
                {
                    if(_skill == 0)
                    {
                        continue;
                    }
                    EnemySkillData _esd = EnemyManager.Instance.GetEnemySkillData(_skill);
                    result += _esd.description.GetString() + "\n" + "\n" + "\n";
                }
            }

            return result;
        }

        public string GetPlayerInformation()
        {
            //$1 is hp, $2 is ammo count
            string result = "";
            result += "HP: " + "$1" + " / " + GetHpTotal().ToString("0") + "\n";
            result += "Def: " + GetDef().ToString("0") + "\n";
            result += "Atk: " + GetAtk().ToString("0") + "\n";
            result += "Ats: " + GetAts().ToString("0") + "\n";
            result += "Ammo Count: " + "$2" + " / " + GetAmmoTotal().ToString("0") + "\n";
            result += "Reload Speed Tier: " + GetAmmoReloadTier().ToString("0") + "\n";
            result += "\n";

            //TODO: List out criRate to countRate if they > 0
            if (GetCriRate() > 0)
            {
                result += "暴擊率: " + GetCriRate().ToString("P1") + "\n";
            }
            if (GetCriRate() > 0 || GetCriDamMultiplier() > 0)
            {
                result += "暴擊傷害: " + GetCriDamMultiplier().ToString("P1") + "\n";
            }
            if (GetBlood() > 0)
            {
                result += "吸血: " + GetBlood().ToString("P1") + "\n";
            }
            if (GetDirectDamage() > 0)
            {
                result += "穿透傷害: " + GetDirectDamage().ToString("0") + "\n";
            }
            if (GetLastHitDamgeIncrease() > 0)
            {
                result += "最後一發傷害: " + GetLastHitDamgeIncrease().ToString("0") + "\n";
            }
            if (GetAmmoReloadSpeedAdd() > 0)
            {
                result += "上彈速度: " + GetAmmoReloadSpeedAdd().ToString("0") + "\n";
            }

            return result;
        }

        public static CharacterAttribute SetUpCharacterAttribute(int _enemyId, float _hpTotalPt, float _defPt, float _atkPt, float _atsPt, List<int> _skillAffect = null, List<Tag> _tagAffect = null, List<int> _gemAffect = null, List<int> _equipmentUidList = null, int _ammoTotal = -1, int _ammoReloadTier = 1)
        {
            CharacterAttribute result = new CharacterAttribute();
            result.enemyId = _enemyId;

            result.hpTotalPt = _hpTotalPt;
            result.defPt = _defPt;
            result.atkPt = _atkPt;
            result.atsPt = _atsPt;
            result.ammoTotal = _ammoTotal;
            result.ammoReloadTier = _ammoReloadTier;

            result.skillAffect = _skillAffect;
            result.tagAffect = _tagAffect;
            result.gemAffect = _gemAffect;
            result.equipmentUidList = _equipmentUidList;

            return result;
        }

        public static CharacterAttribute SetUpCharacterAttributeByEnemyId(int _enemyId)
        {
            EnemyData _e = EnemyManager.Instance.GetEnemyData(_enemyId);
            return SetUpCharacterAttribute(_enemyId, _e.hp, _e.def, _e.atk, _e.ats, _e.skillList, null, null, null, _e.ammoCount, _e.ammoReloadTier);
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
            float result = atkPt;
            float multiplier = 1f;

            if (tagAffect != null)
            {
                foreach (Tag _t in tagAffect)
                {
                    switch (_t.tagDataId)
                    {
                        case 17://溫柔
                            result -= 5f;
                            break;
                    }
                }
            }

            if (equipmentUidList != null)
            {
                foreach (int _e in equipmentUidList)
                {
                    if (_e == 0)
                    {
                        continue;
                    }
                    foreach (int _t in AssetManager.Instance.GetAssetByUid(_e).tagList)
                    {
                        switch (_t)
                        {
                            case 30://裝飾2B
                                multiplier += 0.15f;
                                break;
                            case 32://裝飾3B
                                multiplier += 0.15f;
                                break;
                        }
                    }
                }
            }

            return result * multiplier;
        }

        public float GetAts()
        {
            float result = atsPt / 1000f;
            if (tagAffect != null)
            {
                foreach (Tag _t in tagAffect)
                {
                    switch (_t.tagDataId)
                    {
                        case 2://娘娘腔
                            result *= 0.9f;
                            break;
                    }
                }
            }
            return result;
        }

        public float GetDef()
        {
            return defPt;
        }

        public bool IsDoubleHit()
        {

            if (equipmentUidList != null)
            {
                foreach (int _e in equipmentUidList)
                {
                    if (_e == 0)
                    {
                        continue;
                    }
                    foreach (int _t in AssetManager.Instance.GetAssetByUid(_e).tagList)
                    {
                        switch (_t)
                        {
                            case 25://連射
                                return true;
                                break;
                        }
                    }
                }
            }

            return false;
        }

        public bool IsLastHitCrit()
        {

            if (equipmentUidList != null)
            {
                foreach (int _e in equipmentUidList)
                {
                    if (_e == 0)
                    {
                        continue;
                    }
                    foreach (int _t in AssetManager.Instance.GetAssetByUid(_e).tagList)
                    {
                        switch (_t)
                        {
                            case 25://補刀
                                return true;
                                break;
                        }
                    }
                }
            }

            return false;
        }

        public bool IsFirstHitCrit()
        {
            if (gemAffect != null)
            {
                foreach (int _g in gemAffect)
                {
                    switch (_g)
                    {
                        case 3://狙擊
                            return true;
                            break;
                    }
                }
            }

            return false;
        }
    }

}
