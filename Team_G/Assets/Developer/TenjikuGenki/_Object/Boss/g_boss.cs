using Const;
using System.Collections.Generic;
using UnityEngine;

public class g_boss : BossBase
{
    [System.Serializable] public class enemy_list { public EnemyData db; public GameObject pf; };
    [Header("Å•Generator")]
    public List<enemy_list> list = new List<enemy_list>();
    [Header("Å•Images")]
    public SpriteRenderer img;
    public List<Sprite> sprites;
    [SerializeField] int Timer;
    Vector2 tmp_pos;
    bool left_move = true;
    public GameObject rflash;

    protected override void Start()
    {
        base.Start();
        _states.Add(new MoveSpeed(1.0f));

        // ç≈èâÇÃèÛë‘ÇPhase1Ç…ê›íË
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

        // àÍíËéûä‘Ç≤Ç∆Ç…íeÇî≠éÀ
        if (health > 0)
        {
            if (Timer >= 210)
            {
                Timer = 60;
                ShootBullet();
            } 
        }
        // éÄñSââèo
        else
        {
            if (gameObject.GetComponent<BossDamageEffect>().alive == true)
                gameObject.GetComponent<BossDamageEffect>().alive = false;
        }

        // ç∂âEà⁄ìÆ
        if(left_move)
        {
            _rb.linearVelocityX = _states[(int)StateName.Speed].CurrentState;
        }
        else
        {
            _rb.linearVelocityX = -_states[(int)StateName.Speed].CurrentState;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (health > 0)
            boss_damage(collision);
        if (collision.GetComponent<SideWall>()) left_move = !left_move;
    }

    // éÀåÇ
    void ShootBullet()
    {
        COLOR color = (COLOR)Random.Range(0, list.Count);
        img.sprite = sprites[(int)color];
        //Vector2 d = (Player.Instance.transform.position - transform.position).normalized;
        //var e = Instantiate(list[0].pf, transform.position, Quaternion.identity).GetComponent<ENormal>(); e.Init(list[0].db, d, color, 5);
        var e = Instantiate(list[0].pf, transform.position, Quaternion.identity).GetComponent<ENormal>(); e.Init(list[0].db, new Vector2(0,-2.5f), color, 5);
        AudioManager.instance.PlaySound("Shoot");
    }
}