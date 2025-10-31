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
    public int heal_hp = 1;              //回復量
    public float up_speed = 0.5f;        //スピード上昇率
    public float up_sheild = 0.5f;       //シールド範囲上昇率
    public float up_reflect_speed = 0.5f;//反射スピード上昇率

    private int max_health = 3;//最大体力  
    private int[] item_count;                       //アイテム取得回数   
    private int max_bom = 3;                        //ボム最大所持数
    private Vector3 sheild_size;                    //シールドサイズ

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
        if (collision.gameObject.tag == "Item")
        {
            //アイテムを削除
            Destroy(collision.gameObject);

            //スピード
            if (collision.GetComponent<Item>().item_id == speed_item)
                //累積上限に達していなかった場合
                if (item_count[speed_item] < collision.GetComponent<Item>().max_item_count)
                {
                    //プレイヤーの移動スピードを上げる
                    Player.Instance.Speed += up_speed;
                    Debug.Log(Player.Instance.Speed);
                    //累積カウント
                    item_count[speed_item]++;
                }

            //反射スピード
            if (collision.GetComponent<Item>().item_id == reflect_item)
                //累積上限に達していなかった場合
                if (item_count[reflect_item] < collision.GetComponent<Item>().max_item_count)
                {
                    //反射スピードup
                    enemy_ref.GetComponent<g_enemy>().speed += up_reflect_speed;
                    Debug.Log(enemy_ref.GetComponent<g_enemy>().speed);
                    //累積カウント
                    item_count[reflect_item]++;
                }

            //反射範囲
            if (collision.GetComponent<Item>().item_id == sheild_item)
                //累積上限に達していなかった場合
                if (item_count[sheild_item] < collision.GetComponent<Item>().max_item_count)
                {
                    //シールドを横に大きくする
                    sheild_size.x += up_sheild;
                    Sheild.Instance.transform.localScale = sheild_size;
                    Debug.Log(Sheild.Instance.transform.localScale);
                    //累積カウント
                    item_count[sheild_item]++;
                }

            //回復
            if (collision.GetComponent<Item>().item_id == life_item)
                //プレイヤーの体力が最大じゃない場合
                if (max_health > Player.Instance.Health)
                    //プレイヤーの体力を増やす
                    Player.Instance.Health += heal_hp;

            //ボム
            if (collision.GetComponent<Item>().item_id == bomb_item)
                //ボム所持数が最大じゃない場合
                if (max_bom > Player.Instance.bom)
                {

                }
        }

    }
}

