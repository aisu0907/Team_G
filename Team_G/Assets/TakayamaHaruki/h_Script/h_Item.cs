using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;

public class Item : MonoBehaviour
{
    public Rigidbody2D rb;
    public float fall_Velocity = -3.0f;  //アイテム落下速度
    public int item_id = 0; //アイテムの種類
    public int max_up_count = 5;
    public float up_speed = 0.2f;

    private int [] item_count = { 0 };   
    private Vector2 v;
    public static Item Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = new Vector2(0, fall_Velocity);
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        //シールドに当たった場合
        if(collision.gameObject.tag == "Sheild" || collision.gameObject.tag == "Player")
        {
            //アイテムを削除
            Destroy(gameObject);

            //スピード
            if (item_id == 0)
            {
                if (item_count[0] < max_up_count)
                {
                    //アイテム累積カウント
                    item_count[0]++;
                    //プレイヤーのスピードを上げる
                    Player.Instance.Speed += up_speed;
                    Debug.Log(Player.Instance.Speed);
                }
             }
            //反射スピード
            else if (item_id == 1)
            {
                if (item_count[1] < max_up_count)
                {
                    item_count[0]++;
                    //g_enemy.Instance.enemy_speed += reflect_speed;
                    
                }
            }
            //反射範囲
            else if (item_id == 2)
            {

            }
            //回復
            else if(item_id == 3)
            {

            }
            //ボム
            else if(item_id == 4)
            {

            }

        }
    }
}
