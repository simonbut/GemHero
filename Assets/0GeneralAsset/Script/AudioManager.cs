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
        BGM.volume = 0.05f * bgmVolume * bgmVolumeFadeinTimer;
        SFX.volume = 0.08f * sfxVolume;
    }

    public int bgmVolume;
    public float bgmVolumeFadeinTimer;
    public int sfxVolume;

    public static AudioManager instance;
    [SerializeField] AudioSource BGM;
    [SerializeField] AudioSource SFX;

    [SerializeField] AudioClip exampleBGM;
    [SerializeField] AudioClip exampleSFX;

    public void Init()
    {

    }

    #region audio

    GameObject PlaySFX(AudioClip _ac, float _speed = 1, float _decibelMultiplier = 1f, bool _isOverSenseChange = false)
    {
        GameObject sfx = Instantiate(SFX.gameObject);
        sfx.gameObject.SetActive(true);
        sfx.GetComponent<AudioSource>().clip = _ac;
        sfx.GetComponent<AudioSource>().pitch = _speed;
        sfx.GetComponent<AudioSource>().volume = 0.08f * sfxVolume * _decibelMultiplier * 0.5f;
        sfx.GetComponent<AudioSource>().Play();

        if (Camera.main != null)
        {
            sfx.transform.position = Camera.main.transform.position;
        }

        if (_isOverSenseChange)
        {
            sfx.AddComponent<DontDelete>();
        }

        return sfx;
    }

    #endregion

    #region bgm

    public void ContinueBgm()
    {
        BGM.Play();
    }

    public void StopBgm()
    {
        BGM.Stop();
    }

    public void PlayBGM(AudioClip _bgm)
    {
        BGM.clip = _bgm;
        BGM.Play();
        bgmVolumeFadeinTimer = 0;
    }

    #endregion
}
