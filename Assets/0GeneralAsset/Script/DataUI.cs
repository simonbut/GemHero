using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class DataUI : MonoBehaviour
{
    public bool hiding = false;
    Vector2 originalScale = Vector2.one;
    Coroutine b;
    //public bool isAnime = true;
    // Start is called before the first frame update
    public void OnShow()
    {
        if (!gameObject.activeInHierarchy || hiding)
        {
            gameObject.SetActive(true);
            if (b != null)
            {
                StopCoroutine(b);
            }
            if (!hiding)
            {
                //print("UI opening");
                b = UIManager.Instance.StartCoroutine(OpenUICoroutine());
            }
            else
            {
                transform.localScale = originalScale;
            }
            //if (isAnime)
            //{
            //    b = UIManager.Instance.StartCoroutine(OpenUICoroutine());
            //}
            //else
            //{
            //    transform.localScale = originalScale;
            //}
        }


        //StartCoroutine(OpenUICoroutine());
    }

    public void StopHide()
    {
        if (b != null)
        {
            StopCoroutine(b);
        }

        hiding = false;
        transform.localScale = originalScale;
    }

    // Update is called once per frame
    public void OnHide()
    {
        if (!gameObject.activeInHierarchy)
        {
            return;
        }


        if (b != null)
        {
            StopCoroutine(b);
        }

        b = StartCoroutine(HideUICoroutine());

        //StartCoroutine(HideUICoroutine());
    }

    IEnumerator OpenUICoroutine()
    {
        //yield return new WaitForEndOfFrame();

        //StopCoroutine(OpenUICoroutine());
        //StopCoroutine(HideUICoroutine());

        hiding = false;

        float timer = 0;
        float timerTotal = 0.2f;

        while (timer < timerTotal)
        {
            transform.localScale = new Vector2(originalScale.x, originalScale.y * MathManager.BoundingFloat(timer, 0.7f, 1f, timerTotal, 0.1f));
            timer += Time.unscaledDeltaTime;

            yield return new WaitForEndOfFrame();
        }

        transform.localScale = originalScale;
    }

    IEnumerator HideUICoroutine()
    {
        yield return new WaitForEndOfFrame();

        //StopCoroutine(OpenUICoroutine());
        //StopCoroutine(HideUICoroutine());

        //hiding = true;

        //float timer = 0;
        //float timerTotal = 0.2f;

        //while (timer < timerTotal)
        //{
        //    transform.localScale = new Vector2(originalScale.x, originalScale.y * MathManager.BoundingFloat(timerTotal - timer, 0f, 1f, timerTotal, 0.1f));
        //    timer += Time.unscaledDeltaTime;

        //    yield return new WaitForEndOfFrame();
        //}

        //transform.localScale = originalScale;

        gameObject.SetActive(false);

        hiding = false;
    }

}
