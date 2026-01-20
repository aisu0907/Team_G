using UnityEngine;
using TMPro;

public class h_Item_Text : ItemBase
{
    public int display_off; //テキスト表示時間
    //テキストコンポーネント
    private TMP_Text item_text_display;
    //タイム系
    private int display_time;
    //テキスト
    private string text;
    private bool text_switch;
    //色関係
    private Color off_color;
    private Color on_color;
    //テキスト位置
    private Vector2 text_pos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static h_Item_Text Instance { get; private set; }
    void Awake()
    {
        Instance = this;
    }
/// <summary>
/// 
/// </summary>
    void Start()
    {
        //リセット
        display_time = 0;
        item_text_display = GetComponent<TMP_Text>();
        //色設定
        off_color = new Color(item_text_display.color.r, item_text_display.color.g, item_text_display.color.b, 0);
        on_color = new Color(item_text_display.color.r, item_text_display.color.g, item_text_display.color.b, 1);
        item_text_display.color = off_color;
    }

    // Update is called once per frame
    void Update()
    {
        //プレイヤーに追従
        text_pos = Player.Instance.transform.position;
        gameObject.transform.position = text_pos;

        //アイテムを拾ったら
        if(!text_switch)
        {
            display_time++;
            
            //表示時間が終わったら
            if(display_time > display_off)
            {
                //リセット
                item_text_display.color = off_color; //テキスト表示を消す
                text_switch = true;
                display_time = 0;
            }
        }
    }

    public void Item_Up_Text(Item i)
    {
        //対応したアイテムのテキストを代入
        if (i.item_id == speed_item)   text =  "スピードアップ！";
        if (i.item_id == reflect_item) text = "ハンシャスピードアップ!";
        if (i.item_id == sheild_item)  text = "シールドカクダイ！";
        if (i.item_id == life_item)    text = "HP回復！";

        display_time = 0;
        text_switch = false;
        item_text_display.text = text;
        item_text_display.color = on_color;
    }
}
