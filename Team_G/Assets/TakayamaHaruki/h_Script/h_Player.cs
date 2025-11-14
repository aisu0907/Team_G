using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UIElements;
using UnityEngine.Animations;
using NUnit.Framework.Internal;
using System.Data;

public class h_Player : MonoBehaviour
{
    Rigidbody2D rbody; 
    float axisH, axisV = 0.0f;
    public GameObject sheild;
    public int bom = 0;
    public int bom_time = 0;
    public int health = 3;     //�̗�
    public float speed = 3.0f;   //�ړ����x


    private int frame = 0;
    public static h_Player Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // RigidBody2D��������擾����
        rbody = this.GetComponent<Rigidbody2D>();

        // ���̐���
        Instantiate(sheild);
    }

    // Update is called once per frame
    void Update()
    {
        frame++;
        // �L�[�擾
        axisH = Input.GetAxisRaw("Horizontal");
        axisV = Input.GetAxisRaw("Vertical");
        if (0.2 <= transform.position.y) axisV = -0.05f;
        if (transform.position.y <= -4.5) axisV = 0.05f;

        if( frame >= bom_time)
        {
            if(Input.GetKey(KeyCode.Space) && bom > 0)
            {
                frame = 0;
                // "Block"タグがついたすべてのオブジェクトを取得
                GameObject[] objects = GameObject.FindGameObjectsWithTag("Enemy");

                // 各オブジェクトを削除
                foreach (GameObject obj in objects)
                {
                    Destroy(obj);
                }
                bom--;
            }
        }
            // �Ǐ]����
        Sheild.Instance.transform.position = new Vector2(transform.position.x, transform.position.y + 0.8f);
    }

    void FixedUpdate()
    {
        //�ړ��̓K�p
        rbody.linearVelocity = new Vector2(axisH * speed, axisV * speed);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<IDamageable>(out var hit))
            if (!collision.GetComponent<Enemy>().on_hitting) hit.Damage();

    }
}
