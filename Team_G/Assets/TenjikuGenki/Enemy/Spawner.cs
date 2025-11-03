using UnityEngine;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
    public List<GameObject> Prefab;
    public List<EnemyBase> Enemy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int color = Random.Range(0, Prefab.Count);
        // if (color == 0) { var e = Instantiate(Prefab[color]).GetComponent<ENormal>(); e.Init(Enemy[color], new Vector2(0, -1), color); }
        // if (color == 1) { var e = Instantiate(Prefab[color]).GetComponent<EReflect>(); e.Init(Enemy[color], new Vector2(0, -1), color); }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
