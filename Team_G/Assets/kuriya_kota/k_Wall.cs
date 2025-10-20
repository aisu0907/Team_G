using UnityEngine;

public class k_Wall : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<g_enemy>().speed *= -1.0f;
            Debug.Log(collision.gameObject.GetComponent<g_enemy>().speed);
        }
    }
}
