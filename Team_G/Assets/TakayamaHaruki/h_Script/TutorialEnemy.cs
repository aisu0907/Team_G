using System.Collections.Generic;
using UnityEngine;

public class TutorialEnemy : Enemy, IReflectable
{
    public GameObject tm;
    public List<Sprite> enemy_img;
    public bool Hitting => _onHitting;
    public COLOR Color => _color;
    protected COLOR _color { get; set; } = COLOR.RED;
    protected bool _onHitting { get; set; } = false;

    // Update is called once per frame
    protected override void Update()
    {
        // Spin
        if (_onHitting)
        {
            if (_vec.y < 0)
            {
                _vec.y = -_vec.y;
            }
            transform.Rotate(0, 0, EnemyConst.ROTATION_ANGLE);
        }
    }

    void FixedUpdate()
    {
        // Fix Vector
        rb.linearVelocity = _vec;
        if (rb.linearVelocity.magnitude != _speed)
            rb.linearVelocity = _vec.normalized * _speed;
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (IsHitEnemy(collision.gameObject)) Delete(collision);
    }

    /// <summary>
    /// èâä˙âª
    /// </summary>
    /// <param name="_vec"></param>
    /// <param name="_color"></param>
    /// <param name="_speed"></param>
    public void Init(Vector2 vec, COLOR color, float speed)
    {
        // Initialize Status
        _color = _color;
        _vec = vec;
        _speed = speed;

        // Change Img
        SpriteRenderer img = GetComponent<SpriteRenderer>();
        img.sprite = enemy_img[(int)color];

        // Decision Vector
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = _vec * _speed;
    }

    public void OnDestroy()
    {
        if (_onHitting)
            TutorialManager.Instance.enemy_hit_count++;
    }
}
