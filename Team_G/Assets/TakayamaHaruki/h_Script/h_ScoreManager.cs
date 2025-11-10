using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class h_ScoreManager : MonoBehaviour
{
    public float score_rate = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnEnemiesCollided(Enemy e1, Enemy e2)
    {
        var key = (e1.GetInstanceID() ^ e2.GetInstanceID()).ToString();
        if(!recentCollisions.Contains(key))
        {
            recentCollisions.Add(key);
            //Score.Instance.total_score += (float)((e1.score + e2.score) * score_rate);
        }
    }

    private HashSet<string> recentCollisions = new HashSet<string>();
}
