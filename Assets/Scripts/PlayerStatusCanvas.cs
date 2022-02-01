using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ClassHelper;

public class PlayerStatusCanvas : MonoBehaviour
{
    public GameObject hpGameObject;
    public GameObject virtueGemParent;
    public GameObject virtueGemPrefab;
    int hp;

    // Start is called before the first frame update
    void Start()
    {
        Refresh();
    }

    public void Refresh()
    {
        hp = Player.GetHp();
        foreach (Transform _t in virtueGemParent.transform)
        {
            Destroy(_t.gameObject);
        }
        for (int i = 0; i < Player.GetVirtueGemList().Count; i++)
        {
            GameObject _vgInstance = Instantiate(virtueGemPrefab);
            _vgInstance.transform.SetParent(virtueGemParent.transform);
            _vgInstance.transform.localPosition = new Vector2(65 * i, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (hp < Player.GetHp())
        {
            hp++;
        }
        if (hp > Player.GetHp())
        {
            hp--;
        }
        hpGameObject.transform.Find("Bar").GetComponent<Image>().fillAmount = hp * 1f / Player.GetTotalHp();
        hpGameObject.transform.Find("Text").GetComponent<Text>().text = hp + " / " + Player.GetTotalHp();
    }
}
