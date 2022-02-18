using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ClassHelper;

public class TurnBaseBattleCharacter : MonoBehaviour
{
    [SerializeField] Image hpbar;
    [SerializeField] Image actionbar;
    [SerializeField] Image reloadTimerBar;
    [SerializeField] Image appearence;
    [SerializeField] Text hpText;

    [SerializeField] Text ammoText;
    [SerializeField] List<GameObject> ammoList;

    [SerializeField] Sprite basicSprite;
    [SerializeField] Sprite attackSprite;
    [SerializeField] GameObject gunFireAnimation;

    public float reloadTimer = 1;
    public float displayHpPt;
    public float hpPt;
    public float actionRefillPt;
    public int ammoCount;

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

            basicSprite = Resources.Load<Sprite>("Enemy/" + _characterAttribute.enemyId.ToString("000"));
            attackSprite = Resources.Load<Sprite>("Enemy/" + _characterAttribute.enemyId.ToString("000"));
        }

        hpPt = _characterAttribute.GetHpTotal();
        displayHpPt = hpPt;
        ReloadAmmo();

        if (characterAttribute.GetAmmoTotal() > 0)
        {
            ReloadAmmo();
        }
        else
        {
            ammoText.text = "";
            for (int i = 0; i < ammoList.Count; i++)
            {
                ammoList[i].SetActive(false);
            }
        }
    }

    public void ReloadAmmo()
    {
        ammoCount = characterAttribute.GetAmmoTotal();
        reloadTimer = 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (characterAttribute.GetAmmoTotal() > 0)
        {
            if (ammoCount > ammoList.Count)
            {
                ammoText.text = ammoCount.ToString("0");
            }
            else
            {
                ammoText.text = "";
            }
            for (int i = 0; i < ammoList.Count; i++)
            {
                ammoList[i].SetActive(ammoCount > i);
            }
            reloadTimerBar.transform.parent.gameObject.SetActive(reloadTimer < 1f);
            reloadTimerBar.fillAmount = 1f - reloadTimer;

            actionbar.transform.parent.gameObject.SetActive(ammoCount > 0);
        }

        switch (force)
        {
            case Force.player:
                hpbar.transform.parent.gameObject.SetActive(false);
                hpText.gameObject.SetActive(false);
                Database.userDataJson.hp = Mathf.FloorToInt(hpPt);
                break;
        }

        displayHpPt = Mathf.FloorToInt((displayHpPt * 4f + hpPt) / 5f);
        if (Mathf.Abs(displayHpPt - hpPt) < 3f)
        {
            displayHpPt = hpPt;
        }

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
        hpbar.fillAmount = displayHpPt / characterAttribute.GetHpTotal();
        hpText.text = displayHpPt.ToString("0");
    }

    void SetActionBar()
    {
        actionbar.fillAmount = actionRefillPt;
    }

    public void AtbCharge()
    {
        if (characterAttribute.GetAmmoTotal() > 0 && ammoCount <= 0)
        {
            AmmoCharge();
            return;
        }
        AtbCharge(characterAttribute.GetAts() * Time.deltaTime);
    }

    public void AmmoCharge()
    {
        switch (characterAttribute.GetAmmoReloadTier())
        {
            case 1:
            default:
                reloadTimer -= 0.5f * Time.deltaTime;
                break;
        }
        if (reloadTimer <= 0f)
        {
            ReloadAmmo();
        }
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

    public void MouseEnter()
    {
        TurnBaseBattleView.Instance.ShowEnemyInformation(this);
    }

    public void MouseExit()
    {
        TurnBaseBattleView.Instance.HideEnemyInformation();
    }

    public void MouseDown()
    {
        TurnBaseBattleView.Instance.AimEnemy(this);
    }
}
