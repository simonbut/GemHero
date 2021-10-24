using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlableUI : MonoBehaviour
{
    public int fadeInType = 0;
    public bool isHideLastUI = true;
    Coroutine b;
    void Awake()
    {
        //AddUI();
    }

    public virtual void OnBackPressed()
    {
        if (UIManager.Instance.IsCurrentUI(this))
        {
            OnRemoveUI();
            //UIManager.Instance.itemChoosingIndicator.Hide();
        }
    }

    Vector2 originalScale = Vector2.zero;
    public virtual void AddUI()
    {
        //AudioManager.instance.PlaySFX(AudioManager.instance.uI_Pop_up_Menu);

        if (originalScale.magnitude == 0)
        {
            originalScale = transform.localScale;
        }
        //AudioManager.instance.PlayAudioButtonClick();

        //UIManager.Instance.itemChoosingIndicator.Hide();

        Database.SetDescriptionMessage("");
        UIManager.Instance.OnEnterNewUI();
        UIManager.Instance.AddUI(this);
        if (isHideLastUI)
        {
            UIManager.Instance.HideLastUI();
        }
        gameObject.SetActive(true);
        OnShow();
    }

    IEnumerator OpenUICoroutine()
    {
        float timer = 0;
        float timerTotal = 0.2f;
        while (timer < timerTotal)
        {
            timer += Time.unscaledDeltaTime;

            switch (fadeInType)
            {
                case 0:
                    transform.localScale = new Vector2(originalScale.x, originalScale.y) * MathManager.BoundingFloat(timer, 0.9f, 1f, timerTotal);
                    break;
                case 1:
                case 2:
                    transform.localPosition = new Vector2(MathManager.BoundingFloat(timer, -200f, 0f, timerTotal, 100f), 0f);
                    break;
            }

            yield return new WaitForEndOfFrame();
        }

        FinishAnimation();
    }

    void FinishAnimation()
    {
        switch (fadeInType)
        {
            case 0:
                transform.localScale = originalScale;
                break;
            case 1:
            case 2:
                transform.localPosition = new Vector2(0f, 0f);
                break;
        }
    }

    public virtual void StopAnimation()
    {
        if (b != null)
        {
            StopCoroutine(b);
            b = null;
        }
        FinishAnimation();
    }

    public virtual void OnShow()
    {
        if (b != null)
        {
            StopCoroutine(b);
        }

        b = StartCoroutine(OpenUICoroutine());

        Database.SetDescriptionMessage("");
    }

    public virtual void OnResume()
    {
        Database.SetDescriptionMessage("");
        OnShow();
    }

    public virtual void OnRemoveUI()
    {
        //AudioManager.instance.PlaySFX(AudioManager.instance.uI_DBL_Click);
        Database.SetDescriptionMessage("");
        UIManager.Instance.RemoveUI(this);
        gameObject.SetActive(false);
        UIManager.Instance.ShowLastUI();
        //Destroy(this.gameObject);
    }

    public virtual void OnEnterNewUI()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //public delegate void SimpleCallback();
    //public void SetUpInformationSprite(string _showText, float posX = -41f, float posY = 25.4f, float posX2 = 10, float posY2 = -10)
    //{
    //    GameObject hintObj = Instantiate(CraftView.Instance.informationObj);
    //    hintObj.SetActive(true);
    //    hintObj.transform.SetParent(this.transform);
    //    hintObj.transform.localScale = Vector2.one;
    //    hintObj.transform.localPosition = new Vector2(posX, posY);
    //    hintObj.GetComponent<HintingBox>().SetUpHint(_showText);
    //    hintObj.GetComponent<HintingBox>().pos = new Vector2(posX2, posY2);
    //}
}
