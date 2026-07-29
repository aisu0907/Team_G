using UnityEngine;

public class ObjBase : MonoBehaviour
{
    // ï®óù
    protected Rigidbody2D rb;
    public Vector2 vec;
    public float speed = 1.0f;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {
        
    }
}
