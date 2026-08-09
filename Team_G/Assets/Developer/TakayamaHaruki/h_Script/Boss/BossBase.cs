//BossBase.cs

using UnityEngine;

public class BossBase : ObjBase
{
    //ボスステータス
    [Header("▼Base Status")]
    public int health; //体力
    //ボスの死亡位置
    [Header("▼Death Animation")]
    public float death_pos_x;//死亡後に移動する座標x
    public float death_pos_y;//死亡後に移動する座標y

    //ゲームオブジェクト
    public GameObject explode; //爆発演出

    //ダメージ判定関数
    public void boss_damage(Collider2D collision)
    {
        //触れた相手にEnemyクラスがついていたら
        if (collision.TryGetComponent<IReflectable>(out var enemy))

            //触れたウイルスが打ち返されたものならば
            if (enemy.Hitting)
            {
                Destroy(collision.gameObject);　//触れたウイルスを削除
                health--; //ボスのHPを減らす
                GetComponent<BossDamageEffect>().damage_hit = true; //ダメージを受ける
                Instantiate(explode, transform.position, Quaternion.identity);   //ダメージ演出表示
                
                //ボスの体力が0以下なら
                if(gameObject.GetComponent<BossBase>().health <= 0)
                {
                    _states[(int)StateName.Speed].Mode(false); //移動速度を0にする
                    transform.position = new Vector2(death_pos_x, death_pos_y); //死亡位置に移動させる
                }
            }
    }
}
