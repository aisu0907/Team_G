using System.Collections.Generic;
using UnityEngine;

public class EJammer : Enemy
{
    public GameObject window;

    protected override void Start()
    {
        base.Start();
        EnemySpawn.Instance.counter++;
    }
    
    /// <summary> 初期化変数 </summary>
    /// <param name="db"> 個体データ </param>
    /// <param name="vec"> 進行方向 </param>
    /// <param name="speed"> 速度 </param>
    public void Init(EnemyData db, Vector2 vec, float speed)
    {
        // ステータスの初期化
        type = (int)db.type;
        _vec = vec;
        _states.Add(new MoveSpeed(speed));
        score = db.score;
        power = db.power;
        _damage = 0;

        // ベクトルの補正
        _rb = GetComponent<Rigidbody2D>();
        _rb.linearVelocity = _vec * _states[(int)StateName.Speed].CurrentState;
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

    public override void Hit()
    {
        Instantiate(window).GetComponent<Window>();
        Destroy(gameObject);
    }
}