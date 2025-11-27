using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class h_Boss : MonoBehaviour
{
    public EnemyData bullet_data; //’e‚Ìî•ñ
    public GameObject bullet; //’e
    public Rigidbody2D rb;
    public int health;  //‘Ì—Í
    public float speed; //ˆÚ“®‘¬“x
    public int attack1; //UŒ‚1
    public float attack1_x; //UŒ‚1‚ÌxˆÊ’u
    public float attack1_y1;//UŒ‚1‚ÌyˆÊ’u1
    public float attack1_y2;//UŒ‚1‚ÌyˆÊ’u2
    public int attack3; //UŒ‚3
    public int stairs_attack; //ŠK’iUŒ‚
    public float stairs_attack_cooldown;//ŠK’iUŒ‚‚Ì’e‚ÌƒN[ƒ‹ƒ^ƒCƒ€
    public float stairs_attack_space;   //ŠK’iUŒ‚‚Ì’e‚ÌŠÔŠu
    public int stairs_attack_speed;     //ŠK’iUŒ‚‚Ì’e‚Ì‘¬“x
    public int stairs_attack_max;       //ŠK’iUŒ‚‚Ì’e‚Ì‰ñ”

    private Vector2 v1; //ˆÊ’u•Û‘¶—p
    private Vector2 v2; //
    private Vector2 attack1_v1;
    private Vector2 attack1_v2;
    private float stairs_attack_x;  //ŠK’iUŒ‚‚ÌxˆÊ’u
    private float stairs_attack_y;  //ŠK’iUŒ‚‚ÌyˆÊ’u
    private int stairs_attack_count;//ŠK’iUŒ‚ƒJƒEƒ“ƒg—p
    private int attack1_time; //attack1‚ÌUŒ‚ŠÔŠu
    private int stairs_attack_time; //ŠK’iUŒ‚‚ÌUŒ‚ŠÔŠu
    private float next_stairs_attack_time; //ŠK’iUŒ‚‚Ì’e‚ÌƒN[ƒ‹ƒ^ƒCƒ€”äŠr—p
    private bool move = true;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();//Rigidbody2Dæ“¾

        //ƒŠƒZƒbƒg
        next_stairs_attack_time = 0;
        stairs_attack_time = 0;
        stairs_attack_count = 0;
        stairs_attack_y = transform.position.y - (transform.localScale.y % 2);
        stairs_attack_x = transform.position.x + (-stairs_attack_space * (stairs_attack_max - 2));
        v1 = new Vector2(stairs_attack_x, stairs_attack_y);
        v2 = new Vector2(0, -1);
        attack1_v1 = new Vector2(attack1_x, attack1_y1);
        attack1_v2 = new Vector2(attack1_x, attack1_y2);
    }

    public void Update()
    {
        //if (move) 
        //    rb.linearVelocityX = speed;
        //else 
        //    rb.linearVelocityX = -speed;

        //if (attack1_time >= attack1)
        //{
        //}

        //ŠK’iUŒ‚
        if (stairs_attack_time >= stairs_attack)
        {
            if (Time.time >= next_stairs_attack_time)
            {
                Shot(v1, v2); //’e‚ğ¶¬
                next_stairs_attack_time = Time.time + stairs_attack_cooldown; //UŒ‚‚ÌƒN[ƒ‹ƒ^ƒCƒ€
                v1.x += stairs_attack_space; //’e‚ÌˆÊ’u‚ğ‚¸‚ç‚·
                stairs_attack_count++; //UŒ‚‚ğƒJƒEƒ“ƒg
            }

            //
            if (stairs_attack_count >= stairs_attack_max)
            {
                stairs_attack_count = 0;//UŒ‚ƒJƒEƒ“ƒg‚ğƒŠƒZƒbƒg
                stairs_attack_time = 0;  //UŒ‚ƒpƒ^[ƒ“‚ğƒŠƒZƒbƒg
                v1.x = stairs_attack_x;  //’e‚ÌˆÊ’u‚ğƒŠƒZƒbƒg
            }

        }

        if (stairs_attack_time > attack3)
        {

        }

    }

    private void FixedUpdate()
    {
        //UŒ‚‚Ìƒ^ƒCƒ€ƒJƒEƒ“ƒg
        stairs_attack_time++;
        attack1_time++;
    }


    //ƒ_ƒ[ƒW”»’è
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Enemy>(out var enemy))
            if (enemy.on_hitting)
            {
                Destroy(collision.gameObject);
                health--;
                if (health <= 0) GameManager.Instance.KillBoss(gameObject);
            }
        if (collision.GetComponent<Side_Wall>()) move = !move;
    }

    //ŠK’iUŒ‚
    private void Shot(Vector2 _v1, Vector2 _v2)
    {
        int color = Random.Range(0, 2); //’e‚ÌF‚ğŒˆ‚ß‚é
        var e = Instantiate(bullet, _v1, Quaternion.identity).GetComponent<ENormal>(); //’e‚ğ¶¬
        e.Init(bullet_data, _v2, color, stairs_attack_speed); //’e‚Ìî•ñ‚ğw’è
    }

    //attack1
    
    private void range_attack(Vector2 _v)
    {

    }
}
