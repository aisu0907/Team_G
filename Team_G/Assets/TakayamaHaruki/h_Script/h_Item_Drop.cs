using System.Collections.Generic;
using UnityEngine;

public class h_Item_Drop : MonoBehaviour
{
    [SerializeField] List<GameObject> itemList;
    Vector2 v;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //敵の位置取得
        v = new Vector2(collision.gameObject.transform.position.x, collision.gameObject.transform.position.y);
        //ランダムでアイテムを決める
        int randitem = Random.Range(0, itemList.Count);

        if (collision.gameObject.GetComponent<g_enemy>().OnHitting == false)
        {
            Instantiate(itemList[randitem], v, Quaternion.identity);

        }
    }
}
