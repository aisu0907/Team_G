using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class g_BossPhase1 : MonoBehaviour, IBossState
{
    float Timer;

    public void Enter(g_boss boss)
    {
        Timer = 0;
    }

    public void Main(g_boss boss)
    {
        Timer += Time.deltaTime;

        // 一定時間ごとに弾を発射
        if (Timer >= 1.0f)
        {
            ShootBullet(boss);
            Timer = 0f;
        }

        if (boss.health == 0) GameManager.Instance.KillBoss(gameObject);
    }

    public void Exit(g_boss boss)
    {
        Debug.Log("Phase1終了：次のフェーズへ移行");
    }

    // 弾を発射する処理
    void ShootBullet(g_boss boss)
    {
        Debug.Log("Phase1弾発射！");
        // 実際にはここで弾PrefabをInstantiateしたり、ObjectPoolを使ったりします
        int color = Random.Range(0, boss.list.Count);
        boss.img.sprite = boss.sprites[color];
        Vector2 d = (Player.Instance.transform.position - boss.transform.position).normalized;
        var e = Instantiate(boss.list[0].pf, boss.transform.position, Quaternion.identity).GetComponent<ENormal>(); e.Init(boss.list[0].db, d, color, 5);
        boss.audioSource.PlayOneShot(boss.sound1);
    }
}