//ItemText.cs

using UnityEngine;
using TMPro;

public class ItemText : ItemBase
{
    [Header("▼Display Time")]
    public int display_on;//テキスト表示時間

    private TMP_Text item_text_display;//テキストコンポーネント
    private int display_time;//テキスト表示時間カウント用
    //テキスト
    private string text;     //表示するテキスト
    private bool text_switch;//テキスト表示用フラグ
    //色関係
    private Color default_color;//通常の色
    private Color null_color;   //透明色
    //テキスト位置
    private Vector2 text_pos;
    public static ItemText Instance { get; private set; }
    void Awake()
    {
        //シングルトン
        Instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //リセット
        display_time = 0;
        item_text_display = GetComponent<TMP_Text>(); //コンポーネントを取得
        //色設定
        null_color = item_text_display.color;
        default_color = new Color(item_text_display.color.r, item_text_display.color.g, item_text_display.color.b, 1);
    }

    // Update is called once per frame
    void Update()
    {
        //プレイヤーに追従
        text_pos = Player.Instance.transform.position;
        gameObject.transform.position = text_pos;

        //アイテムを拾ったら
        if(text_switch)
        {
            //タイムカウント
            display_time++;
            
            //表示時間が終わったら
            if(display_time > display_on)
            {
                //リセット
                item_text_display.color = null_color; //テキスト表示を消す
                text_switch = false;
                display_time = 0;
            }
        }
    }

    /// <summary>
    /// 表示するテキストを取得する用のメソッド。 取得したアイテムに応じてテキストを変更します。
    /// </summary>
    /// <param name="i">取得したアイテム</param>
    public void ItemUpText(Item i)
    {
        //対応したアイテムのテキストを代入
        if (i.item_id == speed_item)   text =  "スピードアップ！";
        if (i.item_id == reflect_item) text = "ハンシャスピードアップ!";
        if (i.item_id == shield_item)  text = "シールドカクダイ！";
        if (i.item_id == life_item)    text = "HP回復！";

        //リセット
        display_time = 0;
        text_switch = true;
        item_text_display.text = text; //取得したテキスト代入
        item_text_display.color = default_color; //テキストを表示
    }
}
