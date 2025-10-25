//h_Item_Drop.cs

using System.Collections.Generic;
using UnityEngine;

public class Item_Drop : MonoBehaviour
{
    public GameObject Life_item;
    [SerializeField] List<GameObject> itemList;
    Vector2 v;

    private int life_drop = 0;
    private int randitem;

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
        //�G�̈ʒu�擾
        v = new Vector2(collision.gameObject.transform.position.x, collision.gameObject.transform.position.y);
        //�����_���ŃA�C�e�������߂�
        randitem = Random.Range(0, itemList.Count);
        life_drop = Random.Range(0, 9);
        if (collision.gameObject.tag == "Enemy")
            if (collision.gameObject.GetComponent<g_enemy>().OnHitting == false)
            {
                Instantiate(itemList[randitem], v, Quaternion.identity);

                if(life_drop < 3)
                    Instantiate(Life_item, v, Quaternion.identity);

            }
    }
}
