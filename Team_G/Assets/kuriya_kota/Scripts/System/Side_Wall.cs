using UnityEngine;

public class SideWall : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        // 反射できるオブジェクトなら、
        if (collision.TryGetComponent<IReflectable>(out var enemy))
        {
            // 反射する
            Vector2 vec = new Vector2(-collision.gameObject.GetComponent<Enemy>().Velocity.x,
                collision.gameObject.GetComponent<Enemy>().Velocity.y);
            enemy.Reflect(vec, enemy.Hitting);
        }
    }
}
