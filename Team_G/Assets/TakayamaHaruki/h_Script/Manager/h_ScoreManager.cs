//ScoreManager

using UnityEngine;
using System.Collections.Generic;

public class ScoreManager : MonoBehaviour
{
    public float score_rate;  //エネミー同士がぶつかったときのスコア倍率
    public int item_score = 0;//アイテムスコア
    public bool score_switch; //スコア取得切り替えフラグ

    private float enemy_score = 0;//エネミースコアを保存用

    public static ScoreManager Instance { get; private set; }

    private void Awake()
    {
        //シングルトン
        Instance = this;
    }

    //エネミー衝突スコア関数
    public void OnEnemiesCollided(Enemy e1, Enemy e2)
    {
        //ぶつかったエネミー同士のIDを取得してペアを作る
        var key = (e1.GetInstanceID() ^ e2.GetInstanceID()).ToString();
        //ぶつかったエネミー同士のペアが存在しない場合
        if (!recentCollisions.Contains(key))
        {
            recentCollisions.Add(key); //ペアを追加
            enemy_score = (float)((e1.score + e2.score) * score_rate); //スコア倍率を乗せる
            
            if(score_switch)
                Score.Instance.total_score += (int)enemy_score; //スコアを追加
        }
    }

    /// <summary>
    /// アイテムスコア追加用メソッド。 アイテムを上限以上取得した際にスコアを適用します
    /// </summary>
    public void ItemScore()
    {
        Score.Instance.total_score += item_score; //スコア追加
    }

    /// <summary>
    /// エネミースコア追加用メソッド。 エネミーをぶつけて倒した際にスコアを追加します
    /// </summary>
    /// <param name="e">エネミー</param>
    public void Enemy_Score(Enemy e)
    {
        if (score_switch)
            Score.Instance.total_score += e.score; //スコア追加
    }

    private HashSet<string> recentCollisions = new HashSet<string>();//エネミーペア記憶用
}