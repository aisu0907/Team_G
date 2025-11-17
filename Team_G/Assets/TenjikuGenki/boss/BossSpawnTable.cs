using UnityEngine;

[CreateAssetMenu(fileName = "SO", menuName = "ScriptableObjects/BossSpawnTable")]
public class BossSpawnTable : ScriptableObject
{
    public GameObject prefab;
    public int timer;
}