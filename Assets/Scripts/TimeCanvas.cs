using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ClassHelper;

public class TimeCanvas : MonoBehaviour
{
    public GameObject hourGameObject;
    public GameObject minuteGameObject;
    public Text dayText;

    float time;
    // Start is called before the first frame update
    void Start()
    {
        time = Database.userDataJson.time; 
    }

    // Update is called once per frame
    void Update()
    {
        if (time < Database.userDataJson.time)
        {
            time += Time.deltaTime * 20f;
        }
        if (time > Database.userDataJson.time)
        {
            time = Database.userDataJson.time;
        }
        DisplayClock(Mathf.FloorToInt(time));
    }

    void DisplayClock(int _time)
    {
        int day = Mathf.FloorToInt(_time * 1f/ 1440f) + 1;
        int hour = (_time % 1440);
        int minute = (_time % 60);

        minuteGameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -minute * 1f / 60f * 360f));
        hourGameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -(hour % 720) * 1f / 720f * 360f));

        dayText.text = "Day " + day.ToString("0") + "\n";
        if (hour > 60 * 6 && hour < 1440 / 2f + 60 * 6)//morning
        {
            dayText.text += "Morning";
        }
        else//evening
        {
            dayText.text += "Evening";
        }
    }
}
