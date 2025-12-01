using UnityEngine;

public class h_Boss_Attack : MonoBehaviour
{
    public int damage; //ダメージ
    public int damage_interval; //ダメージ間隔
    public int Display_end;  //表示終了

    private int Display_time; //表示時間
    private int damage_time; //ダメージタイム
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //ダメージ間隔をリセット
        damage_time = damage_interval;
    }

    // Update is called once per frame
    void Update()
    {
        //タイムカウント
        damage_time++;
        Display_time++;

        //表示時間が終了した場合
        if (Display_time >= Display_end)
            Destroy(gameObject); //範囲攻撃を削除
        
        if(h_Boss.Instance.health <= 0)
        {
            Destroy(gameObject); //範囲攻撃を削除
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //プレイヤーに触れた場合
        if(collision.CompareTag("Player") && damage_interval <= damage_time)
        {
            damage_time = 0; //タイムリセット
            Player.Instance.Damage(damage, gameObject, false); //プレイヤーにダメージ
        }
    }
}
