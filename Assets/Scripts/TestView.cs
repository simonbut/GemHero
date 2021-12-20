using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClassHelper;

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
        //MainGameView.Instance.tagBaseCanvas.Show(AssetManager.Instance.GetRecipeData(1).shape, new List<int>() { 4, 3 }, new List<Vector2Int>() { new Vector2Int(-1, -1), new Vector2Int(1, -1) });
        //CompositeView.Instance.StartComposite();
        //UIManager.Instance.choiceUI.Setup(new Vector2(Screen.width / 2f, Screen.height / 2f), new List<string> { "12312312312", "asd" }, new List<Callback> { null, null });
        //MainGameView.Instance.dialogCanvas.Setup(new Vector2(Screen.width / 2f, Screen.height / 2f), "ABC", "asdasd asdasd asdasd asd asd asd asd asd asdasd asdasd asdasd asd asd asd asd asd");
        Database.userDataJson.time += 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
