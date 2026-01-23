//Item_Drop.cs

using System.Collections.Generic;
using UnityEngine;

public class Item_Drop : ItemBase
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
        if (collision.TryGetComponent<Enemy>(out var e))
            if (e.on_hitting)
            {
                //敵の位置取得
                v = e.transform.position;

                //ランダムでアイテムを決める
                randitem = Random.Range(0, itemList.Count);//ドロップアイテムを決定
                randdrop = Random.Range(0, 9);             //ライフドロップを決める

                //アイテムをドロップ
                if (randdrop < life_drop && Player.Instance.health < 3)
                {
                    //敵の位置に回復アイテムをドロップ
                    Instantiate(Life_item, v, Quaternion.identity);
                }
                else
                {
                    //敵の位置にアイテムを生成
                    if (drop_switch)
                        Instantiate(itemList[randitem], v, Quaternion.identity);
                }
            }
    }
}
