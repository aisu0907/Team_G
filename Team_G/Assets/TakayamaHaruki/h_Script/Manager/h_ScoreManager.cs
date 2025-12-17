//h_Score_Manager

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class Score_Manager : MonoBehaviour
{
    public float score_rate = 0; //エネミー同士がぶつかったときのスコア倍率
    public int item_score = 0;   //アイテムスコア

    private float enemy_score = 0; //エネミースコアを保存用

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

    }

    //エネミー衝突スコア関数
    public void OnEnemiesCollided(Enemy e1, Enemy e2)
    {
        //ぶつかったエネミー同士のIDを取得してペアを作る
        var key = (e1.GetInstanceID() ^ e2.GetInstanceID()).ToString();
        //ぶつかったエネミー同士のペアが存在しない場合
        if(!recentCollisions.Contains(key))
        {
            recentCollisions.Add(key); //ペアを追加
            enemy_score = (float)((e1.score + e2.score) * score_rate); //スコア倍率を乗せる
            
            Score.Instance.total_score += (int)enemy_score; //スコアを追加
        }
    }

    //アイテムスコア関数
    public void ItemScore()
    {
        Score.Instance.total_score += item_score; //スコア追加
    }

    //エネミースコア関数
    public void Enemy_Score(Enemy e)
    {
        Score.Instance.total_score += e.score; //スコア追加
    }

    private HashSet<string> recentCollisions = new HashSet<string>();
}
