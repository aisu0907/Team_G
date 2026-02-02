//TopWall

using UnityEngine;

public class TopWall : MonoBehaviour, IPhazeManager
{
    public GameObject manger;
    public int phase { get; set; } = 0;
    public bool is_change_color { get; set; } = false;
    private IPhazeManager ipm;

    private void Start()
    {
       ipm = manger.GetComponent<IPhazeManager>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(ipm.phase >= 6 && collision.gameObject.CompareTag("Enemy"))
        {
            manger.GetComponent<TutorialManager>().enemy_hit_count++;
            //エネミーを削除
            Destroy(collision.gameObject);
            return;
        }
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
