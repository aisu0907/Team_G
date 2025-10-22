using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class g_enemy : CharacterBase
{
    public Rigidbody2D rbody;   //����
    public Vector2 v;           //�x�N�g��

    public float speed = 1f;            //�ō����x
    public int EnemyColor = 1;          //�F
    public int EnemyType = 1;           //���
    [SerializeField] List<Sprite> Img;   //�摜

    public bool OnHitting = false;
    int timer = 0;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
    {
        // CharacterBase�p��
        Health = 1;     //�̗�
        Speed = 1.0f;   //�ړ����x
        

        // �x�N�g���̐ݒ�
        rbody = this.GetComponent<Rigidbody2D>();
        v = new Vector2(0, -Speed);

        // �������N�Ȃ̂����߂�
        // �`�F�E��ޕҁ`
        int[] Index = { Random.Range(0, 2), Random.Range(0, 2) };
        EnemyType = Index[0];
        EnemyColor = Index[1];

        // �`�摜�ҁ`
        SpriteRenderer img = GetComponent<SpriteRenderer>();
        img.sprite = Img[ Index[0] * 2 + Index[1] ];
    }

    // Update is called once per frame
    void Update()
    {
        rbody.linearVelocity = v;
    }

    void FixedUpdate()
    {
        // ���x����
        if (rbody.linearVelocity.magnitude != speed)
        {
            rbody.linearVelocity = rbody.linearVelocity.normalized * speed;
        }

        // �Փˎ�����
        if (OnHitting)
        {
            transform.Rotate(0, 0, 20);
            //�@���˃E�C���X�Ȃ畜�A���Ă���
            if (EnemyType == 1)
            {
                timer++;
                if (timer >= 100)
                {
                    OnHitting = false;
                    transform.localRotation = default;
                    v = new Vector2(0, -speed);
                    timer = 0;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && collision.gameObject.GetComponent<g_enemy>().OnHitting)
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }
}
