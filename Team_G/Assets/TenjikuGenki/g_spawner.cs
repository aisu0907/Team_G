using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class g_spawner : MonoBehaviour
{
    GameObject Enemy;
    public Vector2 Min, Max;
    float Duration, Timer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        
    }

    void Update()
    {
        ++Timer;
        if(Duration >= Timer)
        {
            Timer = 0;
            float X = Random.Range(Min.x, Max.x);
            float Y = Random.Range(Min.y, Max.y);
            Instantiate(Enemy, new Vector2(X, Y), Quaternion.identity);
        }
    }
}
