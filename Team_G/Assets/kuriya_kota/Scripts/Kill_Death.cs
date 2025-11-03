//‰æ–Ê‰º•”(–{‘Ì)‚Ì“–‚½‚è”»’è

using UnityEngine;

public class Kill_Death : MonoBehaviour
{
    public GameObject player;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        { 
            player.GetComponent<Player>().health--;
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "Item")
        {
            Destroy(collision.gameObject);
        }

    }
}
