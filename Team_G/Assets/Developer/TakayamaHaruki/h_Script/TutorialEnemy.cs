using System.Collections.Generic;
using UnityEngine;

public class TutorialEnemy : Enemy, IReflectable
{
    public GameObject tm;
    // ----- プロパティ ----- //
    public bool Hitting => _onHitting;
    protected COLOR _color { get; set; } = COLOR.RED;

    // ----- メンバ変数 ----- //
    [SerializeField] SpriteRenderer _spriteRenderer;
    [SerializeField] List<Sprite> _imgNormal;
    [SerializeField] List<Sprite> _imgDamaged;
    public COLOR Color => _color;
    protected bool _onHitting { get; set; } = false;

    // Update is called once per frame
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

    /// <summary>
    /// 初期化
    /// </summary>
    /// <param name="_vec"></param>
    /// <param name="_color"></param>
    /// <param name="_speed"></param>
    public void Init(Vector2 vec, COLOR color, float speed)
    {
        // Initialize Status
        _color = color;
        _vec = vec;
        _speed = speed;

        // Change Img
        SpriteRenderer img = GetComponent<SpriteRenderer>();
        img.sprite = _imgNormal[(int)color];

        // Decision Vector
        _rb = GetComponent<Rigidbody2D>();
        _rb.linearVelocity = vec.normalized * speed;
    }

    public void OnDestroy()
    {
        if (_onHitting)
            TutorialManager.Instance.enemy_hit_count++;
    }

    public void Reflect(Vector2 ref_vec, bool hitting)
    {
        _rb.linearVelocity = ref_vec.normalized * _speed;
        _onHitting = hitting;
        _spriteRenderer.sprite = _imgDamaged[(int)_color];
    }
}
