using UnityEngine;

[CreateAssetMenu(fileName = "SO", menuName = "ScriptableObjects/EnemyBase")]
public class EnemyBase : ScriptableObject
{
    public types Type;
    public float Speed;
    public int Score;
    public int Power;

    public enum types { Normal, Reflect, Anti };
}