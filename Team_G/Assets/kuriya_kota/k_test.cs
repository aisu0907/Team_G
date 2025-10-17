using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class k_test : MonoBehaviour
{
    Rigidbody2D rb;

    public float x_speed = 3.0f;
    public float y_speed=10.0f;//èâä˙ë¨ìx
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        rb.linearVelocity = new Vector2(x_speed, -y_speed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ReflectY()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * -1);
    }

    public void ReflectX()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x * -1, rb.linearVelocity.y);
    }

}
