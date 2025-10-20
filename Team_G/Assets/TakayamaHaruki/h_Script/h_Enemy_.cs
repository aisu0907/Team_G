using UnityEngine;

public class Enemy_ : MonoBehaviour
{
    Rigidbody2D enemy_rbody;
    public float enemy_speed = 1f;
    public int enemy_number = 0;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemy_rbody = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        enemy_rbody.linearVelocity = new Vector2(0, -enemy_speed);
    }
}
