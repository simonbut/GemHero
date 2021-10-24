using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamAchievement : MonoBehaviour
{


    public static void UnlockAcheivement(int id)
    {
        if (SteamManager.Initialized)
        {
            //Demo don't have Acheivement
            Steamworks.SteamUserStats.SetAchievement("NEW_ACHIEVEMENT_1_" + id);
            Steamworks.SteamUserStats.StoreStats();
        }
    }
}
