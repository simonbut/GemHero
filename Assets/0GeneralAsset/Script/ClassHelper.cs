using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace ClassHelper
{
    //General
    public enum Language
    {
        en,
        zh,
        cn,
        jp,
        de,
    }

    public class GlobalString
    {
        public string zh = "";
        public string en = "";
        public string cn = "";
        public string jp = "";

        public GlobalString(string _zh, string _en, string _cn, string _jp)
        {
            zh = _zh;
            en = _en;
            cn = _cn;
            jp = _jp;
        }

        public string GetString(Language language = new Language())
        {
            if (language == new Language())
            {
                language = Database.globalData.language;
            }

            string result = "";
            switch (language)
            {
                case Language.cn:
                    result = cn;
                    break;
                case Language.jp:
                    result = jp;
                    break;
                //case Language.de:
                //    return de;
                //    break;
                case Language.zh:
                    result = zh;
                    break;
                case Language.en:
                default:
                    result = en;
                    break;
            }
            //if (JoyStickManager.Instance.IsJoyStickEnable())
            //{
            //    result = result.Replace("「W」, 「A」, 「S」, and 「D」", "「D-Pad」");
            //    result = result.Replace("「W」、「A」、「S」、「D」", "「D-Pad」");
            //    result = result.Replace("「Q」 and 「E」", "「L1」 and 「R1」");
            //    result = result.Replace("「Q」、「E」", "「L1」、「R1」");
            //}
            if (result.Contains("「") && result.Contains("」"))
            {
                result = result.Replace("「", "<b> ");
                result = result.Replace("」", " </b>");
            }
            result = result.Replace("，", "， ");
            result = result.Replace("、", "、 ");
            result = result.Replace("。", "。 ");
            return result;
        }
    }


    public class Achievement
    {

        //get from database
        public int achievement_id;
        public GlobalString name;
        public GlobalString description;
        public int rewardOrder;
        public bool isValid;

        public bool IsAchievementGet()
        {
            if (achievement_id == 0)
            {
                return true;
            }
            return Database.globalData.completed_achievement_id.Contains((int)achievement_id);
        }
    }

    public class Memory
    {
        public int memory_id;
        public string oice_uuid;
        public int stage;
        public bool isBefore;

        public bool isGet = false;
    }

    public enum KeyBoardInput
    {
        Input_1 = 1,
        Input_2,
        Input_3,
        Input_4,
        Input_5,
        Input_6,
        Input_7,
        Input_8,
        Input_9,
        Input_0,
        Input_Enter,
        Input_Left,
        Input_Right,
        Input_Up,
        Input_Down,
        //1-15

        Input_sW,
        Input_Z,
        Input_A,
        Input_E,
        Input_R,
        Input_T,
        Input_Y,
        Input_U,
        Input_I,
        Input_O,
        Input_P,
        Input_Q,
        Input_S,
        Input_D,
        Input_F,
        Input_G,
        Input_H,
        Input_J,
        Input_K,
        Input_L,
        Input_M,
        Input_W,
        Input_X,
        Input_C,
        Input_Space,

        Input_Left_Click,
        Input_Right_Click,
        Input_Red,

        Input_sP,
        Input_V,
        Input_B,
        Input_N,

        Input_Green,
        Input_Blue,
        Input_Gear,
        Input_Money,
        Input_EnergyTank,

        Input_HOPE,
        Input_Shop,
        Input_Tech,
        Input_Action,
        Input_Star,

        F1,
        F2,
        F3,
        F4,
        F5,
        F6,
        F7,
        F8,
        F9,

        Up,
        Down,

        joystick_direction,
        joystick_l1,
        joystick_l2,
        joystick_r1,
        joystick_r2,
        joystick_circle,
        joystick_square,
        joystick_cross,
        joystick_triangle,
        joystick_R3Up,
        joystick_R3Down,

    }
    //


    public delegate void ListItemCallback(int _id, ListItem gi);
    public delegate void GridItemCallback(int _id, GridItem gi);
}