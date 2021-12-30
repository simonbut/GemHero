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

    [SerializeField] Sprite basicSprite;
    [SerializeField] Sprite attackSprite;
    [SerializeField] GameObject gunFireAnimation;

    public float hpPt;
    public float actionRefillPt;


    public CharacterAttribute characterAttribute;
    public Force force;//0 = player, 1 = enemy
    public TurnBaseBattleCharacter target;

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

        if (attackAnimationTimer >= 1f)
        {
            attackAnimation = false;
        }
        if (hurtAnimationTimer >= 1f)
        {
            hurtAnimation = false;
        }

        appearence.transform.localPosition = Vector2.zero;
        appearence.GetComponent<Image>().sprite = basicSprite;
        if (attackAnimation)
        {
            attackAnimationTimer += Time.deltaTime * 2f;
            appearence.GetComponent<Image>().sprite = attackSprite;
        }
        else
        {
            //step back
            if (hurtAnimation)
            {
                appearence.transform.localPosition = new Vector2(-Mathf.Sin(hurtAnimationTimer * Mathf.PI), 0) * 3f;
            }
        }

        //hurt color change
        if (hurtAnimation)
        {
            hurtAnimationTimer += Time.deltaTime * 2f;
            float pTemp = Mathf.Sin(hurtAnimationTimer * Mathf.PI);
            appearence.GetComponent<Image>().color = Color.Lerp(Color.white, Color.red, pTemp);
        }
    }

    void SetHpBar()
    {
        hpbar.fillAmount = hpPt / characterAttribute.GetHpTotal();
    }

    void SetActionBar()
    {
        actionbar.fillAmount = actionRefillPt;
    }

    public void AtbCharge()
    {
        AtbCharge(characterAttribute.GetAts() / 1000f * Time.deltaTime);
    }

    public void AtbCharge(float chargeAmount)
    {
        actionRefillPt += chargeAmount;
    }

    public void ResetAtb()
    {
        actionRefillPt = 0;
    }

    public void CalcuateAndGetDamage(float _enemyAtk)
    {
        float result = _enemyAtk / 4f + Mathf.Max(0, _enemyAtk / 2f - characterAttribute.GetDef()) + Mathf.Max(0, _enemyAtk / 2f - characterAttribute.GetDef() / 2f);
        GetDamage(result);
    }

    public void GetDamage(float _amount)
    {
        hpPt -= _amount;
        if (hpPt <= 0)
        {
            DestroyCharacter();
        }
    }

    public void DestroyCharacter()
    {
        Destroy(gameObject);
    }

    bool attackAnimation = false;
    float attackAnimationTimer = 0f;
    public void RunAttackAnimation()
    {
        attackAnimation = true;
        attackAnimationTimer = 0f;

        gunFireAnimation.SetActive(true);
    }

    bool hurtAnimation = false;
    float hurtAnimationTimer = 0f;
    public void RunHurtAnimation()
    {
        hurtAnimation = true;
        hurtAnimationTimer = 0f;
    }
}
