using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rbody;
    float moveX = 0.0f;
    float moveY = 0.0f;

    public float offsetX = 0.5f;
    public float offsetY = 0.5f;

    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        moveX = Input.GetAxisRaw("Horizontal"); 
        moveY = Input.GetAxisRaw("Vertical");   
    }

    void FixedUpdate()
    {
        rbody.linearVelocity = new Vector2(moveX * 10.0f, moveY * 5.0f);//移動速度

        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));//画面外侵入防止
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));

        Vector2 pos = transform.position;

        pos.x = Mathf.Clamp(pos.x, min.x + offsetX, max.x - offsetX);
        pos.y = Mathf.Clamp(pos.y, min.y + offsetY, max.y - offsetY);

        transform.position = pos;
    }

   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Rigidbody2D enemyRb = collision.gameObject.GetComponent<Rigidbody2D>();

            if (enemyRb != null)
            {
                float hitPoint = collision.transform.position.x - transform.position.x;

                float playerWidth = GetComponent<Collider2D>().bounds.size.x;

                
                float offset = hitPoint / (playerWidth / 2);

                Vector2 newDir = new Vector2(offset, 1).normalized;

                float speedUpFactor = 2.0f; 
                float speed = enemyRb.linearVelocity.magnitude*speedUpFactor;
                enemyRb.linearVelocity = newDir * speed;
            }
        }
    }
}
