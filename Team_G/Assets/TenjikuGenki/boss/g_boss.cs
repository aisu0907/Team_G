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
    Rigidbody2D rb; // ”­ËŠÔŠu
    bool left_move = true;
    
    void Update()
    {
        Timer += 1;

        // ˆê’èŠÔ‚²‚Æ‚É’e‚ğ”­Ë
        if (health > 0)
        {
            if (Timer >= 120)
            {
                Timer = 60;
                ShootBullet();
            } 
        }
        // €–S‰‰o
        else
        {
            if (gameObject.GetComponent<BossDamageEffect>().alive == true)
                gameObject.GetComponent<BossDamageEffect>().alive = false;
        }

        // ¶‰EˆÚ“®
        if(left_move)
        {
            rb.linearVelocityX = speed;
        }
        else
        {
            rb.linearVelocityX = -speed;
        }
    }
    private void Start()
    {
        // Å‰‚Ìó‘Ô‚ğPhase1‚Éİ’è
        img = GetComponent<SpriteRenderer>();
        tmp_pos = transform.position;
        rb = GetComponent<Rigidbody2D>();
        transform.position = new Vector2(transform.position.x - 0.5f,transform.position.y);
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
        int color = Random.Range(0, list.Count);
        img.sprite = sprites[color];
        //Vector2 d = (Player.Instance.transform.position - transform.position).normalized;
        //var e = Instantiate(list[0].pf, transform.position, Quaternion.identity).GetComponent<ENormal>(); e.Init(list[0].db, d, color, 5);
        var e = Instantiate(list[0].pf, transform.position, Quaternion.identity).GetComponent<ENormal>(); e.Init(list[0].db, new Vector2(0,-3), color, 5);
        AudioManager.instance.PlaySound("Shoot");
    }
}