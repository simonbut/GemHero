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

    }
}
