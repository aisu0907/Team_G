using Unity.VisualScripting;
using UnityEngine;

public class Sheild : MonoBehaviour
{
    public Sprite RED;
    public Sprite GREEN;
    SpriteRenderer img;
    public int SheildColor = 1;

    public GameObject follow;
    Vector2 vec;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        img = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // �Ǐ]����
        vec = follow.transform.position;
        vec.y += 0.7f;
        transform.position = vec;

        // �F�ύX����
        if (Input.GetKey(KeyCode.Z))
        {
            img.sprite = RED;
            SheildColor = 1;
        }
        if (Input.GetKey(KeyCode.X))
        {
            img.sprite = GREEN;
            SheildColor = 2;
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (collision.gameObject.GetComponent<g_enemy>().EnemyColor == SheildColor) {
                if (collision.gameObject.GetComponent<g_enemy>().EnemyType != 2)
                {
                    Vector2 d = collision.gameObject.transform.position - transform.position;
                    collision.gameObject.GetComponent<g_enemy>().v = d;
                }
            }
            else
            {
                // ��e����
                GameObject.Find("Player").GetComponent<Player>().Health--;
                Destroy(collision.gameObject);
                Debug.Log(GameObject.Find("Player").GetComponent<Player>().Health);
            }
        }
    }
}
