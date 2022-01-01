using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ClassHelper;

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

    public GameObject canvas;
    public GameObject characterObjectPrefab;

    public List<TurnBaseBattleCharacter> characterList;
    public int enemyCount = 0;

    public void StartBattle()
    {
        canvas.SetActive(true);

        //test
        AddCharacter(CharacterAttribute.SetUpCharacterAttribute(Database.userDataJson.hp, ParameterManager.Instance.GetParameter("basicPlayerDef"), ParameterManager.Instance.GetParameter("basicPlayerAtk"), 1000, null, new List<int>(), new List<int>(), 10,1), Force.player);
        AddCharacter(CharacterAttribute.SetUpCharacterAttributeByEnemyId(1), Force.enemy); 
        AddCharacter(CharacterAttribute.SetUpCharacterAttributeByEnemyId(1), Force.enemy);
        AddCharacter(CharacterAttribute.SetUpCharacterAttributeByEnemyId(1), Force.enemy);
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
        //Check Attack
        foreach (TurnBaseBattleCharacter _tbbc in characterList)
        {
            if (_tbbc == null)
            {
                continue;
            }
            _tbbc.AtbCharge();
            if (_tbbc.actionRefillPt >= 1f)
            {
                PerformAttack(_tbbc);
                _tbbc.ResetAtb();
            }
        }
    }

    void SetTarget(TurnBaseBattleCharacter from)
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

    List<TurnBaseBattleCharacter> GetCharacterList(Force _force)
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
            SetTarget(from);
        }
        if (from.target == null)//can't find target
        {
            return;
        }
        PerformAttack(from, from.target);
    }

    void PerformAttack(TurnBaseBattleCharacter from, TurnBaseBattleCharacter to)
    {
        from.ammoCount--;
        from.RunAttackAnimation();
        to.RunHurtAnimation();
        to.CalcuateAndGetDamage(from.characterAttribute.GetAtk());
    }
}
