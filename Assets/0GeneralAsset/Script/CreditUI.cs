using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CreditUI : ControlableUI
{
    public Text mainText;
    public Text lyricsText;
    //public AudioClip themeSong;

   public void PlayBGMThemeSong()
    {
        //AudioManager.instance.PlayBGM(AudioManager.instance.theme_Song);
    }

    Font f;
    private void Awake()
    {
        f = mainText.font;
    }

    private void Update()
    {
        mainText.font = f;

        //if (Database.globalData.isCredit1Seen)
        //{
        if (JoyStickManager.Instance.IsInputDown("Circle") || JoyStickManager.Instance.IsInputDown("Cross") || JoyStickManager.Instance.IsInputDown("Escape"))
        {
            if (UIManager.Instance.IsCurrentUI(this))
            {
                OnBackPressed();
                SceneManager.LoadScene("MainMenu");
            }
        }
        //}

        lyricsText.text = "";
        //ShowLyrics();

        if (timer < 248)
        {
            creditBody.anchoredPosition = new Vector2(0, timer * 5.5f);
        }
        else
        {
            AudioManager.instance.StopBGM();
        }

        //mainText.font = SystemManager.;

        timer += Time.deltaTime;

        if (timer > 258)
        {
            OnBackPressed();
            SceneManager.LoadScene("MainMenu");

            Database.globalData.isCredit1Seen = true;
            Database.SaveGlobalSave();
        }
    }

    public RectTransform creditBody;
    float timer = 0f;
    public override void OnShow()
    {
        //prevVol = AudioManager.instance.bgmVolume;
        //AudioManager.instance.bgmVolume = 0;
        PlayBGMThemeSong();

        ControlMeaningUI.Instance?.Hide();
        timer = 0f;
        creditBody.anchoredPosition = new Vector2(0, 0);

        //AudioManager.instance.bgmVolume = 100;

        base.OnShow();
    }

    //int prevVol = 0;
    //public override void OnRemoveUI()
    //{
    //    AudioManager.instance.bgmVolume = prevVol;
    //    base.OnRemoveUI();
    //}

    //public void ShowLyrics()
    //{
    //    if (timer >= 3.5 && timer < 7.5)
    //    {
    //        lyricsText.text = "猜 不透未來";
    //    }
    //    if (timer >= 9 && timer < 13)
    //    {
    //        lyricsText.text = "此刻 狂風中感嘆息";
    //    }
    //    if (timer >= 14.5 && timer < 17.5)
    //    {
    //        lyricsText.text = "沒有將來的夢";
    //    }
    //    if (timer >= 17.5 && timer < 20.5)
    //    {
    //        lyricsText.text = "我踏上了征途";
    //    }
    //    if (timer >= 20.5 && timer < 25)
    //    {
    //        lyricsText.text = "尋覓那 回憶的美好";
    //    }

    //    if (timer >= 29 && timer < 33)
    //    {
    //        lyricsText.text = "當 風雨襲來";
    //    }
    //    if (timer >= 35 && timer < 40)
    //    {
    //        lyricsText.text = "再找  離不開的借口";
    //    }
    //    if (timer >= 41 && timer < 44)
    //    {
    //        lyricsText.text = "俗世 一模一樣";
    //    }
    //    if (timer >= 44 && timer < 47)
    //    {
    //        lyricsText.text = "每日 要怎生存";
    //    }
    //    if (timer >= 47 && timer < 51)
    //    {
    //        lyricsText.text = "還沒有 想像 天堂";
    //    }

    //    if (timer >= 53 && timer < 59)
    //    {
    //        lyricsText.text = "遺憾了 誰的期盼 夜空的星宿";
    //    }
    //    if (timer >= 59 && timer < 65)
    //    {
    //        lyricsText.text = "努力再 閃耀 卻又化做了泡影";
    //    }
    //    if (timer >= 65 && timer < 71)
    //    {
    //        lyricsText.text = "人在命運的旅途 或者想留下";
    //    }
    //    if (timer >= 71 && timer < 79)
    //    {
    //        lyricsText.text = "告別了 疲倦了 停頓了片刻 一聲嘆息";
    //    }

    //    if (timer >= 88 && timer < 93)
    //    {
    //        lyricsText.text = "若現實 沒結果 不信命數";
    //    }
    //    if (timer >= 94 && timer < 99)
    //    {
    //        lyricsText.text = "在落日 未了解 不想記起";
    //    }
    //    if (timer >= 101 && timer < 106)
    //    {
    //        lyricsText.text = "察覺了未來 孤單的歲月";
    //    }
    //    if (timer >= 106 && timer < 112)
    //    {
    //        lyricsText.text = "始終躲不過 命運作弄";
    //    }

    //    if (timer >= 112 && timer < 117)
    //    {
    //        lyricsText.text = "若現在 或與你 相距漸遠";
    //    }
    //    if (timer >= 117 && timer < 123)
    //    {
    //        lyricsText.text = "在舊日 沒半點 傷心記憶";
    //    }
    //    if (timer >= 124 && timer < 130)
    //    {
    //        lyricsText.text = "哪裡有意義 星空的變幻";
    //    }
    //    if (timer >= 130 && timer < 135.5)
    //    {
    //        lyricsText.text = "天真該醒了 學習接受";
    //    }
    //    if (timer >= 135.5 && timer < 140)
    //    {
    //        lyricsText.text = "一切 只是 過程";
    //    }

    //    if (timer >= 191 && timer < 196)
    //    {
    //        lyricsText.text = "若現實 沒結果 不信命數";
    //    }
    //    if (timer >= 197 && timer < 202)
    //    {
    //        lyricsText.text = "在落日 未了解 不想記起";
    //    }
    //    if (timer >= 203 && timer < 209)
    //    {
    //        lyricsText.text = "察覺了未來 孤單的歲月";
    //    }
    //    if (timer >= 209 && timer < 214)
    //    {
    //        lyricsText.text = "始終躲不過 命運作弄";
    //    }

    //    if (timer >= 215 && timer < 220)
    //    {
    //        lyricsText.text = "若現在 或與你 相距漸遠";
    //    }
    //    if (timer >= 220 && timer < 226)
    //    {
    //        lyricsText.text = "在舊日 沒半點 傷心記憶";
    //    }
    //    if (timer >= 227 && timer < 233)
    //    {
    //        lyricsText.text = "哪裡有意義 星空的變幻";
    //    }
    //    if (timer >= 233 && timer < 237)
    //    {
    //        lyricsText.text = "天真該醒了 學習接受";
    //    }
    //    if (timer >= 238 && timer < 243)
    //    {
    //        lyricsText.text = "一切 只是 過程";
    //    }

    //    if (timer >= 268 && timer < 272)
    //    {
    //        lyricsText.text = "聽 天際白雲";
    //    }
    //    if (timer >= 274 && timer < 279)
    //    {
    //        lyricsText.text = "說出 微風中的訊息";
    //    }
    //    if (timer >= 280 && timer < 284)
    //    {
    //        lyricsText.text = "盡了千帆的夢";
    //    }
    //    if (timer >= 284 && timer < 287.5)
    //    {
    //        lyricsText.text = "我踏上了歸途";
    //    }
    //    if (timer >= 287.5 && timer < 293)
    //    {
    //        lyricsText.text = "尋覓那 回憶的美好";
    //    }

    //    lyricsText.text = Database.GetLocalizedText(lyricsText.text);
    //}
}
