using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UIElements;
using UnityEngine.Animations;

public class Player : MonoBehaviour
{
    Rigidbody2D rbody; 
    float axisH = 0.0f; //���x�N�g��
    float axisV = 0.0f; //�c�x�N�g��
    public GameObject sheild;
    public int bom = 0;
    public int health = 3;     //�̗�
    public float speed = 3.0f;   //�ړ����x

    public static Player Instance { get; private set; }

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
        // �L�[�擾
        axisH = Input.GetAxisRaw("Horizontal");
        axisV = Input.GetAxisRaw("Vertical");
        if (0.2 <= transform.position.y) axisV = -0.05f;
        if (transform.position.y <= -4.5) axisV = 0.05f;

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
        if (collision.gameObject.tag == "Enemy")
            if (!collision.gameObject.GetComponent<Enemy>().on_hitting)
            {
                // ��e����
                Player.Instance.health--;
                Destroy(collision.gameObject);
            }
        if (collision.gameObject.tag == "Boss")
        {
                Player.Instance.health--;
        }
    }
}
