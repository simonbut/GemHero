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
        //MainGameView.Instance.compositeMenuCanvas.AddUI(1);
        //UIManager.Instance.compositionDataUI.Show(1, true);
        //MainGameView.Instance.inGameMainMenuUI.AddUI();
        MainGameView.Instance.tagBaseCanvas.Show(AssetManager.Instance.GetRecipeData(1).shape, new List<int>() { 4, 3 }, new List<Vector2Int>() { new Vector2Int(-1, -1), new Vector2Int(1, -1) });
        //CompositeView.Instance.StartComposite();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
