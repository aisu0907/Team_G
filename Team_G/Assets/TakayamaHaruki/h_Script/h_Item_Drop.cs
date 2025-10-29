//h_Item_Drop.cs

using System.Collections.Generic;
using UnityEngine;

public class Item_Drop : MonoBehaviour
{
    [SerializeField] List<GameObject> itemList;//アイテムリスト
    public GameObject Life_item;   //回復アイテムオブジェクト
    public bool drop_switch = true;//アイテムドロップ
    public int life_drop = 0;  //回復アイテムがドロップする確率

    private int randdrop = 0;   //回復アイテムドロップ確率
    private int randitem = 0;   //ドロップアイテム確率
    private Vector2 v;          //敵の位置

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        //敵の位置取得
        v = new Vector2(collision.gameObject.transform.position.x, collision.gameObject.transform.position.y);
        //ランダムでアイテムを決める
        randitem = Random.Range(0, itemList.Count);//ドロップアイテムを決定
        randdrop = Random.Range(0, 9);             //ライフドロップを決める
        //エネミーに触れたら
        if (collision.gameObject.tag == "Enemy")
            //アイテムがドロップする場合
            if (drop_switch)
                if (collision.gameObject.GetComponent<g_enemy>().OnHitting == false)
                {
                    if (randdrop < life_drop)
                        //敵の位置に回復アイテムをドロップ
                        Instantiate(Life_item, v, Quaternion.identity);
                    else
                    {
                        //敵の位置にアイテムを生成
                        Instantiate(itemList[randitem], v, Quaternion.identity);
                    }
                }
    }
}
