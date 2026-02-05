//ShieldItem.cs

using UnityEngine;

public class Shield_Item : ItemBase
{
    [Header("Object Data")]
    public GameObject display;

    //アイテム効果の上昇率
    [Header("Item EffectUp Setting")]
    public int plus_bom = 1;             //ボムの取得量
    public int item_score = 0;           //アイテム取得時のスコア
    public int heal_hp = 1;              //回復量
    public float up_speed = 0.5f;        //スピード上昇率
    public float up_shield = 0.5f;       //シールド範囲上昇率
    public float up_reflect_speed = 0.5f;//反射スピード上昇率
    public float reflect_speed;          //反射スピード保存用

    public int[] item_count;//アイテム取得回数

    private int max_health = 3; //最大体力  
    private Vector2 shield_size;//シールドサイズ
    private Player player;

    public static Shield_Item Instance { get; private set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        //シングルトン
        Instance = this;
    }

    void Start()
    {
        //変数リセット
        item_count = new int[3];
        shield_size = new Vector2(3, 3);
        player = Player.Instance;

        // データがあったら引き継ぐ
        if((DataHolder.game_phaze > 0))
        {
            // 速度
            player.speed += up_speed * DataHolder.player_took_item[0];

            reflect_speed += up_reflect_speed * DataHolder.player_took_item[1];
            // 盾の大きさ
            shield_size.x += up_shield * DataHolder.player_took_item[2];
            Shield.Instance.transform.localScale = shield_size;

            // 回数の同期
            for(int i = 0; i < 3; i++) item_count[i] = DataHolder.player_took_item[i];
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //アイテムに当たった場合
        if (collision.TryGetComponent<Item>(out var i))
        {
            //音を鳴らす
            AudioManager.instance.PlaySound("GetItem");

            ItemGet(i, i.item_id);

            //アイテムを削除
            Destroy(i.gameObject);
        }
    }

    /// <summary>
    /// アイテム取得時の演出用メソッド。 取得したアイテムを大きく表示する演出を表示します。
    /// </summary>
    /// <param name="i">取得したアイテム</param>
    /// <param name="item_id">アイテムID/param>
     public void ItemGet(Item i, int item_id)
    {
        //回復アイテム
        if (i.item_id == life_item)
            //プレイヤーの体力が最大じゃない場合
            if (max_health > player.health)
            {
                //プレイヤーの体力を増やす
                player.health += heal_hp;

                //アイテムテキストを表示
                ItemText.Instance.ItemUpText(i);

                return;
            }
            else
            {
                ScoreManager.Instance.ItemScore();
                return;
            }

        if (item_count[item_id] < i.max_item_count)
        {
            //対応したアイテムの効果を適用する
            {
                //移動速度アップアイテム
                if (i.item_id == speed_item)
                    player.speed += up_speed; //プレイヤーの移動スピードを上げる
                
                //反射速度アップアイテム
                if (i.item_id == reflect_speed)
                   reflect_speed += up_reflect_speed; //反射スピードup
                
                //反射範囲アップアイテム
                if (i.item_id == shield_item)
                {
                    //シールドを横に大きくする
                    shield_size.x += up_shield;
                    Shield.Instance.transform.localScale = shield_size;
                }

            }

            item_count[item_id]++; //累積カウント

            ItemGetEffect(i); //アイテム演出メソッド
        }
        else
            ScoreManager.Instance.ItemScore();

    }

    /// <summary>
    /// アイテム取得演出用メソッド。 アイテム取得時に呼び出し対応したアイテムの演出を表示します。
    /// </summary>
    /// <param name="i">取得したアイテム</param>
    void ItemGetEffect(Item i)
    {
        //アイテムの表示
        Get_Item ui = i.GetComponent<Get_Item>();

        var d = Instantiate(display, gameObject.transform.position, Quaternion.Euler(0, 0, 10)).GetComponent<DisplayItem>();
        d.SummonDisplay(i.GetComponent<SpriteRenderer>().sprite);

        //テキスト表示
        ItemText.Instance.ItemUpText(i);
    }
}

