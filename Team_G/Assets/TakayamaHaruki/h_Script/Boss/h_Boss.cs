using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class h_Boss : MonoBehaviour
{
    //ƒQ[ƒ€ƒIƒuƒWƒFƒNƒg
    public EnemyData bullet_data; //’e‚Ìî•ñ
    public GameObject bullet;  //’e
    public GameObject warning;  //Œx
    public GameObject explode; //”š”­‰‰o
    //ƒ{ƒXƒXƒe[ƒ^ƒX
    public int health;  //‘Ì—Í
    //”ÍˆÍUŒ‚Œn
    public int range_attack;//”ÍˆÍUŒ‚
    public float warnig_x;     //Œx‚ÌxˆÊ’u
    public float warning_y_top; //Œx‚ÌyˆÊ’u1
    public float warning_y_down;//Œx‚ÌyˆÊ’u2
    //ŠK’iUŒ‚Œn
    public int stairs_attack;           //ŠK’iUŒ‚
    public float stairs_attack_cooldown;//ŠK’iUŒ‚‚Ì’e‚ÌƒN[ƒ‹ƒ^ƒCƒ€
    public float stairs_attack_space;   //ŠK’iUŒ‚‚Ì’e‚ÌŠÔŠu
    public int stairs_attack_speed;     //ŠK’iUŒ‚‚Ì’e‚Ì‘¬“x
    public int stairs_attack_max;       //ŠK’iUŒ‚‚Ì’e‚Ì‰ñ”

    //À•WŒW
    private Vector2 v1; //ˆÊ’u•Û‘¶—p
    private Vector2 v2; //ˆê“I
    private Vector2 warning_save;
    private Vector2 warning_top;
    private Vector2 warning_down;
    private Vector2 boss_start; //ƒ{ƒX‚Ì‰ŠúˆÊ’u
    //ŠK’iUŒ‚Œn
    private float stairs_attack_x;  //ŠK’iUŒ‚‚ÌxˆÊ’u
    private float stairs_attack_y;  //ŠK’iUŒ‚‚ÌyˆÊ’u
    private int stairs_attack_count;//ŠK’iUŒ‚ƒJƒEƒ“ƒg—p
    private int stairs_attack_time; //ŠK’iUŒ‚‚ÌUŒ‚ŠÔŠu
    private float next_stairs_attack_time; //ŠK’iUŒ‚‚Ì’e‚ÌƒN[ƒ‹ƒ^ƒCƒ€”äŠr—p
    //”ÍˆÍUŒ‚Œn
    private int range_attack_time; //”ÍˆÍUŒ‚‚ÌUŒ‚ŠÔŠu
    //
    private bool dead;
    private Rigidbody2D rb; //
    public static h_Boss Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        //ƒŠƒZƒbƒg
        rb = GetComponent<Rigidbody2D>();
        //ƒ^ƒCƒ€ŠÖŒWƒŠƒZƒbƒg
        next_stairs_attack_time = 0;
        stairs_attack_time = 0;
        //ƒJƒEƒ“ƒgƒŠƒZƒbƒg
        stairs_attack_count = 0;
        //À•Wì¬
        boss_start = transform.position;
        stairs_attack_y = transform.position.y - (transform.localScale.y % 2);
        stairs_attack_x = transform.position.x + (-stairs_attack_space * (stairs_attack_max - 2));
        v1 = new Vector2(stairs_attack_x, stairs_attack_y);
        v2 = new Vector2(0, -1);
        warning_top = new Vector2(warnig_x, warning_y_top);
        warning_down = new Vector2(warnig_x, warning_y_down);
    }

    public void Update()
    {
        //UŒ‚‚Ìƒ^ƒCƒ€ƒJƒEƒ“ƒg
        stairs_attack_time++;
        range_attack_time++;

        if (health >= 0)
        {
            //ŠK’iUŒ‚
            if (stairs_attack_time >= stairs_attack)
            {
                //ƒN[ƒ‹ƒ^ƒCƒ€‚ªI‚í‚Á‚Ä‚¢‚½ê‡
                if (Time.time >= next_stairs_attack_time)
                {
                    Shot(v1, v2); //’e‚ğ¶¬
                    next_stairs_attack_time = Time.time + stairs_attack_cooldown; //UŒ‚‚ÌƒN[ƒ‹ƒ^ƒCƒ€
                    v1.x += stairs_attack_space; //’e‚ÌˆÊ’u‚ğ‚¸‚ç‚·
                    stairs_attack_count++; //UŒ‚‚ğƒJƒEƒ“ƒg
                }

                //Å‘å‚Ü‚ÅUŒ‚‚µ‚½ê‡
                if (stairs_attack_count >= stairs_attack_max)
                {
                    stairs_attack_count = 0;//UŒ‚ƒJƒEƒ“ƒg‚ğƒŠƒZƒbƒg
                    stairs_attack_time = 0;  //UŒ‚ƒpƒ^[ƒ“‚ğƒŠƒZƒbƒg
                    v1.x = stairs_attack_x;  //’e‚ÌˆÊ’u‚ğƒŠƒZƒbƒg
                }

            }

            //”ÍˆÍUŒ‚
            if (range_attack_time >= range_attack)
            {
                range_attack_time = 0;
                warnig_spwn();
            }
        }
        else
        {
            if (dead)
            {
                stairs_attack_time = 0;
                dead = false;
            }
            if (stairs_attack_time % 5 == 0)
                transform.position = new Vector2(boss_start.x + 0.1f, transform.position.y);
            else
                transform.position = new Vector2(boss_start.x - 0.1f, transform.position.y);

            rb.linearVelocityY = 1.0f;
            if (stairs_attack_time >= 330) if (health <= 0) GameManager.Instance.KillBoss(gameObject);
        }
    }

    //ƒ_ƒ[ƒW”»’è
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Enemy>(out var enemy))
            if (enemy.on_hitting)
            {
                Destroy(collision.gameObject);
                health--;
                Instantiate(explode, new Vector2(enemy.transform.position.x, enemy.transform.position.y + 0.5f), Quaternion.identity);
            }
    }

    //ŠK’iUŒ‚
    private void Shot(Vector2 _v1, Vector2 _v2)
    {
        int color = Random.Range(0, 2); //’e‚ÌF‚ğŒˆ‚ß‚é
        var e = Instantiate(bullet, _v1, Quaternion.identity).GetComponent<ENormal>(); //’e‚ğ¶¬
        e.Init(bullet_data, _v2, color, stairs_attack_speed); //’e‚Ìî•ñ‚ğw’è
    }

    //Œx
    private void warnig_spwn()
    {
        int save = Random.Range(0, 3); //ƒ‰ƒ“ƒ_ƒ€‚Å”’l‚ğæ“¾

        //Œx‚ÌÀ•Wİ’è
        if (save < 2)
        warning_save = warning_down;
        else 
        warning_save = warning_top;

        //Œx‚ğ¶¬
        Instantiate(warning, warning_save, Quaternion.identity);
    }
}
