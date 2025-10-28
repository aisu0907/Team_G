//h_Item_Drop.cs

using System.Collections.Generic;
using UnityEngine;

public class Item_Drop : MonoBehaviour
{
    public GameObject Life_item;
    [SerializeField] List<GameObject> itemList;
    public bool drop_switch = true;

    private int life_drop = 0;
    private int randitem;
    private Vector2 v;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        drop_switch = true;
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
        randitem = Random.Range(0, itemList.Count);
        life_drop = Random.Range(0, 9);
        if (collision.gameObject.tag == "Enemy")
            if(drop_switch)
                if (collision.gameObject.GetComponent<g_enemy>().OnHitting == false)
                {
                    Instantiate(itemList[randitem], v, Quaternion.identity);

                    if (life_drop < 3)
                        Instantiate(Life_item, v, Quaternion.identity);

                }
            
    }
}
