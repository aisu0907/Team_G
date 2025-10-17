using UnityEngine;

public class kill_death : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<k_test>().y_speed *= -1.0f;
            Destroy(collision.gameObject);
            //Debug.Log(collision.gameObject.GetComponent<k_test>().x_speed);
        }

    }
}
