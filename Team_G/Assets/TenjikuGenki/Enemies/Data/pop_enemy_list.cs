using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/PopEnemyList")]
public class PopEnemyList : ScriptableObject
{
    public List<EnemyData> list;
    public int spawn_timer;
}