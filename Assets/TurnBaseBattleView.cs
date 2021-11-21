using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ClassHelper;

public class TurnBaseBattleView : MonoBehaviour
{
    public GameObject canvas;
    public GameObject characterObjectPrefab;

    public List<TurnBaseBattleCharacter> characterList;
    public int enemyCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        //test
        AddCharacter(new CharacterAttribute(), Force.player);
        AddCharacter(new CharacterAttribute(), Force.enemy);
        AddCharacter(new CharacterAttribute(), Force.enemy);
        AddCharacter(new CharacterAttribute(), Force.enemy);
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
            characterObjectInstance.transform.localPosition = Vector3.left * 200f;
        }
        else
        {
            enemyCount++;
            switch (enemyCount)
            {
                case 1:
                    characterObjectInstance.transform.localPosition = new Vector2(200, 0);
                    break;
                case 2:
                    characterObjectInstance.transform.localPosition = new Vector2(350, 100);
                    break;
                case 3:
                    characterObjectInstance.transform.localPosition = new Vector2(350, -100);
                    break;
            }
        }
        //
    }

    // Update is called once per frame
    void Update()
    {
        //Set Attack Target
        //foreach (TurnBaseBattleCharacter _tbbc in characterList)
        //{
        //    if (_tbbc.target == null)
        //    {
        //        SetTarget(_tbbc);
        //    }
        //}

        //Check Attack
        foreach (TurnBaseBattleCharacter _tbbc in characterList)
        {
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
            from.target = _cl[ran];
        }
        else
        {
            List<TurnBaseBattleCharacter> _cl = GetCharacterList(Force.player);
            int ran = Random.Range(0, _cl.Count);
            from.target = _cl[ran];
        }
    }

    List<TurnBaseBattleCharacter> GetCharacterList(Force _force)
    {
        List<TurnBaseBattleCharacter> result = new List<TurnBaseBattleCharacter>();
        foreach (TurnBaseBattleCharacter _tbbc in characterList)
        {
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
        to.GetDamage(from.characterAttribute.GetAtk());
    }
}
