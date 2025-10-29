using UnityEngine;

public class Kill_Death : MonoBehaviour
{
    public GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        { 
            player.GetComponent<Player>().Health--;
            Destroy(collision.gameObject);
            //GameObject.FindWithTag("Player").GetComponent<Player>().Health--;
        }

        if (collision.gameObject.tag == "Item")
        {
            Destroy(collision.gameObject);
        }

    }
}
