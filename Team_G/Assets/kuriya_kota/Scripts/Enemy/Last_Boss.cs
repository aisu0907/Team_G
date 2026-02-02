//using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class LastBoss : BossBase 
{
    Rigidbody2D rb;
    SpriteRenderer img;

    public int timer;
    public int jamamer_timer;
    public int attack=0;
    int mode = 0;

    bool move = true;
    bool isDying = false;


    bool spiralOnce = true;
    bool summonOnce = true;
    bool targetOnce=true;
    bool beamOnce = true;

    Vector2 basePos;

    public GameObject prefab;
    public EnemyData enemy;

    public GameObject prefab2;
    public EnemyData enemy2;
    public GameObject prefab3;
    public GameObject r_falsh;

    AudioSource audioSource;

    private void Awake()
    {
        Instantiate(r_falsh, transform.position, Quaternion.identity);
        Instance = this;
    }

    public static LastBoss Instance { get; private set; }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        img = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();

        basePos = transform.position;
     
    }

    void Update()
    {
        jamamer_timer++;

        if (move) Move();
        else Death_Move();

        if (jamamer_timer >= attack*4&&!isDying)
        {
            jamamer_timer = 0;
            SummonJammer();
        }

        if (move && health <= 5)
        {
            img.color = Color.Lerp(
                Color.white,
                Color.red,
                Mathf.Sin(Time.time * 2f) * 0.5f + 0.5f);
        }

        switch (mode)
        {
            case 0:
                if(!isDying&&move) timer++;
                if (timer >= attack)
                {
                    mode = Random.Range(1, 5);
                }
                break;

            case 1:
                if (health <= 5)
                    StartCoroutine(RapidBeam());
                else
                    Beam();
                break;

            case 2:
                StartCoroutine(ContinuousShot());
                if (health <= 5) Beam();
                break;

            case 3:
                SpiralShot();
                if (health <= 5) Beam();
                break;

            case 4:
                SpiralShot();
                if (health <= 5) Beam();
                break;
        }

        if (health <= 0 && !isDying)
        {
            Die();
        }
    }
    /// <summary>
    /// ボスの体力を減らす
    /// </summary>
    /// <param name="collision"></param>
    void OnTriggerEnter2D(Collider2D collision) {
        if (health > 0)
            boss_damage(collision);
    }
    /// <summary>
    /// ボスを左右に一定幅動かす
    /// </summary>
    private void Move()
    {
            float x = Mathf.Sin(Time.time) * speed - 2f;
            transform.position = new Vector2(x, transform.position.y);
    }

    /// <summary>
    /// ボスをY0に固定してX方向に震わせる
    /// </summary>
    private void Death_Move()
    {
        float speed = 40f;
        float amount = 0.1f;

        float offsetX = Mathf.Sin(Time.time * speed) * amount;

        transform.position = basePos + new Vector2(offsetX, 0);
    }
    /// <summary>
    /// 下方向にウイルスを3体拡散して飛ばす
    /// </summary>
    private void SpiralShot()
    {
        if (!spiralOnce) return;
        spiralOnce = false;

        timer = 0;

        for (int i = 0; i < 3; i++)
        {

            float angle = (-90f + (i - 1) * 70f) * Mathf.Deg2Rad;
            Vector2 d = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

            var e = Instantiate(prefab, transform.position, Quaternion.identity)
                    .GetComponent<ENormal>();
            e.Init(enemy, d, Random.Range(0, 2), 3.5f);
        }
        AudioManager.instance.PlaySound("Shoot");

        spiralOnce = true;
        mode = 0;
    }
    /// <summary>
    /// ビームのオブジェクトをプレイヤーの位置に呼び出す
    /// </summary>
    private void Beam()
    {
        if (!beamOnce) return;
        beamOnce = false;

        timer = 0;

        Instantiate(prefab3, Player.Instance.transform.position, Quaternion.identity);

        StartCoroutine(ResetBeam());
    }

    /// <summary>
    ///  beamOnceを管理するためのコルーチン
    /// </summary>
    /// <returns></returns>
    IEnumerator ResetBeam()
    {
        yield return new WaitForSeconds(2f);
        beamOnce = true;
        timer = 0;
        mode = 0;
    }
    /// <summary>
    /// ジャマーウイルスを生成
    /// </summary>
    private void SummonJammer()
    {
        if (!summonOnce) return;
        summonOnce = false;

        var e = Instantiate(prefab2, transform.position, Quaternion.identity)
                .GetComponent<EJammer>();
        e.Init(enemy2, new Vector2(0, -1.5f), enemy2.speed);

        summonOnce = true;
    }
    /// <summary>
    /// ビームを連続で3本生成する
    /// </summary>
    /// <returns></returns>
    private IEnumerator RapidBeam()
    {
        if (!beamOnce) yield break;
        beamOnce = false;

        for (int i = 0; i < 2; i++)
        {
            Instantiate(prefab3, Player.Instance.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(2f);
        }

        beamOnce = true;
        mode = 0;
    }
    /// <summary>
    /// ウイルスをプレイヤーの方向に連続で発射する
    /// </summary>
    /// <returns></returns>
    private IEnumerator ContinuousShot()
    {
        if (!targetOnce) yield break;
        targetOnce = false;

        for (int i = 0; i < 5; i++)
        {

            Vector2 d = (Player.Instance.transform.position - transform.position).normalized;
            var e = Instantiate(prefab, transform.position, Quaternion.identity).GetComponent<ENormal>(); e.Init(enemy, d, Random.Range(0, 2), 3);
            AudioManager.instance.PlaySound("Shoot");
            yield return new WaitForSeconds(1.0f);
        }

        targetOnce = true;
        timer = 0;
        mode = 0;
    }

    /// <summary>
    /// 死亡時演出
    /// </summary>
    public void Die()
    {
        if (isDying&&timer==0) return;
        isDying = true;
        move = false;
        StageBGM.Instance.bgm_stop = true;

        if (gameObject.GetComponent<BossDamageEffect>().alive == true)
            gameObject.GetComponent<BossDamageEffect>().alive = false;
    }
}
