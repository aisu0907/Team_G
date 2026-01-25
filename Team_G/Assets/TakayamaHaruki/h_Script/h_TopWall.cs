//TopWall

using UnityEngine;

public class TopWall : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        //壁に触れたらエネミーを削除
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //触れたエネミーの情報を保存
            Enemy other = collision.gameObject.GetComponent<Enemy>();
            
            if (other != null)
            {
                //壁に触れた敵のスコアを追加
                ScoreManager.Instance.Enemy_Score(other);
            }
            //エネミーを削除
            Destroy(collision.gameObject);
        }
    }
}
