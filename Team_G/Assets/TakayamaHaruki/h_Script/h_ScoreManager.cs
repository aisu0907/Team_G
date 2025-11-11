//h_Score_Manager

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class Score_Manager : MonoBehaviour
{
    public float score_rate = 0;
    public int item_score = 0;

    private float enemy_score = 0;

    public static Score_Manager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    //    void OnCollisionEnter2D(Collision2D collision)
    //    {
    //        Enemy other = collision.gameObject.GetComponent<Enemy>();
    //        if (other != null)
    //        {
    //            Score_Manager.Instance.OnEnemiesCollided(this, other);
    //        }
    //    }
    }

    public void OnEnemiesCollided(Enemy e1, Enemy e2)
    {
        var key = (e1.GetInstanceID() ^ e2.GetInstanceID()).ToString();
        if(!recentCollisions.Contains(key))
        {
            recentCollisions.Add(key);
            enemy_score = (float)((e1.score + e2.score) * score_rate);
            Score.Instance.total_score += (int)enemy_score;
        }
    }

    public void ItemScore()
    {
        Score.Instance.total_score += item_score;
    }

    private HashSet<string> recentCollisions = new HashSet<string>();
}
