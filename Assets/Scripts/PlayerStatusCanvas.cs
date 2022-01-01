using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ClassHelper;

public class PlayerStatusCanvas : MonoBehaviour
{
    public GameObject hpGameObject;
    int hp;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    void Init()
    {
        hp = Database.userDataJson.hp;
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
        hpGameObject.transform.Find("Bar").GetComponent<Image>().fillAmount = hp * 1f / Player.GetTotalHp();
        hpGameObject.transform.Find("Text").GetComponent<Text>().text = hp + " / " + Player.GetTotalHp();
    }
}
