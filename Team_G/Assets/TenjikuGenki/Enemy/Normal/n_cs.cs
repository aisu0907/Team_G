using System.Collections.Generic;
using UnityEngine;

public class ENormal : Enemy
{
    Rigidbody2D rb;
    public List<Sprite> Img;

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
        // Spin
        if (OnHitting)
            transform.Rotate(0, 0, 20);
    }

    void FixedUpdate()
    {
        // Fix Vector
        rb.linearVelocity = Vec;
        if (rb.linearVelocity.magnitude != Speed)
        {
            rb.linearVelocity = Vec.normalized * Speed;
        }
    }

    public void Init(EnemyBase db, Vector2 _Vec, int _Color)
    {
        // Initialize Status
        Type = (int)db.Type;
        Color = _Color;
        Vec = _Vec;
        Speed = db.Speed;
        Score = db.Score;
        Power = db.Power;

        // Change Img
        SpriteRenderer img = GetComponent<SpriteRenderer>();
        img.sprite = Img[Color];

        // Decision Vector
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = Vec * Speed;
    }
}