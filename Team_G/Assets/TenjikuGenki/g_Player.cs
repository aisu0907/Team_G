using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UIElements;
using UnityEngine.Animations;

public class Player : CharacterBase
{
    Rigidbody2D rbody; 
    float axisH = 0.0f; //���x�N�g��
    float axisV = 0.0f; //�c�x�N�g��
    public GameObject sheild;
    public int bom = 0;

    public static Player Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // CharacterBase�p��
        Health = 3;     //�̗�
        Speed = 3.0f;   //�ړ����x

        // RigidBody2D��������擾����
        rbody = this.GetComponent<Rigidbody2D>();

        // ���̐���
        Instantiate(sheild);
    }

    // Update is called once per frame
    void Update()
    {
        // �L�[�擾
        axisH = Input.GetAxisRaw("Horizontal");
        axisV = Input.GetAxisRaw("Vertical");

        // �Ǐ]����
        Sheild.Instance.transform.position = new Vector2(transform.position.x, transform.position.y + 0.8f);
    }

    void FixedUpdate()
    {
        //�ړ��̓K�p
        rbody.linearVelocity = new Vector2(axisH * Speed, axisV * Speed);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
            if (!collision.gameObject.GetComponent<g_enemy>().OnHitting)
            {
                // ��e����
                Player.Instance.Health--;
                Destroy(collision.gameObject);
            }
        if (collision.gameObject.tag == "Boss")
        {
                Player.Instance.Health--;
        }
    }
}
