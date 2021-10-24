using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Net;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Globalization;
using ClassHelper;

public class InitScene : MonoBehaviour
{
    public bool isTestMode;
    public bool isDemo;
    public bool unlockAllOice;
    //public bool isNoSkillMode;
    public int versionNo;

    public Image firepillar2Icon;

    // Start is called before the first frame update
    void Start()
    {
        //EventSystem.current.currentInputModule.DeactivateModule();
        Application.targetFrameRate = 30;

        //Cursor.lockState = CursorLockMode.Confined;

        Database.isDemo = isDemo;
        //Database.isNoSkillMode = isNoSkillMode;
        Database.versionNo = versionNo;
        Database.isTestMode = isTestMode;

        Database.Init();
        //oiceManager.Instance.Init();

        //if (unlockAllOice)
        //{
        //    StartCoroutine(DownloadAllOice());
        //}
        //else
        //{
            //Database.PlayOice("92cd4ba8da894f9f945f902f73f0c95a", true);
            //print(Database.CheckExpOnThisLevel(0));
            //print(Database.ExpectedLevel(0));

            //Application.targetFrameRate = 30;

            //test save
            //Database.ContinueGame();
            //Database.InitSave(9,1,1,new List<int> {1,2,3});
            //Database.Save();
            //SceneManager.LoadScene("tunnelTest2");
            //SceneManager.LoadScene("craftTest1");
            //SceneManager.LoadScene("OrderScene");
            //


            //SceneManager.LoadScene("StartScene");
        //}

        //try
        //{
        //    ElasticsearchManager.ip = Dns.GetHostEntry(Dns.GetHostName()).AddressList[0].ToString();
        //    print(ElasticsearchManager.ip);
        //}
        //catch
        //{

        //}
    }

    //IEnumerator DownloadAllOice()
    //{
    //    foreach (Memory _m in MemoryManager.Instance.GetAllMemory())
    //    {
    //        yield return new WaitForSecondsRealtime(5f);
    //        MemoryManager.Instance.UnlockOice(_m.oice_uuid, true);
    //        MemoryManager.Instance.DownloadOice(_m.oice_uuid, true);
    //    }
    //}

    float timer = 0f;
    float timerTotal = 3f;
    int index = 0;
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        //if (index > 0 && index <= PreloadAssetManager.Instance.preloadGameObject.Count)
        //{

        //}
        if (index < PreloadAssetManager.Instance.preloadGameObject.Count)
        {
            if (PreloadAssetManager.Instance.preloadGameObject[index] != null)
            {
                //print(PreloadAssetManager.Instance.preloadGameObject[index].name);
                GameObject preloadInstance = Instantiate<GameObject>(PreloadAssetManager.Instance.preloadGameObject[index]);
                preloadInstance.SetActive(false);
                Destroy(preloadInstance);
            }
            index++;
        }
        if (index < PreloadAssetManager.Instance.preloadGameObject.Count)
        {
            if (PreloadAssetManager.Instance.preloadGameObject[index] != null)
            {
                //print(PreloadAssetManager.Instance.preloadGameObject[index].name);
                GameObject preloadInstance = Instantiate<GameObject>(PreloadAssetManager.Instance.preloadGameObject[index]);
                preloadInstance.SetActive(false);
                Destroy(preloadInstance);
            }
            index++;
        }
        if (index < PreloadAssetManager.Instance.preloadGameObject.Count)
        {
            if (PreloadAssetManager.Instance.preloadGameObject[index] != null)
            {
                //print(PreloadAssetManager.Instance.preloadGameObject[index].name);
                GameObject preloadInstance = Instantiate<GameObject>(PreloadAssetManager.Instance.preloadGameObject[index]);
                preloadInstance.SetActive(false);
                Destroy(preloadInstance);
            }
            index++;
        }

        if (timer > timerTotal)
        {
            if (!unlockAllOice)
            {
                //MainMenu,StartScene
                SceneManager.LoadScene("StartScene");
            }
        }
        else
        {
            firepillar2Icon.gameObject.transform.localScale = Vector3.one + (Vector3.one / 2f) * timer / timerTotal;
            firepillar2Icon.color = new Color(1, 1, 1, 2f * (1 - timer / (timerTotal - 0.5f)));
        }

    }
}
