using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClassHelper;

public class ResourcePoint : MonoBehaviour
{
    public ReactType reactType;
    public string reactText;
    public int resourcePointId = 1;

    // Start is called before the first frame update
    void Start()
    {
        RegisterResourcePoint();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RegisterResourcePoint()
    {
        MainGameView.Instance.resourcePointList.Add(this);
        Material mat = Instantiate(GetComponent<SpriteRenderer>().material);
        gameObject.GetComponent<SpriteRenderer>().material = mat;
    }

    public void React()
    {
        switch (MainGameView.Instance.reactingObject.reactType)
        {
            case ReactType.Collect:

                break;
        }
    }

    public void CollectEnd()
    {

        gameObject.SetActive(false);
    }
}
