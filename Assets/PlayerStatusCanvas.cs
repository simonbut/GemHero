using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatusCanvas : MonoBehaviour
{
    public GameObject hpGameObject;
    int hp;
    int hpTotal;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    void Init()
    {
        hp = Database.userDataJson.hp;
        hpTotal = Database.userDataJson.hpTotal;
    }

    // Update is called once per frame
    void Update()
    {
        if (hp < Database.userDataJson.hp)
        {
            hp++;
        }
        if (hp > Database.userDataJson.hp)
        {
            hp--;
        }
        if (hpTotal < Database.userDataJson.hpTotal)
        {
            hpTotal++;
        }
        if (hpTotal > Database.userDataJson.hpTotal)
        {
            hpTotal--;
        }
        hpGameObject.transform.Find("Bar").GetComponent<Image>().fillAmount = hp * 1f / hpTotal;
        hpGameObject.transform.Find("Text").GetComponent<Text>().text = hp + " / " + hpTotal;
    }
}
