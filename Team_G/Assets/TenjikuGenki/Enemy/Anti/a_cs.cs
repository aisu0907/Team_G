using System.Collections.Generic;
using UnityEngine;

public class EAnti : Enemy, IDamageable
{
    Rigidbody2D rb;
    public List<Sprite> Img;
    int timer;

    void Awake()
    {
        ;
    }

    void Start()
    {
        ;
    }

    void Update()
    {
        ;
    }

    void FixedUpdate()
    {
        ;
    }

    public void Init(EnemyBase db, Vector2 _vec, int _color, float _speed)
    {
        // Initialize Status
        type = (int)db.type;
        color = _color;
        vec = _vec;
        speed = _speed;
        score = db.score;
        power = db.power;

        // Change Img
        SpriteRenderer img = GetComponent<SpriteRenderer>();
        img.sprite = Img[color];

        // Decision Vector
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = vec * speed;
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (IsHitEnemy(collision.gameObject)) Delete(collision);
    }
}