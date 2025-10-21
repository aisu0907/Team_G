using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class g_enemy : UnitBase
{
    public Rigidbody2D rbody;   //����
    public Vector2 v;           //�x�N�g��

    public int EnemyColor = 1;          //�F
    public int EnemyType = 1;           //���
    [SerializeField] List<Sprite> Img;   //�摜

    public bool World = true;
    public bool OnHitting = false;
    int timer = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    { 
        rbody = GetComponent<Rigidbody2D>();

        // UnitBase�p��
        Speed = 3.0f;   //�ړ����x
        Health = 3;     //�̗�
    }

    // Update is called once per frame
    void Update()
    {
        rbody.linearVelocity = v;
    }

    void FixedUpdate()
    {
        // ���x����
        if (rbody.linearVelocity.magnitude != Speed)
        {
            rbody.linearVelocity = rbody.linearVelocity.normalized * Speed;
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
                    v = new Vector2(0, -Speed);
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
        }
    }

    public void RandCreate(Vector2 _v)
    {
        // �x�N�g���̐ݒ�
        //v = new Vector2(0, -speed);

        // �������N�Ȃ̂����߂�
        // �`�F�E��ޕҁ`
        int[] Index = { Random.Range(0, 2), Random.Range(0, 2) };
        EnemyType = Index[0];
        EnemyColor = Index[1];

        // �`�摜�ҁ`
        SpriteRenderer img = GetComponent<SpriteRenderer>();
        img.sprite = Img[Index[0] * 2 + Index[1]];
    }

    public void Create(Vector2 _Pos, Vector2 _v, int _type, int _color)
    {
        transform.position = _Pos;
        v = _v;
        EnemyType = _type;
        EnemyColor = _color;
        SpriteRenderer img = GetComponent<SpriteRenderer>();
        img.sprite = Img[_type * 2 + _color];
    }
}
