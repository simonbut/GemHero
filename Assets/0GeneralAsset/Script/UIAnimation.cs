using UnityEngine;
using UnityEngine.UI;

public class UIAnimation : MonoBehaviour
{
    public bool playOnEnable = false;
    public float animationDelay = 0f;
    public float speedMultiplier = 1f;
    public bool isRepeat = false;
    public bool dependOnTimeScale = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        if (playOnEnable)
        {
            StartAnimation();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlaying)
        {
            if (dependOnTimeScale)
            {
                timer += Time.deltaTime * speedMultiplier;
            }
            else
            {
                timer += Time.unscaledDeltaTime * speedMultiplier;
            }
            image.sprite = sprites[Mathf.Clamp(Mathf.FloorToInt(timer * 24f), 0, sprites.Length - 1)];
            transform.localScale = scaleWhenStart * (1 * (1 - timer / totalTime) + endScale * timer / totalTime);
            if (timer > totalTime)
            {
                if (isRepeat)
                {
                    PlayAnimation();
                }
                else
                {
                    image.gameObject.SetActive(false);
                    isPlaying = false;
                }
            }
        }
    }

    float timer = 0;
    bool isPlaying = false;
    //public Texture2D texture;
    public Image image;

    float totalTime = 0;
    public Sprite[] sprites;

    public void StartAnimation()
    {
        if (animationDelay > 0f)
        {
            Invoke("PlayAnimation", animationDelay);
        }
        else
        {
            PlayAnimation();
        }
    }

    void PlayAnimation()
    {
        image.gameObject.SetActive(true);
        //sprites = Resources.LoadAll<Sprite>("UI/Animation/" + texture.name);
        totalTime = sprites.Length / 24f;
        isPlaying = true;
        timer = 0;
        if (scaleWhenStart.x < 0f)
        {
            scaleWhenStart = transform.localScale;
        }
    }

    Vector3 scaleWhenStart = Vector3.one * -1f;
    public float endScale = 1f;

}
