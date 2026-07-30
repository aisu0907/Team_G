using UnityEngine;

public class ObjBase : MonoBehaviour
{
    // ----- プロパティ ----- //
    [Header("物理")]
    public float Speed => _speed;
    public Vector2 Velocity => rb.linearVelocity;

    // ----- メンバ変数 ----- //
    [Header("物理")]
    protected Rigidbody2D rb;
    protected Vector2 _vec;
    protected float _speed = 1.0f;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {
        
    }
}
