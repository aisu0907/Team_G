//‰æ–Ê‰º•”(–{‘Ì)‚Ì“–‚½‚è”»’è

using UnityEngine;

public class Kill_Death : MonoBehaviour
{
    public GameObject player;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy")) Player.Instance.Damage(1, gameObject);
        if (collision.TryGetComponent<EJammer>(out var jammer)) jammer.PopWindow(); 

        if (collision.gameObject.tag == "Item")
        {
            Destroy(collision.gameObject);
        }

    }
}
