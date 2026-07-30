using UnityEngine;
using UnityEngine.UI;

public class Enemy : ObjBase
{
    public int HitDamage => _damage;
    [Header("▼ BaseStatus")]
    public int type;
    public int score;
    protected int power;
    public GameObject explode;
    protected int _damage;
    
    // Update is called once per frame
    protected override void Update()
    {
        ;
    }

    // 死亡
    public void Delete(Collider2D obj = null)
    {
        //オブジェクトを生成
        Instantiate(explode, transform.position, Quaternion.identity);
        if (obj != null) Destroy(obj.gameObject);
        Destroy(gameObject);
        if (Player.Instance.bom < Player.Instance.max_bom)
            BombGage.Instance.bomb_gage.value += power;
    }

    // ヒットチェック
    public virtual bool IsHitEnemy(GameObject obj)
    {
        if (obj.TryGetComponent<IReflectable>(out var enemy)) return obj.GetComponent<IReflectable>().Hitting;
        return false;
    }

    // 被弾
    public virtual void Damage()
    {
        Destroy(gameObject);
    }

    public void VectorReshape(Vector2 vec)
    {
        rb.linearVelocity = vec;
    }
}
