using System.Collections.Generic;
using UnityEngine;

public class ENormal : Enemy, IDamageable
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
        if (on_hitting)
            transform.Rotate(0, 0, 20);
    }

    void FixedUpdate()
    {
        // Fix Vector
        rb.linearVelocity = vec;
        if (rb.linearVelocity.magnitude != speed) rb.linearVelocity = vec.normalized * speed;
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

    void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy other = collision.gameObject.GetComponent<Enemy>();
        if (other != null)
        {
            Score_Manager.Instance.OnEnemiesCollided(this, other);
            Debug.Log("‚ ‚ ‚ ‚ ‚ ");
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (IsHitEnemy(collision.gameObject)) Delete(collision);
    }
}