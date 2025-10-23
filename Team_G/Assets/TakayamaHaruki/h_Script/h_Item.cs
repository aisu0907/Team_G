using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;

public class Item : MonoBehaviour
{
    public Rigidbody2D rb;
    public float fall_Velocity = -3.0f;  //�A�C�e���������x
    public int item_id = 0; //�A�C�e���̎��
    public int max_up_count = 5;
    public float up_speed = 0.2f;

    private int [] item_count = { 0 };   
    private Vector2 v;
    public static Item Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = new Vector2(0, fall_Velocity);
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        //�V�[���h�ɓ��������ꍇ
        if(collision.gameObject.tag == "Sheild" || collision.gameObject.tag == "Player")
        {
            //�A�C�e�����폜
            Destroy(gameObject);

            //�X�s�[�h
            if (item_id == 0)
            {
                if (item_count[0] < max_up_count)
                {
                    //�A�C�e���ݐσJ�E���g
                    item_count[0]++;
                    //�v���C���[�̃X�s�[�h���グ��
                    Player.Instance.Speed += up_speed;
                    Debug.Log(Player.Instance.Speed);
                }
             }
            //���˃X�s�[�h
            else if (item_id == 1)
            {
                if (item_count[1] < max_up_count)
                {
                    item_count[0]++;
                    //g_enemy.Instance.enemy_speed += reflect_speed;
                    
                }
            }
            //���˔͈�
            else if (item_id == 2)
            {

            }
            //��
            else if(item_id == 3)
            {

            }
            //�{��
            else if(item_id == 4)
            {

            }

        }
    }
}
