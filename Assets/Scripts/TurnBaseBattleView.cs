using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ClassHelper;
using CharacterAttributeClass;

public class TurnBaseBattleView : MonoBehaviour
{
    #region instance
    private static TurnBaseBattleView m_instance;

    public static TurnBaseBattleView Instance
    {
        get
        {
            return m_instance;
        }
    }

    void Awake()
    {
        if (TurnBaseBattleView.Instance == null)
        {
            m_instance = this;
        }
    }
    #endregion

    public TurnBaseBattleCanvas canvas;
    public GameObject characterObjectPrefab;

    public List<TurnBaseBattleCharacter> characterList;
    public int enemyCount = 0;

    public int questId = 0;
    public int monsterPoint = 0;

    public bool isWin = false;
    public bool isLose = false;

    public void ShowEnemyInformation(TurnBaseBattleCharacter _character)
    {
        canvas.ShowEnemyInformation(_character);
    }

    public void HideEnemyInformation()
    {
        canvas.HideEnemyInformation();
    }

    public void AimEnemy(TurnBaseBattleCharacter _character)
    {
        SelectTarget(_character);
    }

    public void ResetField()
    {
        enemyCount = 0;
        foreach (TurnBaseBattleCharacter _c in characterList)
        {
            if (_c == null)
            {
                continue;
            }
            Destroy(_c.gameObject);
        }
        characterList = new List<TurnBaseBattleCharacter>();
    }


    public void StartBattleByMonsterPoint(int _monsterPontId)
    {
        ResetField();

        canvas.AddUI();
        questId = 0;
        monsterPoint = _monsterPontId;

        List<ResourcePointData> _rp = ResourcePointManager.Instance.GetResourcePointDataList(_monsterPontId);
        AssetData _a = AssetManager.Instance.GetAssetByUid(Database.userDataJson.equipment[0]).GetAssetData();

        AddCharacter(CharacterAttribute.SetUpCharacterAttribute(0, Database.userDataJson.hp, Player.GetBasicDef(), Player.GetBasicAtk(), Player.GetBasicAts(), null, Database.userDataJson.playerTags, Database.userDataJson.virtueGem, Database.userDataJson.equipment , _a.ammoCount, _a.ammoReloadTier), Force.player);

        print(_rp[0].enemyList.Count);
        for (int i = 0; i < _rp[0].enemyList.Count; i++)
        {
            print(_rp[0].enemyList[i]);
            AddCharacter(CharacterAttribute.SetUpCharacterAttributeByEnemyId(_rp[0].enemyList[i]), Force.enemy);
        }
    }


    public void StartBattleByQuest(int _questId)
    {
        ResetField();

        canvas.AddUI();
        questId = _questId;
        monsterPoint = 0;

        QuestData _q = QuestManager.Instance.GetQuestData(questId);
        AssetData _a = AssetManager.Instance.GetAssetByUid(Database.userDataJson.equipment[0]).GetAssetData();

        AddCharacter(CharacterAttribute.SetUpCharacterAttribute(0, Database.userDataJson.hp, Player.GetBasicDef(), Player.GetBasicAtk(), Player.GetBasicAts(), null, Database.userDataJson.playerTags, Database.userDataJson.virtueGem, Database.userDataJson.equipment, _a.ammoCount, _a.ammoReloadTier), Force.player);

        for (int i = 0; i < _q.enemyList.Count; i++)
        {
            AddCharacter(CharacterAttribute.SetUpCharacterAttributeByEnemyId(_q.enemyList[i]), Force.enemy);
        }
    }

    void AddCharacter(CharacterAttribute _characterAttribute, Force _force)
    {
        GameObject characterObjectInstance = Instantiate(characterObjectPrefab);
        characterObjectInstance.transform.SetParent(canvas.transform);

        TurnBaseBattleCharacter _tbbc = characterObjectInstance.GetComponent<TurnBaseBattleCharacter>();
        _tbbc.InitCharacter(_characterAttribute, _force);
        characterList.Add(_tbbc);

        //determine position
        if (_force == Force.player)
        {
            characterObjectInstance.transform.localPosition = Vector3.left * 150f;
        }
        else
        {
            enemyCount++;
            switch (enemyCount)
            {
                case 1:
                    characterObjectInstance.transform.localPosition = new Vector2(150, 0);
                    break;
                case 2:
                    characterObjectInstance.transform.localPosition = new Vector2(300, 150);
                    break;
                case 3:
                    characterObjectInstance.transform.localPosition = new Vector2(300, -150);
                    break;
            }
        }
        //
    }

