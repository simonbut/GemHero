using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        Init();
        instance = this;
    }

    public bool isMute;

    private void Update()
    {
        if (bgmVolumeFadeinTimer < 1)
        {
            bgmVolumeFadeinTimer += Time.deltaTime;
        }
        else
        {
            bgmVolumeFadeinTimer = 1;
        }
        //Camera.main.GetComponent<AkAudioListener>().
        BGM.volume = 0.05f * Database.globalData.bgm * bgmVolumeFadeinTimer;
        SFX.volume = 0.08f * Database.globalData.sfx;
        Voice.volume = 0.08f * Database.globalData.sfx;
        if (isMute)
        {
            BGM.volume = 0;
            SFX.volume = 0;
            Voice.volume = 0;
        }
    }

    //public int bgmVolume;
    public float bgmVolumeFadeinTimer;
    //public int sfxVolume;

    public static AudioManager instance;
    [SerializeField] AudioSource BGM;
    [SerializeField] AudioSource SFX;
    [SerializeField] AudioSource Voice;

    public void Init()
    {
        //PlaySFX(akEvent);
    }


    GameObject bgmObject = null;
    public void PlayBGM(AudioSource _bgm)
    {

    }

    public void StopBGM()
    {

    }

    public void PlaySFX(AudioSource _sfx,GameObject _g = null)
    {
        //return;
        if(_g == null)
        {
            GameObject _instance = new GameObject();
            //GameObject _instance = Instantiate(new GameObject());
            if (Camera.main != null)
            {
                _instance.transform.position = Camera.main.transform.position;
            }

            _instance.transform.SetParent(transform);
            _instance.name = _sfx.name;

            _g = _instance;
        }
    }

    GameObject dizzySFX;
    public void RegisterDizzySFX(GameObject _g)//
    {
        dizzySFX = _g;
    }

    public void DeleteDizzySFX()//
    {
        Destroy(dizzySFX);
    }
}
