using UnityEngine;

public class g_Wall : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Vector2 tmp = new Vector2(-collision.gameObject.GetComponent<g_enemy>().v.x, collision.gameObject.GetComponent<g_enemy>().v.y);
            collision.gameObject.GetComponent<g_enemy>().v = tmp;
        }
    }
}
