using UnityEngine;

public class k_kill_death : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        { 
            Destroy(collision.gameObject);
        }

    }
}
