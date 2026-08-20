// 定数クラス
using UnityEngine;

namespace Const
{
    public static class SceneNames
    {
        public static string Title = "TitleScene";
        public static string Infomatione = "InfomationScene";
        public static string Tutorial = "TutorialScene";
        public static string Play = "PlayScene";
        public static string Result = "ResultScene";
        public static string Gameover = "GameoverScene";
    }


    public static class AudioConst
    {
        //オーディオID
        public const int BGM = 0;
        public const int SE = 1;

        public enum BGM_ID
        {
        }

        public enum SE_ID
        {
        }
    }
}
public enum COLOR
{
    RED = 0,
    GREEN = 1
}
public static class EnemyConst
{
    public enum TYPE { NORMAL, REFLECT, JAMMER }
    public static readonly int DOUBLE = 2;
    public static readonly int ROTATION_ANGLE = 20;
    public static readonly int TIME_SPENT_IN_RETURN = 100;
}

public static class DisplayItemConst
{
    public static readonly int MAX_SIZE = 5;
    public static readonly float ADD_SIZE = 0.5f;
}