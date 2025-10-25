//h_Item.cs

using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;

public class Item : MonoBehaviour
{
    public Rigidbody2D rb;
    public float item_fall_Velocity = -3.0f;  //アイテム落下速度
    public int item_id = 0;                   //アイテムの種類
    public int max_item_count = 5;            //アイテム累積上限
    public float up_speed = 0.2f;             //スピード上昇率
    public float up_sheild = 0.5f;            //シールド範囲上昇率

    private int item_count;                   //アイテム取得回数   
    private Vector2 sheild_size;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();

        sheild_size = new Vector2(3, 3);
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = new Vector2(0, item_fall_Velocity);
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        //シールドに当たった場合
        if(collision.gameObject.tag == "Sheild" || collision.gameObject.tag == "Player")
        {
            //アイテムを削除
            Destroy(gameObject);

            //アイテム累積上限じゃない場合
            if (item_count < max_item_count)
            {
                //スピード
                if (item_id == 0)
                {
                    //プレイヤーの移動スピードを上げる
                    Player.Instance.Speed += up_speed;
                    Debug.Log(Player.Instance.Speed);
                }
                //反射スピード
                else if (item_id == 1)
                {
                    //g_enemy.Instance.enemy_speed += reflect_speed;
                }
                //反射範囲
                else if (item_id == 2)
                {
                    //シールドを横に大きくする
                    sheild_size.x += up_sheild;
                    Debug.Log(sheild_size.x);
                    Sheild.Instance.transform.localScale = sheild_size;
                    Debug.Log(Sheild.Instance.transform.localScale);
                }
                //回復
                else if (item_id == 3)
                {
                    Player.Instance.Health += 1;
                }
                //ボム
                else if (item_id == 4)
                {

                }

                //アイテム累積カウント
                item_count++;
            }

        }
    }
}
