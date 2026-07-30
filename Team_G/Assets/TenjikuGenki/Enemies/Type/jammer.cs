using System.Collections.Generic;
using UnityEngine;

public class EJammer : Enemy
{
    public GameObject window;

    void Awake()
    {
        ;
    }

    protected override void Start()
    {
        base.Start();
        EnemySpawn.Instance.counter++;
    }

    protected override void Update()
    {
        ;
    }

    void FixedUpdate()
    {
        ;
    }

    /// <summary>
    /// 初期化
    /// </summary>
    /// <param name="db"></param>
    /// <param name="_vec"></param>
    /// <param name="_color"></param>
    /// <param name="_speed"></param>
    public void Init(EnemyData db, Vector2 vec, float speed)
    {
        // ステータスの初期化
        type = (int)db.type;
        _vec = vec;
        _speed = speed;
        score = db.score;
        power = db.power;
        _damage = 0;

        // ベクトルの補正
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = _vec * _speed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy other = collision.gameObject.GetComponent<Enemy>();
        if (other != null)
        {
            ScoreManager.Instance.OnEnemiesCollided(this, other);
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (IsHitEnemy(collision.gameObject)) Delete(collision);
    }

    void OnDestroy()
    {
        EnemySpawn.Instance.counter--;
    }

    public override void Damage()
    {
        Instantiate(window).GetComponent<Window>();
        Destroy(gameObject);
        base.Damage();
    }
}