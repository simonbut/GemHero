using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEditor;
using System.Linq;
using System.Text;
using ClassHelper;
using System.Net;
using Newtonsoft.Json;

public class ElasticsearchManager : MonoBehaviour
{

    public static void ESLog(string eventName, Dictionary<string, object> param)
    {
        if (param == null)
        {
            param = new Dictionary<string, object>();
        }
        param.Add("EventType", eventName);
        //Debug.Log(System.DateTime.UtcNow.ToString("yyyy-MM-dd'T'HH:mm:ssZ"));
        param = AppendBasicInfo(param);
        string json = MiniJSON.Json.Serialize(param);
        //string json = JsonUtility.ToJson(param);
        //Debug.Log(json);

        StaticCoroutine.DoCoroutine(ESLogRequest(json));

    }

    static string Authenticate(string username, string password)
    {
        string auth = username + ":" + password;
        auth = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(auth));
        auth = "Basic " + auth;
        return auth;
    }

    static IEnumerator ESLogRequest(string json)
    {
        string authorization = Authenticate("logging", "ninoisekai");
        string _id = ((uint)((System.DateTime.UtcNow - new System.DateTime(0)).TotalSeconds)).ToString() + ((uint)((System.DateTime.UtcNow - new System.DateTime(0)).TotalMilliseconds % 1000)).ToString();
        //string url = "https://ee9a7fa85d9745e68ab702775e290f4b.us-east-1.aws.found.io:9243/dtank/_doc/" + _id + ((int)Random.Range(0, 10000)).ToString() + "?pipeline=geoip";
        string url = "https://ee9a7fa85d9745e68ab702775e290f4b.us-east-1.aws.found.io:9243/dtank/_doc/" + _id + ((int)Random.Range(0, 10000)).ToString();

        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(json);

        //print(json);
        //print(url);

        UnityWebRequest www = UnityWebRequest.Put(url, bytes);
        www.SetRequestHeader("Content-Type", "application/json");
        www.SetRequestHeader("AUTHORIZATION", authorization);

        yield return www.SendWebRequest();

        if (www.isNetworkError)
        {
            Debug.Log("Error while Receiving: " + www.error);
        }
        else
        {
            //Debug.Log(www.downloadHandler.text);
        }
    }

    static public Dictionary<string, object> AppendInGameInfo(Dictionary<string, object> _param)
    {
        Dictionary<string, object> result = new Dictionary<string, object>(_param);

        result.Add("SaveId", Database.saveId);
        result.Add("Tutorial", GlobalCommunicateManager.isTutorial);

        //List<string> totemList = new List<string>();
        //foreach (Totem _t in TotemManager.Instance.GetTotemList())
        //{
        //    totemList.Add(AbilityManager.Instance.GetAbility(_t.ability).name.GetString(Language.zh));
        //}
        //result.Add("TotemList", totemList);



        return result;
    }

    //static public string ip;
    static protected Dictionary<string, object> AppendBasicInfo(Dictionary<string, object> _param)
    {
        Dictionary<string, object> result = new Dictionary<string, object>(_param);
        //if (Application.platform == RuntimePlatform.Android)
        //{
        //    result.Add("Platform", "Android");
        //    result.Add("BuildNumber", PlayerSettings.Android.bundleVersionCode);
        //}
        //else
        //{
        //    result.Add("Platform", "iOS");
        //    result.Add("BuildNumber", PlayerSettings.iOS.buildNumber);
        //}
        result.Add("@timestamp", System.DateTime.UtcNow.ToString("yyyy-MM-dd'T'HH:mm:ssZ"));
        //result.Add("CreateTime", PlayerManager.Instance.Profile.CreateTime.ToString("yyyy-MM-dd'T'HH:mm:ssZ"));
        result.Add("version", Database.versionNo);
        try
        {
            //string hostName = Dns.GetHostName(); // Retrive the Name of HOST  
            string myIP = Dns.GetHostAddresses(Dns.GetHostName())[0].ToString();
            result.Add("DeviceIdentifier", SystemInfo.deviceUniqueIdentifier);
            result.Add("IP", myIP);
            //result.Add("IP", multi.player.ipAddress);
            //result.Add("IP", Dns.GetHostEntry(Dns.GetHostName()).AddressList[0].ToString());
            //result.Add("IP", ip);
        }
        catch
        {

        }
        //result.Add("DayFromCreate", Mathf.FloorToInt((float)(System.DateTime.UtcNow - PlayerManager.Instance.Profile.CreateTime).TotalDays));
        //Debug.Log(result);
        return result;
    }


}
