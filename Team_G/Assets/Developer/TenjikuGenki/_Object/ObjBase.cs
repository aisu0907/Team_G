using System.Collections.Generic;
using UnityEngine;

public class ObjBase : MonoBehaviour
{
    // ----- プロパティ ----- //
    [Header("物理")]
    public float Speed => _states[(int)StateName.Speed].CurrentState;
    public Vector2 Velocity => _rb.linearVelocity;

    // ----- メンバ変数 ----- //
    [Header("物理")]
    protected Rigidbody2D _rb;
    protected Vector2 _vec;
    public List<State> _states = new();

    protected virtual void Start()
    {
        // Rigidbodyの取得
        if (_rb == null)
            _rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {
        
    }
}
