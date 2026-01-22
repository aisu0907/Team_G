using UnityEngine;

public class BossBase : MonoBehaviour
{
    //ボスステータス
    [Header("▼Base Status")]
    public int health; //体力
    public float speed;//移動速度
    [Header("▼Death Animation")]
    public float death_pos_x;//死亡後に移動する座標x
    public float death_pos_y;//死亡後に移動する座標y

    //ゲームオブジェクト
    public GameObject explode; //爆発演出

    //ダメージ判定関数
    public void boss_damage(Collider2D collision)
    {
        //触れた相手にEnemyクラスがついていたら
        if (collision.TryGetComponent<Enemy>(out var enemy))

            //触れたウイルスが打ち返されたものならば
            if (enemy.on_hitting)
            {
                Destroy(collision.gameObject);　//触れたウイルスを削除
                gameObject.GetComponent<BossBase>().health--; //ボスのHPを減らす
                gameObject.GetComponent<Boss_Damage_Effect>().damage_hit = true; //ダメージを受ける
                Instantiate(explode, transform.position, Quaternion.identity);   //ダメージ演出表示
                
                //ボスの体力が0以下なら
                if(gameObject.GetComponent<BossBase>().health <= 0)
                {
                    gameObject.GetComponent<BossBase>().speed = 0; //移動速度を0にする
                    gameObject.transform.position = new Vector2(death_pos_x, death_pos_y); //死亡位置に移動させる
                }
            }
    }
}
