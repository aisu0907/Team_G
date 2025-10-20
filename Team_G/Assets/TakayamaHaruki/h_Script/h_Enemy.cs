using UnityEngine;

public class h_enemy : MonoBehaviour
{
    Rigidbody2D rbody;
    public float speed = 1f;
    public int EnemyColor = 1;
    public int EnemyType = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rbody = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rbody.linearVelocity = new Vector2(0, -speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }
}
