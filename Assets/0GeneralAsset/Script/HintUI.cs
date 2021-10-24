using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HintUI : MonoBehaviour
{
    [SerializeField] Image bg;
    [SerializeField] TextMeshProUGUI text;
    bool isShow = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isShow)
        {
            text.color = (text.color * 5f + new Color(1f, 1f, 1f, 1)) / (5f + 1f);
            bg.color = (text.color * 5f + new Color(1, 1, 1, 1)) / (5f + 1f);
        }
        else
        {
            text.color = (text.color + new Color(1f, 1f, 1f, 0) * 5f) / (5f + 1f);
            bg.color = (text.color + new Color(1, 1, 1, 0) * 5f) / (5f + 1f);
        }
    }

    public void Show(string message)
    {
        text.SetText(message);
        isShow = true;
    }

    public void Hide()
    {
        isShow = false;
    }
}
