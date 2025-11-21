using UnityEngine;

[CreateAssetMenu(fileName = "SO", menuName = "ScriptableObjects/EnemyData")]
public class EnemyData : ScriptableObject
{
    public types type;
    public float speed;
    public int score;
    public int power;

    public enum types { Normal, Reflect, Jammer };
}