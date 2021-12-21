using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatusCanvas : MonoBehaviour
{
    public GameObject hpGameObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        hpGameObject.transform.Find("Bar").GetComponent<Image>().fillAmount = Database.userDataJson.hp * 1f / Database.userDataJson.hpTotal;
        hpGameObject.transform.Find("Text").GetComponent<Text>().text = Database.userDataJson.hp + " / " + Database.userDataJson.hpTotal;
    }
}
