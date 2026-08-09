using System.Collections.Generic;
using UnityEngine;

public class EReflect : Enemy, IReflectable
{
    [SerializeField] SpriteRenderer _spriteRenderer;
    [SerializeField] List<Sprite> _imgNormal;
    [SerializeField] List<Sprite> _imgDamaged;
    public int _timer { get; set; } = 0;
    public bool Hitting => _onHitting;
    public COLOR Color => _color;
    protected COLOR _color { get; set; } = COLOR.RED;
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
        if (!_onHitting) return;

        transform.Rotate(0, 0, EnemyConst.ROTATION_ANGLE);
        if (++_timer >= EnemyConst.TIME_SPENT_IN_RETURN)
        {
            // 物理関係をリセット
            _rb.linearVelocity = _vec.normalized * _states[(int)StateName.Speed].CurrentState;
            transform.localRotation = default;
            _onHitting = false;

            // タイマーを初期化
            _timer = 0;
            _spriteRenderer.sprite = _imgNormal[(int)_color];
        }
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

    /// <summary>
    /// 初期化
    /// </summary>
    /// <param name="db"></param>
    /// <param name="_vec"></param>
    /// <param name="_color"></param>
    /// <param name="_speed"></param>
    public void Init(EnemyData db, Vector2 vec, COLOR color, float speed)
    {
        // ステータスの初期化
        type = (int)db.type;
        _color = color;
        _vec = vec;
        _states.Add(new MoveSpeed(speed));
        score = db.score;
        power = db.power;
        _damage = 1;

        // 色と画像を合わせる
        SpriteRenderer img = GetComponent<SpriteRenderer>();
        img.sprite = _imgNormal[(int)_color];

        // ベクトルの補正
        _rb = GetComponent<Rigidbody2D>();
        _rb.linearVelocity = vec.normalized * speed;
    }

    public void Reflect(Vector2 ref_vec, bool hitting)
    {
        _rb.linearVelocity = ref_vec.normalized * _states[(int)StateName.Speed].CurrentState;
        _onHitting = hitting;
        _spriteRenderer.sprite = _imgDamaged[(int)_color];
    }
}