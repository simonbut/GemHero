using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcePoint : MonoBehaviour
{
    public string reactText;

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
    }
}
