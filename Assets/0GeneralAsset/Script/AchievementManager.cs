using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClassHelper;

public class AchievementManager : MonoBehaviour
{
    #region instance
    private static AchievementManager m_instance;

    public static AchievementManager Instance
    {
        get
        {
            return m_instance;
        }
    }

    void Awake()
    {
        if (AchievementManager.Instance == null)
        {
            m_instance = this;
        }
    }
    #endregion

    [HideInInspector]
    public List<Achievement> achievement = new List<Achievement>();
    public List<Achievement> GetAchievementFullList()
    {
        return achievement;
    }

    public void LoadAchievementData()
    {
        achievement = new List<Achievement>();
        //TextAsset data = Resources.Load("database/Database - " + globalData.language.ToString() + " - achievement") as TextAsset;
        string data = Database.ReadDatabaseWithoutLanguage("Achievement");
        if (data.Length > 0)
        {
            string[] _a = data.Split('\n');
            for (int i = 1; i < _a.Length; i++)
            {
                Achievement _b = new Achievement();
                string[] _c = _a[i].Split('\t');
                int.TryParse(_c[0], out _b.id);
                _b.name = new LocalizedString(_c[1], _c[1], _c[1], "");
                _b.description = new LocalizedString(_c[2], _c[2], _c[2], "");
            }
        }
        else
        {
            Debug.Log("data is null");
        }
    }



    public Achievement GetAchievement(int achievement_id)
    {
        Achievement result = new Achievement();
        foreach (Achievement _a in achievement)
        {
            if (_a.id == achievement_id)
            {
                result = _a;
                return result;
            }
        }
        return result;
    }

    public bool IsAchievementGet(int achievement_id)
    {
        if (achievement_id == 0)
        {
            return true;
        }
        return Database.globalData.completedAchievementId.Contains((int)achievement_id);
    }

    public void CompleteAchievement(int achievement_id)
    {
        print("CompleteAchievement " + achievement_id);
        if (!Database.globalData.completedAchievementId.Contains((int)achievement_id))
        {
            SteamAchievement.UnlockAcheivement(achievement_id);
            Database.globalData.completedAchievementId.Add(achievement_id);
            //AchievementManager.Instance.GetAchievement(achievement_id).isGet = true;

            Database.SaveGlobalSave();

            //foreach (Card _c in CardManager.Instance.GetAllSuitableCard())
            //{
            //    if (_c.GetAchievement().achievement_id == achievement_id)
            //    {
            //        UIManager.Instance.AddNewCardStack(_c.id);
            //    }
            //}
        }
    }

    public void CheckNewCardGet()
    {
        //if (UIManager.Instance.newCardStack.Count > 0)
        //{
            UIManager.Instance.AddAchievementUI();
        //}
    }

    public void SyncSteamAchievements()
    {
        foreach (int _id in Database.globalData.completedAchievementId)
        {
            SteamAchievement.UnlockAcheivement(_id);
        }
    }
}
