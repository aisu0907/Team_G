using UnityEngine;

public class h_enemy : MonoBehaviour
{
    Rigidbody2D rbody;
    public float speed = 1f;
    public int EnemyColor = 1;
    public int EnemyType = 1;
    public Vector2 v;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rbody = this.GetComponent<Rigidbody2D>();
        v = new Vector2(0, -speed);
    }

    // Update is called once per frame
    void Update()
    {
        rbody.linearVelocity = v;
    }
    void FixedUpdate()
    {
        if (rbody.linearVelocity.magnitude != speed)
        {
            rbody.linearVelocity = rbody.linearVelocity.normalized * speed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }
}
