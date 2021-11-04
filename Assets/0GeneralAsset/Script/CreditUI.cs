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

        //if (JoyStickManager.Instance.IsInputDown("Circle") || JoyStickManager.Instance.IsInputDown("Cross") || JoyStickManager.Instance.IsInputDown("Escape"))
        //{
        //    if (UIManager.Instance.IsCurrentUI(this))
        //    {
        //        OnBackPressed();
        //        SceneManager.LoadScene("MainMenu");
        //    }
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
    //        lyricsText.text = "";
    //    }

    //    lyricsText.text = Database.GetLocalizedText(lyricsText.text);
    //}
}
