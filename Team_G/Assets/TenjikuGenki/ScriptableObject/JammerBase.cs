using UnityEngine;
using static JammerBase;

[CreateAssetMenu(fileName = "SO", menuName = "ScriptableObjects/JammerBase")]
public class JammerBase : ScriptableObject
{
    public float speed;
    public int score;
    public int power;
}