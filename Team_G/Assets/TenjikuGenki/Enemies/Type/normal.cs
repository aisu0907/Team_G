using System.Collections.Generic;
using UnityEngine;

public class ENormal : Enemy, IDamageable, IReflectable
{
    public List<Sprite> Img;
    public float ref_speed { get; set; } = 0;

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
            transform.Rotate(0, 0, EnemyConst.ROTATION_ANGLE);
    }

    void FixedUpdate()
    {
        // Fix Vector
        rb.linearVelocity = vec;
        if (rb.linearVelocity.magnitude != speed)
            if(on_hitting)
                rb.linearVelocity = vec.normalized * speed;
            else
                rb.linearVelocity = vec.normalized * (speed + ref_speed);
    }

    public void Init(EnemyData db, Vector2 _vec, int _color, float _speed)
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

    void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy other = collision.gameObject.GetComponent<Enemy>();
        if (other != null)
        {
            Score_Manager.Instance.OnEnemiesCollided(this, other);
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (IsHitEnemy(collision.gameObject)) Delete(collision);
    }
}