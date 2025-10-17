using UnityEngine;

public class k_Wall : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<k_test>().x_speed *= -1.0f;
        }
    
    }
}
