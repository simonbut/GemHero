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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DisplayClock();
    }

    void DisplayClock()
    {
        int day = Mathf.FloorToInt(Database.userDataJson.time * 1f/ 1440f) + 1;
        int hour = (Database.userDataJson.time % 1440);
        int minute = (Database.userDataJson.time % 60);

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
