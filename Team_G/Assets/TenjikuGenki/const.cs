// íËêîÉNÉâÉX
using UnityEngine;

public enum COLOR
{
    RED, GREEN
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

public static class WindowConst
{
    public static readonly Vector2 POS_1 = new Vector2(-3, 3);
    public static readonly Vector2 POS_2 = new Vector2(-1, 1);
    public static readonly Vector2 POS_3 = new Vector2(3.5f, 1.5f);
}