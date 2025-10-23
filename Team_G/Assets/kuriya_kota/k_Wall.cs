using UnityEngine;

public class k_Wall : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Vector2 tmp = new Vector2(-collision.gameObject.GetComponent<g_enemy>().vec.x, collision.gameObject.GetComponent<g_enemy>().vec.y);
            collision.gameObject.GetComponent<g_enemy>().vec = tmp;
        }
    }
}
