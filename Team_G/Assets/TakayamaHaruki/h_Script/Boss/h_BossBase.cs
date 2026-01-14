using UnityEngine;

public class BossBase : MonoBehaviour
{
    //ボスステータス
    [Header("▼Base Status")]
    public int health;
    public float speed;
    [Header("▼Death Animation")]
    public float death_pos_x;
    public float death_pos_y;

    //ゲームオブジェクト
    public GameObject explode; //爆発演出

    //ダメージ判定関数
    public void boss_damage(Collider2D collision)
    {
        if (collision.TryGetComponent<Enemy>(out var enemy))
            if (enemy.on_hitting)
            {
                gameObject.GetComponent<Boss_Damage_Effect>().damage_hit = true;
                Destroy(collision.gameObject);　//触れたウイルスを削除
                gameObject.GetComponent<BossBase>().health--; //ボスのHPを減らす
                Instantiate(explode, transform.position, Quaternion.identity); //ダメージ演出表示
                if(gameObject.GetComponent<BossBase>().health <= 0)
                {
                    gameObject.GetComponent<BossBase>().speed = 0;
                    gameObject.transform.position = new Vector2(death_pos_x, death_pos_y);
                }
            }
    }
}
