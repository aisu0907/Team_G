//using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class k_boss : BossBase 
{
    Rigidbody2D rb;
    SpriteRenderer img;

    public int timer;
    public int jamamer_timer;
    public int attack=240;
    int mode = 0;
    int jama = 0;

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
    public GameObject kill;

    public GameObject flash;

    AudioSource audioSource;

    private void Awake()
    {
        Instance = this;
    }

    public static k_boss Instance { get; private set; }

    void Start()
    {
        Instantiate(flash, transform.position, Quaternion.identity);
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

        if (jamamer_timer >= attack*4)
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
                if(!isDying) timer++;
                if (timer >= attack)
                {
                    mode = Random.Range(1, 5);
                }
                break;

            case 1:
                if (health <= 5)
                    StartCoroutine(KillRapid());
                else
                    Beam();
                break;

            case 2:
                StartCoroutine(Gumishot());
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

    void OnTriggerEnter2D(Collider2D collision) {
        if (health > 0)
            boss_damage(collision);
    }

    private void Move()
    {
            float x = Mathf.Sin(Time.time) * speed - 2f;
            transform.position = new Vector2(x, transform.position.y);
    }

    private void Death_Move()
    {
        float speed = 40f;
        float amount = 0.1f;

        float offsetX = Mathf.Sin(Time.time * speed) * amount;

        transform.position = basePos + new Vector2(offsetX, 0);
    }

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

    private void Beam()
    {
        if (!beamOnce) return;
        beamOnce = false;

        timer = 0;

        Instantiate(kill, Player.Instance.transform.position, Quaternion.identity);

        StartCoroutine(ResetBeam());
    }

    IEnumerator ResetBeam()
    {
        yield return new WaitForSeconds(2f);
        beamOnce = true;
        timer = 0;
        mode = 0;
    }

    private void SummonJammer()
    {
        if (!summonOnce) return;
        summonOnce = false;

        var e = Instantiate(prefab2, transform.position, Quaternion.identity)
                .GetComponent<EJammer>();
        e.Init(enemy2, new Vector2(0, -0.2f), enemy2.speed);

        summonOnce = true;
    }

    private IEnumerator KillRapid()
    {
        if (!beamOnce) yield break;
        beamOnce = false;

        for (int i = 0; i < 2; i++)
        {
            Instantiate(kill, Player.Instance.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(2f);
        }

        beamOnce = true;
        mode = 0;
    }

    private IEnumerator Gumishot()
    {
        if (!targetOnce) yield break;
        targetOnce = false;

        for (int i = 0; i < 5; i++)
        {

            Vector2 d = (Player.Instance.transform.position - transform.position).normalized;
            var e = Instantiate(prefab, transform.position, Quaternion.identity).GetComponent<ENormal>(); e.Init(enemy, d, Random.Range(0, 2), 3);
            AudioManager.instance.PlaySound("Shoot");
            yield return new WaitForSeconds(0.6f);
        }

        targetOnce = true;
        timer = 0;
        mode = 0;
    }


    public void Die()
    {
        if (isDying) return;
        isDying = true;
        move = false;
        Stage_BGM.Instance.bgm_stop = true;

        if (gameObject.GetComponent<Boss_Damage_Effect>().alive == false)
            gameObject.GetComponent<Boss_Damage_Effect>().alive = true;
    }
}
