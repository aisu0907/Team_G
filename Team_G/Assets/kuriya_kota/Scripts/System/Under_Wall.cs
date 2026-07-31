//‰æ–Ê‰º•”(–{‘Ì)‚Ì“–‚½‚è”»’è

using UnityEngine;

public class UnderWall : MonoBehaviour
{
    public Player player;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IHitable>(out var e))
        {
            e.Hit();
            player.Damage(e.Damage);
        }

        if (collision.gameObject.tag == "Item")
        {
            Destroy(collision.gameObject);
        }

    }
}
