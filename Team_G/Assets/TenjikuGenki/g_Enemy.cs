using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class g_enemy : UnitBase
{
    public Rigidbody2D rbody;   //物理
    public Vector2 v;           //ベクトル

    public int EnemyColor = 1;          //色
    public int EnemyType = 1;           //種類
    [SerializeField] List<Sprite> Img;   //画像

    public bool World = true;
    public bool OnHitting = false;
    int timer = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    { 
        rbody = GetComponent<Rigidbody2D>();

        // UnitBase継承
        Speed = 3.0f;   //移動速度
        Health = 3;     //体力
    }

    // Update is called once per frame
    void Update()
    {
        rbody.linearVelocity = v;
    }

    void FixedUpdate()
    {
        // 速度調整
        if (rbody.linearVelocity.magnitude != Speed)
        {
            rbody.linearVelocity = rbody.linearVelocity.normalized * Speed;
        }

        // 衝突時処理
        if (OnHitting)
        {
            transform.Rotate(0, 0, 20);
            //　反射ウイルスなら復帰してくる
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
        // ベクトルの設定
        //v = new Vector2(0, -speed);

        // 自分が誰なのか決める
        // ～色・種類編～
        int[] Index = { Random.Range(0, 2), Random.Range(0, 2) };
        EnemyType = Index[0];
        EnemyColor = Index[1];

        // ～画像編～
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
