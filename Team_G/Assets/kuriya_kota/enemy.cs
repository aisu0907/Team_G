using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    public Vector2 moveDirection = new Vector2(1, 1); // 初期移動方向
    public float moveSpeed = 5f;
    float enemy_position=0.0f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.linearVelocity = moveDirection.normalized * moveSpeed;
    }

    private void Update()
    {
        GetComponent<Position>();

        

    }



    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            // 法線ベクトルを使って反射する
            Vector2 normal = collision.contacts[0].normal;
            moveDirection = Vector2.Reflect(moveDirection, normal);

            // 速度を更新
            rb.linearVelocity = moveDirection.normalized * moveSpeed*2.0f;
        }
    }
}
