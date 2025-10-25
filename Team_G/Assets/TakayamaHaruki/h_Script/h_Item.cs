//h_Item.cs

using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;

public class Item : MonoBehaviour
{
    public Rigidbody2D rb;
    public float item_fall_Velocity = -3.0f;  //�A�C�e���������x
    public int item_id = 0;                   //�A�C�e���̎��
    public int max_item_count = 5;            //�A�C�e���ݐϏ��
    public float up_speed = 0.2f;             //�X�s�[�h�㏸��
    public float up_sheild = 0.5f;            //�V�[���h�͈͏㏸��

    private int item_count;                   //�A�C�e���擾��   
    private Vector2 sheild_size;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();

        sheild_size = new Vector2(3, 3);
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = new Vector2(0, item_fall_Velocity);
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        //�V�[���h�ɓ��������ꍇ
        if(collision.gameObject.tag == "Sheild" || collision.gameObject.tag == "Player")
        {
            //�A�C�e�����폜
            Destroy(gameObject);

            //�A�C�e���ݐϏ������Ȃ��ꍇ
            if (item_count < max_item_count)
            {
                //�X�s�[�h
                if (item_id == 0)
                {
                    //�v���C���[�̈ړ��X�s�[�h���グ��
                    Player.Instance.Speed += up_speed;
                    Debug.Log(Player.Instance.Speed);
                }
                //���˃X�s�[�h
                else if (item_id == 1)
                {
                    //g_enemy.Instance.enemy_speed += reflect_speed;
                }
                //���˔͈�
                else if (item_id == 2)
                {
                    //�V�[���h�����ɑ傫������
                    sheild_size.x += up_sheild;
                    Debug.Log(sheild_size.x);
                    Sheild.Instance.transform.localScale = sheild_size;
                    Debug.Log(Sheild.Instance.transform.localScale);
                }
                //��
                else if (item_id == 3)
                {
                    Player.Instance.Health += 1;
                }
                //�{��
                else if (item_id == 4)
                {

                }

                //�A�C�e���ݐσJ�E���g
                item_count++;
            }

        }
    }
}
