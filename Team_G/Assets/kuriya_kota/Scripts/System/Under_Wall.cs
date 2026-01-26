//‰æ–Ê‰º•”(–{‘Ì)‚Ì“–‚½‚è”»’è

using UnityEngine;

public class UnderWall : MonoBehaviour
{
    public GameObject player;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<EJammer>(out var jammer)) jammer.PopWindow();
        else if (collision.CompareTag("Enemy")) Player.Instance.Damage(1, collision.gameObject);

        if (collision.gameObject.tag == "Item")
        {
            Destroy(collision.gameObject);
        }

    }
}
