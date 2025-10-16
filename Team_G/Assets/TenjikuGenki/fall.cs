using UnityEngine;

public class fall : MonoBehaviour
{
    Rigidbody2D rbody;
    public float speed = 1f;
    public Vector2 moveDirection = new Vector2(1, 1);


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rbody = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rbody.linearVelocity = new Vector2(rbody.linearVelocity.x, -speed);
    }
}
