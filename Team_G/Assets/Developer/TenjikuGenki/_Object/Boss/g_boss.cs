using Const;
using System.Collections.Generic;
using UnityEngine;

public class g_boss : BossBase
{
    [System.Serializable] public class enemy_list { public EnemyData db; public GameObject pf; };
    [Header("¥Generator")]
    public List<enemy_list> list = new List<enemy_list>();
    [Header("¥Images")]
    public SpriteRenderer img;
    public List<Sprite> sprites;
    [SerializeField] int Timer;
    Vector2 tmp_pos;
    bool left_move = true;
    public GameObject rflash;
    public float hit_up_speed;
    public int attack_time;
    public int down_attack_time;

    private float up_speed;
    private int hit_count;

    protected override void Start()
    {
        base.Start();
        _states.Add(new MoveSpeed(1.0f));

        up_speed = 0;
        hit_count = 0;

        // Å‰‚Ìó‘Ô‚ğPhase1‚Éİ’è
        img = GetComponent<SpriteRenderer>();
        tmp_pos = transform.position;
        transform.position = new Vector2(transform.position.x - 0.5f, transform.position.y);
        Instantiate(rflash);

        boss_bgm = h_AudioManager.Instance;

        boss_bgm.PlayBGM(AudioConst.BGM_ID.BOSS_BGM_1, bgm_volume);
    }

    protected override void Update()
    {
        Timer += 1;

        // ˆê’èŠÔ‚²‚Æ‚É’e‚ğ”­Ë
        if (health > 0)
        {
            if (Timer >= attack_time)
            {
                Timer = 60;
                ShootBullet();
            } 
        }
        // €–S‰‰o
        else
        {
            if (gameObject.GetComponent<BossDamageEffect>().alive == true)
            {
                up_speed = 0;
                gameObject.GetComponent<BossDamageEffect>().alive = false;
            }
        }

        // ¶‰EˆÚ“®
        if(left_move)
        {
            _rb.linearVelocityX = (_states[(int)StateName.Speed].CurrentState + up_speed);
        }
        else
        {
            _rb.linearVelocityX = (-_states[(int)StateName.Speed].CurrentState - up_speed);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (health > 0)
            boss_damage(collision);
        if (collision.GetComponent<SideWall>()) left_move = !left_move;
    }

    // ËŒ‚
    void ShootBullet()
    {
        COLOR color = (COLOR)Random.Range(0, list.Count);
        img.sprite = sprites[(int)color];
        //Vector2 d = (Player.Instance.transform.position - transform.position).normalized;
        //var e = Instantiate(list[0].pf, transform.position, Quaternion.identity).GetComponent<ENormal>(); e.Init(list[0].db, d, color, 5);
        var e = Instantiate(list[0].pf, transform.position, Quaternion.identity).GetComponent<ENormal>(); e.Init(list[0].db, new Vector2(0,-2.5f), color, 5);
        AudioManager.instance.PlaySound("Shoot");
    }

    public override void boss_damage(Collider2D collision)
    {
        base.boss_damage(collision);

        //G‚ê‚½‘Šè‚ÉEnemyƒNƒ‰ƒX‚ª‚Â‚¢‚Ä‚¢‚½‚ç
        if (collision.TryGetComponent<IReflectable>(out var enemy))
            //G‚ê‚½ƒEƒCƒ‹ƒX‚ª‘Å‚¿•Ô‚³‚ê‚½‚à‚Ì‚È‚ç‚Î
            if (enemy.Hitting)
            {

                if (hit_count >= 2)
                {
                    up_speed += hit_up_speed;
                    attack_time -= down_attack_time;
                    hit_count = 0;
                }
                else
                    hit_count++;
            }
    }
}