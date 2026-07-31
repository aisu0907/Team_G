using System.Collections.Generic;
using UnityEngine;

public class ENormal : Enemy, IReflectable
{
    // ----- プロパティ ----- //
    public bool Hitting => _onHitting;
    protected COLOR _color { get; set; } = COLOR.RED;

    // ----- メンバ変数 ----- //
    public List<Sprite> Img;
    public COLOR Color => _color;
    protected bool _onHitting { get; set; } = false;
    IReflectable iref;

    void Awake()
    {
        ;
    }

    protected override void Start()
    {
        base.Start();

        iref = GetComponent<IReflectable>();
        EnemySpawn.Instance.counter++;
    }

    protected override void Update()
    {
        base.Update();

        // ヒット中なら回転
        if (_onHitting)
            transform.Rotate(0.0f, 0.0f, EnemyConst.ROTATION_ANGLE);
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

    /// <summary> 初期化 </summary>
    public void Init(EnemyData db, Vector2 vec, COLOR color, float speed)
    {
        // ステータスの初期化
        type = (int)db.type;
        _color = (COLOR)color;
        _vec = vec;
        _speed = speed;
        score = db.score;
        power = db.power;
        _damage = 1;

        // 色と画像を合わせる
        SpriteRenderer img = GetComponent<SpriteRenderer>();
        img.sprite = Img[(int)color];

        // ベクトルの補正
        _rb = GetComponent<Rigidbody2D>();
        _rb.linearVelocity = vec.normalized * speed;
    }

    public void Reflect(Vector2 ref_vec, bool hitting)
    {
        _rb.linearVelocity = ref_vec.normalized * _speed;
        _onHitting = hitting;
    }
}