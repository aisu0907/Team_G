using System.Collections.Generic;
using UnityEngine;

public class ENormal : Enemy, IDamageable, IReflectable
{
    public int timer { get; set; } = 0;
    public List<Sprite> Img;
    IReflectable iref;
    void Awake()
    {
        ;
    }

    void Start()
    {
        iref = GetComponent<IReflectable>();
        EnemySpawn.Instance.counter++;
    }

    void Update()
    {
        // Spin
        if (on_hitting)
        {
            if (vec.y < 0)
            {
                vec.y = -vec.y;
            }
            transform.Rotate(0, 0, EnemyConst.ROTATION_ANGLE);
            if (EnemySpawn.Instance.spawn_switch == false)
                iref.SpinLimit(this);
        }
    }

    void FixedUpdate()
    {
        // Fix Vector
        rb.linearVelocity = vec;
        if (rb.linearVelocity.magnitude != speed)
            if(on_hitting)
                rb.linearVelocity = vec.normalized * (speed + Shield_Item.Instance.reflect_speed);
            else
                rb.linearVelocity = vec.normalized * speed;
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (IsHitEnemy(collision.gameObject)) Delete(collision);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy other = collision.gameObject.GetComponent<Enemy>();
        if (other != null)
        {
            ScoreManager.Instance.OnEnemiesCollided(this, other);
        }
    }

    void OnDestroy()
    {
        EnemySpawn.Instance.counter--;
    }

    /// <summary>
    /// èâä˙âª
    /// </summary>
    /// <param name="db"></param>
    /// <param name="_vec"></param>
    /// <param name="_color"></param>
    /// <param name="_speed"></param>
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
}