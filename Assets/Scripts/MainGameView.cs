using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ClassHelper;

public class MainGameView : MonoBehaviour
{
    #region instance
    private static MainGameView m_instance;

    public static MainGameView Instance
    {
        get
        {
            return m_instance;
        }
    }

    void Awake()
    {
        if (MainGameView.Instance == null)
        {
            m_instance = this;
        }
    }
    #endregion

    public ResourcePoint reactingObject;
    public List<ResourcePoint> resourcePointList = new List<ResourcePoint>();

    public InGameMainMenuUI inGameMainMenuUI;
    public RecipeMenuCanvas recipeMenuCanvas;

    // Start is called before the first frame update
    void Start()
    {
        //inGameMainMenuUI.AddUI();

        //test
        CompositeView.Instance.StartComposite();
    }

    // Update is called once per frame
    void Update()
    {
        InteractiveDialog.transform.position = MathManager.WorldPosToCanvasPos(CharacterControlView.Instance.player.transform.position + Vector3.up * 1f);

        foreach (ResourcePoint _rp in resourcePointList)
        {
            if (_rp == null)
            {
                continue;
            }
            if (reactingObject == _rp)
            {
                _rp.GetComponent<SpriteRenderer>().material.SetFloat("IsReactable", 1);
                ShowInteractiveDialog(_rp.reactText);
            }
            else
            {
                _rp.GetComponent<SpriteRenderer>().material.SetFloat("IsReactable", 0);
            }
        }
        if (reactingObject == null)
        {
            HideInteractiveDialog();
        }
    }

    public GameObject InteractiveDialog;
    public void ShowInteractiveDialog(string _text)
    {
        InteractiveDialog.SetActive(true);
        InteractiveDialog.transform.Find("Text").GetComponent<Text>().text = _text;
    }

    public void HideInteractiveDialog()
    {
        InteractiveDialog.SetActive(false);
    }

    public void CharacterReact()
    {
        switch (reactingObject.reactType)
        {
            case ReactType.Collect:
                UIManager.Instance.assetDataUI.Show(ResourcePointManager.Instance.DrawAsset(reactingObject.resourcePointId));
                break;
            case ReactType.Talk:

                break;
        }
    }

    public void OpenRecipeMenu()
    {
        recipeMenuCanvas.AddUI();
    }
}
