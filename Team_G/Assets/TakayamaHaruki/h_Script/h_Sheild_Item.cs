//h_Sheild_Item.cs

using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;

public class Sheild_Item : ItemBase
{
    public GameObject enemy_ref;

    //アイテム効果の上昇率
    public int puls_bom = 1;             //ボムの取得量
    public int item_score = 0;           //アイテム取得時のスコア
    public int heal_hp = 1;              //回復量
    public float up_speed = 0.5f;        //スピード上昇率
    public float up_sheild = 0.5f;       //シールド範囲上昇率
    public float up_reflect_speed = 0.5f;//反射スピード上昇率

    private int max_health = 3; //最大体力  
    private int[] item_count;   //アイテム取得回数   
    private int max_bom = 3;    //ボム最大所持数
    private Vector3 sheild_size;//シールドサイズ
    public GameObject display;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //変数リセット
        item_count = new int[3];
        sheild_size = new Vector2(3, 3);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //アイテムに当たった場合
        if (collision.TryGetComponent<Item>(out var i))
        {
            //スピード
            if (i.item_id == speed_item)
                //累積上限に達していなかった場合
                if (item_count[speed_item] < i.max_item_count)
                {
                    //プレイヤーの移動スピードを上げる
                    Player.Instance.speed += up_speed;
                    Debug.Log(Player.Instance.speed);
                    //累積カウント
                    item_count[speed_item]++;

                    // アイテムの表示
                    var d = Instantiate(display).GetComponent<DisplayItem>();
                    d.SummonDisplay(i.GetComponent<SpriteRenderer>().sprite);
                }
                else
                    Score_Manager.Instance.ItemScore();

            //反射スピード
            if (i.item_id == reflect_item)
                //累積上限に達していなかった場合
                if (item_count[reflect_item] < i.max_item_count)
                {
                    //反射スピードup
                    if (collision.TryGetComponent<Enemy>(out var e)) e.speed += up_reflect_speed;
                    //累積カウント
                    item_count[reflect_item]++;
                }
                else
                    Score_Manager.Instance.ItemScore();

            //反射範囲
            if (i.item_id == sheild_item)
                    //累積上限に達していなかった場合
                    if (item_count[sheild_item] < i.max_item_count)
                    {
                        //シールドを横に大きくする
                        sheild_size.x += up_sheild;
                        Sheild.Instance.transform.localScale = sheild_size;
                        Debug.Log(Sheild.Instance.transform.localScale);
                        //累積カウント
                        item_count[sheild_item]++;
                    }
                    else
                        Score_Manager.Instance.ItemScore();

            //回復
            if (i.item_id == life_item)
                //プレイヤーの体力が最大じゃない場合
                if (max_health > Player.Instance.health)
                    //プレイヤーの体力を増やす
                    Player.Instance.health += heal_hp;

            //ボム
            if (i.item_id == bomb_item)
                //ボム所持数が最大じゃない場合
                if (max_bom > Player.Instance.bom)
                {

                }

        //アイテムを削除
        Destroy(i.gameObject);

        }
    }
}

