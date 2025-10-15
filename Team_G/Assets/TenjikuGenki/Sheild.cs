using UnityEngine;
using UnityEngine.EventSystems;

public class Sheild : MonoBehaviour
{
    public Sprite RSheild;
    public Sprite GSheild;
    SpriteRenderer tmp_s;

    public GameObject follow;
    Vector2 tmp_v;

    Vector2 moveDirection_s = new Vector2(1,1);
    public float moveSpeed_s = 5.0f;

    Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tmp_s = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // 追従処理
        tmp_v = follow.transform.position;
        tmp_v.y += 0.7f;
        transform.position = tmp_v;

        // 色変更処理
        if (Input.GetKey(KeyCode.Z))
        {
            tmp_s.sprite = RSheild;
        }
        if (Input.GetKey(KeyCode.X))
        {
            tmp_s.sprite = GSheild;
        }
    }

    //void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Green")
    //    {
    //        Debug.Log("Hit");
    //        Destroy(collision.gameObject);
    //    }
    //}
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Green"))
        {
            fall test = collision.gameObject.GetComponent<fall>();

            moveSpeed_s = test.speed;
            moveDirection_s = test.moveDirection;

            // 法線ベクトルを使って反射する
            Vector2 normal = collision.contacts[0].normal;
            moveDirection_s = Vector2.Reflect(moveDirection_s, normal);

            // 速度を更新
            collision.rigidbody.linearVelocity = moveDirection_s.normalized * -moveSpeed_s;
        }
    }
}
