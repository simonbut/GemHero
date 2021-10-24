using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClassHelper;

public class GlobalCommunicateManager : MonoBehaviour
{

    public static string oiceId = "";

    public static int selectingId = 0;
    public static int selectingScrollViewId = 0;

    public static bool backToMenuFromGame = false;

    public static bool insideGame = false;

    public static float time;//Time on the current frame
    public static float preTime;//Time on the previous frame

    public static bool isTutorial = false;

    public static bool IsStepOneSecond()
    {
        return (Mathf.FloorToInt(time) > Mathf.FloorToInt(preTime));
    }
    public static bool IsStepHalfSecond()
    {
        return (Mathf.FloorToInt(time * 2f) > Mathf.FloorToInt(preTime * 2f));
    }
    public static bool IsStepFiveSecond()
    {
        return (Mathf.FloorToInt(time / 5f) > Mathf.FloorToInt(preTime / 5f));
    }

    public static float costMultiplier = 1f;

    private void Update()
    {
        preTime = time;
        time = Time.time;

        //print("selectingScrollViewId " + GlobalCommunicateManager.selectingScrollViewId);
    }

}
