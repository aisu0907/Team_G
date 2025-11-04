//画面上部の当たり判定

using UnityEngine;

public class Top_Wall : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        //壁に触れたらエネミーを削除
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.gameObject);
        }
    }
}
