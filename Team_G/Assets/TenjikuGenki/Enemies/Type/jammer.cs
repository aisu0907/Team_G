using System.Collections.Generic;
using UnityEngine;

public class EJammer : Enemy
{
    Rigidbody2D rb;
    public GameObject window;

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

    public void Init(EnemyData db, Vector2 _vec, float _speed)
    {
        // Initialize Status
        type = (int)db.type;
        vec = _vec;
        speed = _speed;
        score = db.score;
        power = db.power;

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

    public void PopWindow()
    {
        Instantiate(window).GetComponent<Window>();
        Destroy(gameObject);
    }
}