using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestView : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //test
        //ResourcePointManager.Instance.DrawAsset(1);
        //ResourcePointManager.Instance.DrawAsset(1);
        MainGameView.Instance.compositeMenuCanvas.AddUI(1);
        //UIManager.Instance.compositionDataUI.Show(1, true);
        //MainGameView.Instance.inGameMainMenuUI.AddUI();
        //CompositeView.Instance.StartComposite();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
