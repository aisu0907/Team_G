//‰¡‚Ì•Ç

using UnityEngine;

public class Side_Wall : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            //”½Ëˆ—
            Vector2 tmp = new Vector2(-collision.gameObject.GetComponent<g_enemy>().vec.x, collision.gameObject.GetComponent<g_enemy>().vec.y);
            collision.gameObject.GetComponent<g_enemy>().vec = tmp;
        }
    }
}
