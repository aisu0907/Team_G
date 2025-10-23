using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class g_enemy : CharacterBase
{
    public Rigidbody2D rbody;   //物理
    public Vector2 v;           //ベクトル

    public float speed = 1f;            //最高速度
    public int EnemyColor = 1;          //色
    public int EnemyType = 1;           //種類
    [SerializeField] List<Sprite> Img;   //画像

    public bool OnHitting = false;
    int timer = 0;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
    {
        // CharacterBase継承
        Health = 1;     //体力
        Speed = 1.0f;   //移動速度
        

        // ベクトルの設定
        rbody = this.GetComponent<Rigidbody2D>();
        v = new Vector2(0, -Speed);

        // 自分が誰なのか決める
        // ～色・種類編～
        int[] Index = { Random.Range(0, 2), Random.Range(0, 2) };
        EnemyType = Index[0];
        EnemyColor = Index[1];

        // ～画像編～
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
        // 速度調整
        if (rbody.linearVelocity.magnitude != speed)
        {
            rbody.linearVelocity = rbody.linearVelocity.normalized * speed;
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
