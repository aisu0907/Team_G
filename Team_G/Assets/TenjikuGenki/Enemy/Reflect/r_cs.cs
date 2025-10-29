using System.Collections.Generic;
using UnityEngine;

public class EReflect : Enemy
{
    Rigidbody2D rb;
    public List<Sprite> Img;
    public int timer;

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
        if (on_hitting)
        {
            transform.Rotate(0, 0, 20);
            timer++;
            if (timer >= 100)
            {
                on_hitting = false;
                transform.localRotation = default;
                vec = new Vector2(0, -speed);
                timer = 0;
            }
        }
    }

    void FixedUpdate()
    {
        // Fix Vector
        rb.linearVelocity = vec;
        if (rb.linearVelocity.magnitude != speed)
        {
            rb.linearVelocity = vec.normalized * speed;
        }
    }

    public void Init(EnemyBase db, Vector2 _Vec, int _Color)
    {
        // Initialize Status
        type = (int)db.Type;
        color = _Color;
        vec = _Vec;
        speed = db.Speed;
        score = db.Score;
        power = db.Power;

        // Change Img
        SpriteRenderer img = GetComponent<SpriteRenderer>();
        img.sprite = Img[color];

        // Decision Vector
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = vec * speed;
    }
}