//‰æ–Ê‰º•”(–{‘Ì)‚Ì“–‚½‚è”»’è

using UnityEngine;

public class UnderWall : MonoBehaviour
{
    public Player player;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Enemy>(out var e))
        {
            e.Damage();
            player.Damage(e.HitDamage, collision.gameObject);
        }

        if (collision.gameObject.tag == "Item")
        {
            Destroy(collision.gameObject);
        }

    }
}
