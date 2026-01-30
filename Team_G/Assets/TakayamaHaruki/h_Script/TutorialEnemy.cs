using System.Collections.Generic;
using UnityEngine;

public class TutorialEnemy : Enemy, IDamageable
{
    [SerializeField] List<Sprite> enemy_img;

    // Update is called once per frame
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
        }
    }

    void FixedUpdate()
    {
        // Fix Vector
        rb.linearVelocity = vec;
        if (rb.linearVelocity.magnitude != speed)
            rb.linearVelocity = vec.normalized * speed;
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
    public void Init(Vector2 _vec, int _color, float _speed)
    {
        // Initialize Status
        color = _color;
        vec = _vec;
        speed = _speed;

        // Change Img
        SpriteRenderer img = GetComponent<SpriteRenderer>();
        img.sprite = enemy_img[color];

        // Decision Vector
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = vec * speed;
    }
}
