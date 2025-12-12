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
    public int[] item_count;   //アイテム取得回数   
    private int max_bom = 3;    //ボム最大所持数
    private Vector3 sheild_size;//シールドサイズ
    public GameObject display;
    public Player player;

    List<GameObject> item_name;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //変数リセット
        item_count = new int[3];
        sheild_size = new Vector2(3, 3);
        player = Player.Instance;

        // データがあったら引き継ぐ
        if(!(DataHolder.game_phaze < 0))
        {
            // 速度
            player.speed += up_speed * DataHolder.player_took_item[0];

            // 盾の大きさ
            sheild_size.x += up_sheild * DataHolder.player_took_item[2];
            Sheild.Instance.transform.localScale = sheild_size;

            // 回数の同期
            for(int i = 0; i < 3; i++) item_count[i] = DataHolder.player_took_item[i];
        }
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
            AudioManager.instance.PlaySound("GetItem");
            //スピード
            if (i.item_id == speed_item)
                //累積上限に達していなかった場合
                if (item_count[speed_item] < i.max_item_count)
                {
                    //プレイヤーの移動スピードを上げる
                    player.speed += up_speed;
                    Debug.Log(Player.Instance.speed);
                    //累積カウント
                    item_count[speed_item]++;

                    SummonDisplay(i);
                }
                else
                    Score_Manager.Instance.ItemScore();

            //反射スピード
            if (i.item_id == reflect_item)
                //累積上限に達していなかった場合
                if (item_count[reflect_item] < i.max_item_count)
                {
                    //反射スピードup
                    //if (collision.TryGetComponent<Enemy>(out var e)) e.speed += up_reflect_speed;
                    //累積カウント
                    item_count[reflect_item]++;

                    SummonDisplay(i);
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

                        SummonDisplay(i);
                    }
                    else
                        Score_Manager.Instance.ItemScore();

            //回復
            if (i.item_id == life_item)
                //プレイヤーの体力が最大じゃない場合
                if (max_health > player.health)
                    //プレイヤーの体力を増やす
                    player.health += heal_hp;

            //ボム
            if (i.item_id == bomb_item)
                //ボム所持数が最大じゃない場合
                if (max_bom > player.bom)
                {

                }

        //アイテムを削除
        Destroy(i.gameObject);

        }
    }

    void SummonDisplay(Item i)
    {
        // アイテムの表示
        var d = Instantiate(display, transform.position,Quaternion.Euler(0, 0, 10)).GetComponent<DisplayItem>();
        d.SummonDisplay(i.GetComponent<SpriteRenderer>().sprite);
    }

    void air_ride(Item i)
    {




    }
}

