using System.Collections.Generic;
using UnityEngine;

public class EReflect : Enemy, IDamageable
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
        // Spin
        if (on_hitting)
        {
            transform.Rotate(0, 0, EnemyConst.ROTATION_ANGLE);
            timer++;
            if (timer >= EnemyConst.TIME_SPENT_IN_RETURN)
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
        if (on_hitting) { if (rb.linearVelocity.magnitude != speed * EnemyConst.DOUBLE) rb.linearVelocity = vec.normalized * speed * EnemyConst.DOUBLE; }
        else if (rb.linearVelocity.magnitude != speed) rb.linearVelocity = vec.normalized * speed;
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