using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ClassHelper;

public class TurnBaseBattleCharacter : MonoBehaviour
{
    [SerializeField] Image hpbar;
    [SerializeField] Image actionbar;
    [SerializeField] Image appearence;

    public float hpPt;
    public float actionRefillPt;

    public CharacterAttribute characterAttribute;
    Force force;//0 = player, 1 = enemy

    public void InitCharacter(CharacterAttribute _characterAttribute, Force _force)
    {
        characterAttribute = _characterAttribute;
        force = _force;
        if (force == Force.enemy)
        {
            appearence.transform.localScale = new Vector2(-appearence.transform.localScale.x, appearence.transform.localScale.y);
        }

        hpPt = _characterAttribute.GetHpTotal();

        //TODO: set up character appearence
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SetHpBar();
        SetActionBar();
    }

    void SetHpBar()
    {
        hpbar.fillAmount = hpPt / characterAttribute.GetHpTotal();
    }

    void SetActionBar()
    {
        actionbar.fillAmount = actionRefillPt;
    }
}
