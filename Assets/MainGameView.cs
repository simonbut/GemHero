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

    public List<ResourcePoint> resourcePointList = new List<ResourcePoint>();

    // Start is called before the first frame update
    void Start()
    {
        //test
        //CompositeView.Instance.StartComposite();
    }

    // Update is called once per frame
    void Update()
    {
        InteractiveDialog.transform.position = MathManager.WorldPosToCanvasPos(CharacterControlView.Instance.player.transform.position + Vector3.up * 1f);
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
}