    // Update is called once per frame
    void Update()
    {
        if (!UIManager.Instance.IsCurrentUI(canvas))
        {
            return;
        }

        //Check Attack
        foreach (TurnBaseBattleCharacter _tbbc in characterList)
        {
            if (_tbbc == null)
            {
                continue;
            }
            _tbbc.AtbCharge();
            if (_tbbc.actionRefillPt >= _tbbc.actionRefillRequire)
            {
                PerformAttack(_tbbc);
                _tbbc.ResetAtb();
            }
        }

        CheckLose();
        CheckWin();

        if (isWin)
        {
            canvas.OnBackPressed();
            if (monsterPoint > 0)
            {
                MainGameView.Instance.assetConfirmCanvas.AddUI(new List<Asset> { ResourcePointManager.Instance.DrawAsset(monsterPoint), ResourcePointManager.Instance.DrawAsset(monsterPoint), ResourcePointManager.Instance.DrawAsset(monsterPoint) });
            }
            if (questId > 0)
            {
                if (QuestManager.Instance.GetQuestData(questId).questType == QuestType.mainBattle)
                {
                    MainGameView.Instance.StageClear();
                }
                else
                {
                    MainGameView.Instance.rewardAfterBattleCanvas.AddUI(questId < 100);
                }
            }
        }

        if (isLose)
        {
            MainGameView.Instance.GameOver();
        }
    }

    void CheckLose()
    {
        foreach (TurnBaseBattleCharacter _tbbc in characterList)
        {
            if (_tbbc.force == Force.player)
            {
                if (_tbbc.hpPt <= 0)
                {
                    isLose = true;
                }
            }
        }
    }

    void CheckWin()
    {
        if (isLose)
        {
            return;
        }
        bool isWinResult = true;
        foreach (TurnBaseBattleCharacter _tbbc in characterList)
        {
            if (_tbbc.force == Force.enemy)
            {
                if (_tbbc.hpPt > 0)
                {
                    isWinResult = false;
                }
            }
        }
        isWin = isWinResult;
    }

    void SelectTarget(TurnBaseBattleCharacter _enemy)
    {
        if (_enemy.force == Force.player)
        {
            return;
        }
        List<TurnBaseBattleCharacter> _cl = GetCharacterList(Force.player);
        if (_cl.Count <= 0)
        {
            return;
        }
        _cl[0].target = _enemy;
    }

    void RandomTarget(TurnBaseBattleCharacter from)
    {
        if (from.force == Force.player)
        {
            List<TurnBaseBattleCharacter> _cl = GetCharacterList(Force.enemy);
            int ran = Random.Range(0,_cl.Count);
            if (_cl.Count > 0)
            {
                from.target = _cl[ran];
            }
        }
        else
        {
            List<TurnBaseBattleCharacter> _cl = GetCharacterList(Force.player);
            int ran = Random.Range(0, _cl.Count);
            if (_cl.Count > 0)
            {
                from.target = _cl[ran];
            }
        }
    }

    public List<TurnBaseBattleCharacter> GetCharacterList(Force _force)
    {
        List<TurnBaseBattleCharacter> result = new List<TurnBaseBattleCharacter>();
        foreach (TurnBaseBattleCharacter _tbbc in characterList)
        {
            if (_tbbc == null)
            {
                continue;
            }
            if (_tbbc.force == _force)
            {
                result.Add(_tbbc);
            }
        }
        return result;
    }

    void PerformAttack(TurnBaseBattleCharacter from)
    {
        if (from.target == null)
        {
            RandomTarget(from);
        }
        if (from.target == null)//can't find target
        {
            return;
        }
        PerformAttack(from, from.target);
    }

    void PerformAttack(TurnBaseBattleCharacter from, TurnBaseBattleCharacter to)
    {
        from.RunAttackAnimation();
        to.RunHurtAnimation();
        if (from.characterAttribute.IsDoubleHit())
        {
            to.CalcuateAndGetDamage(from,0.7f);
            from.ammoCount--;
            if (from.ammoCount>0)
            {
                to.CalcuateAndGetDamage(from,0.7f);
                from.ammoCount--;
            }
        }
        else
        {
            to.CalcuateAndGetDamage(from);
            from.ammoCount--;
        }
    }
}
