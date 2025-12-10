using UnityEngine;

public class BossBase : MonoBehaviour
{
    //ボスステータス
    public int health;
    public float speed;
    //ゲームオブジェクト
    public GameObject explode;      //爆発演出
    public GameObject boss_explode; //死亡演出

    //ダメージ判定関数
    public void boss_damage(GameObject boss, Collider2D collision)
    {
        if (collision.TryGetComponent<Enemy>(out var enemy))
            if (enemy.on_hitting)
            {
                boss.GetComponent<Boss_Damage_Effect>().damage_hit = true;
                Destroy(collision.gameObject);　//触れたウイルスを削除
                boss.GetComponent<BossBase>().health--; //ボスのHPを減らす
                Instantiate(explode, transform.position, Quaternion.identity); //ダメージ演出表示
            }
    }
}
