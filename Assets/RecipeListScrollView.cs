using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ClassHelper;

public class RecipeListScrollView : ListScrollView
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
    }


    public GameObject GenerateItem(string _name, int _id, int _hpLoss)
    {
        GameObject listItemInstance = base.GenerateItem(_name, _id);

        listItemInstance.transform.Find("HpLossText").GetComponent<Text>().text = _hpLoss.ToString("0");

        return listItemInstance;
    }
}
