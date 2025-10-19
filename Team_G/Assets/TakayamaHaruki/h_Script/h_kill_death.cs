using UnityEngine;

public class kill_death : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Green" || collision.gameObject.tag == "Red")
        { 
            Destroy(collision.gameObject);
            //Debug.Log(collision.gameObject.GetComponent<k_test>().x_speed);
        }

    }
}
