using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class g_enemy : CharacterBase
{
    public Rigidbody2D rb;   //����
    public Vector2 vec;           //�x�N�g��

    public Vector2 pos;
    public float speed = 1f;            //�ō����x
    public int EnemyColor = 1;          //�F
    public int EnemyType = 1;           //���
    [SerializeField] List<Sprite> Img;   //�摜

    public bool OnHitting = false;
    int timer = 0;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transform.position = pos;
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = vec;
    }

    void FixedUpdate()
    {
        // ���x����
        if (rb.linearVelocity.magnitude != speed)
        {
            rb.linearVelocity = vec.normalized * speed;
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
                    vec = new Vector2(0, -speed);
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

    public void Create(Vector2 _pos, Vector2 _vec, int _type, int _color, float _speed)
    {
        pos = _pos;
        vec = _vec;
        EnemyType = _type;
        EnemyColor = _color;
        speed = _speed;
        SpriteRenderer img = GetComponent<SpriteRenderer>();
        img.sprite = Img[EnemyType * 2 + EnemyColor];
    }


    public void RandCreate(Vector2 _pos, Vector2 _vec, float _speed)
    {
        pos = _pos;
        vec = _vec;
        EnemyType = Random.Range(0, 2);
        EnemyColor = Random.Range(0, 2);
        speed = _speed;
        SpriteRenderer img = GetComponent<SpriteRenderer>();
        img.sprite = Img[EnemyType * 2 + EnemyColor];
    }
}