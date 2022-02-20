using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClassHelper;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class EquipmentCanvas : ControlableUI
{
    public GridScrollView gridScrollView;

    public override void OnRemoveUI()
    {
        UIManager.Instance.HideAllDataUI();

        base.OnRemoveUI();
    }


    public override void OnShow()
    {
        base.OnShow();
    }

    public void Refresh()
    {
        UIManager.Instance.assetDataUI.Hide();
        UIManager.Instance.compositionDataUI.Hide();

        gridScrollView.Setup("Asset List", this, ClickData, SelectingData, DisSelectingData);

        GenerateList();
    }

    void GenerateList()
    {
        foreach (Asset _a in AssetManager.Instance.GetAssetList(0, 0, 0, equipmentTypes[session]))
        {
            GameObject gridItemInstance = gridScrollView.GenerateItem("Asset/" + _a.assetId.ToString("000"), _a.assetUid);
        }
    }

    void ClickData(int uid, GridItem gi)
    {
        for (int i = 0; i < Database.userDataJson.equipment.Count; i++)
        {
            if (Database.userDataJson.equipment[i] == uid)
            {
                if (session == i)
                {
                    print("disselect");
                    Database.userDataJson.equipment[i] = 0;
                    sessions[i].transform.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("AssetType/" + AssetManager.Instance.GetAssetTypeData(equipmentTypes[i]).id.ToString("000"));
                    return;
                }
                else
                {
                    print("this is already used");
                    return;
                }
            }
        }

        Database.userDataJson.equipment[session] = uid;
        sessions[session].transform.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("Asset/" + AssetManager.Instance.GetAssetByUid(Database.userDataJson.equipment[session]).assetId.ToString("000"));

        session = -1;
    }

    void SelectingData(int uid, GridItem gi)
    {
        Asset _a = AssetManager.Instance.GetAssetByUid(uid);
        UIManager.Instance.assetDataUI.Show(_a, 1200);
    }

    void DisSelectingData(int uid, GridItem gi)
    {

    }

    // Update is called once per frame
    void Update()
    {
        gridScrollView.gameObject.SetActive(session > -1);

        for (int i = 0; i < Database.userDataJson.equipment.Count; i++)
        {
            sessions[i].transform.Find("BlackOverlay").gameObject.SetActive(session != i);
            sessions[i].transform.Find("Tick").gameObject.SetActive(false);
        }

        if (!UIManager.Instance.IsCurrentUI(this))
        {
            return;
        }

        if (ControlView.Instance.controls.Map1.Cancel.triggered)
        {
            UIManager.Instance.assetDataUI.Hide();
            UIManager.Instance.compositionDataUI.Hide();
            OnBackPressed();
        }
    }

    public List<GameObject> sessions;
    public int session = 0;
    public List<int> equipmentTypes = new List<int> { 10002, 10006, 10004, 10004, 10004 };

    public override void AddUI()
    {
        DefineSessions();
        session = -1;

        base.AddUI();
    }

    void DefineSessions()
    {
        for (int i = 0; i < sessions.Count; i++)
        {
            if (i < equipmentTypes.Count)
            {
                sessions[i].SetActive(true);
                AssetTypeData _atd = AssetManager.Instance.GetAssetTypeData(equipmentTypes[i]);
                sessions[i].transform.Find("Text").GetComponent<Text>().text = "";//_atd.name.GetString()
                if (Database.userDataJson.equipment[i] > 0)
                {
                    sessions[i].transform.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("Asset/" + AssetManager.Instance.GetAssetByUid(Database.userDataJson.equipment[i]).assetId.ToString("000"));
                }
                else
                {
                    sessions[i].transform.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>(AssetManager.Instance.GetRecipeAssetIconPath(_atd.id));
                }
            }
            else
            {
                sessions[i].SetActive(false);
            }
        }
    }

    public void SelectSession(int _session)
    {
        print("select session " + _session);
        session = _session;
        Refresh();
    }
}
