//ItemDrop.cs

using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    //ゲームオブジェクト
    [Header("▼ItemDropObjectData")]
    [SerializeField] List<GameObject> itemList;//アイテムリスト
    public GameObject life_item;//回復アイテムオブジェクト
    //アイテムドロップ
    [Header("▼ItemDropSetting")]
    public bool drop_switch = true;//アイテムドロップ用フラグ
    public int life_drop = 0; //回復アイテムがドロップする確率
    private int rand_drop = 0;//回復アイテムドロップ抽選用
    private int rand_item = 0;//ドロップアイテム抽選用

    void OnTriggerEnter2D(Collider2D collision)
    {
        //エネミーにぶつかったら
        if (collision.TryGetComponent<Enemy>(out var e))
            //
            if (e.on_hitting)
            {
                //ランダムでアイテムを決める
                rand_item = Random.Range(0, itemList.Count);//ドロップアイテムを決定
                rand_drop = Random.Range(0, 9);             //回復アイテムがドロップするかを決める

                //アイテムをドロップ
                if (rand_drop < life_drop && Player.Instance.health < 3)
                {
                    //敵の位置に回復アイテムを生成
                    Instantiate(life_item, e.transform.position, Quaternion.identity);
                }
                else
                {
                    //敵の位置にアイテムを生成
                    if (drop_switch)
                        Instantiate(itemList[rand_item], e.transform.position, Quaternion.identity);
                }
            }
    }
}
