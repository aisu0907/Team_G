//h_Item_Drop.cs

using System.Collections.Generic;
using UnityEngine;

public class Item_Drop : MonoBehaviour
{
    [SerializeField] List<GameObject> itemList;//�A�C�e�����X�g
    public GameObject Life_item;   //�񕜃A�C�e���I�u�W�F�N�g
    public bool drop_switch = true;//�A�C�e���h���b�v
    public int life_drop = 0;  //�񕜃A�C�e�����h���b�v����m��

    private int randdrop = 0;   //�񕜃A�C�e���h���b�v�m��
    private int randitem = 0;   //�h���b�v�A�C�e���m��
    private Vector2 v;          //�G�̈ʒu

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
        randitem = Random.Range(0, itemList.Count);//�h���b�v�A�C�e��������
        randdrop = Random.Range(0, 9);             //���C�t�h���b�v�����߂�
        //�G�l�~�[�ɐG�ꂽ��
        if (collision.gameObject.tag == "Enemy")
            //�A�C�e�����h���b�v����ꍇ
            if (drop_switch)
                if (collision.gameObject.GetComponent<g_enemy>().OnHitting == false)
                {
                    if (randdrop < life_drop)
                        //�G�̈ʒu�ɉ񕜃A�C�e�����h���b�v
                        Instantiate(Life_item, v, Quaternion.identity);
                    else
                    {
                        //�G�̈ʒu�ɃA�C�e���𐶐�
                        Instantiate(itemList[randitem], v, Quaternion.identity);
                    }
                }
    }
}
