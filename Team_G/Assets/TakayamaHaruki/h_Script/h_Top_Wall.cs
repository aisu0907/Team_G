//画面上部の当たり判定

using UnityEngine;

public class TopWall : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        //壁に触れたらエネミーを削除
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy other = collision.gameObject.GetComponent<Enemy>();
            if (other != null)
            {
                Score_Manager.Instance.Enemy_Score(other);
            }
            Destroy(collision.gameObject);
        }
    }
}
