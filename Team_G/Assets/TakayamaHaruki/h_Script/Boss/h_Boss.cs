using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class h_Boss : MonoBehaviour
{
    public EnemyData bullet_data; //’e‚Ìî•ñ
    public GameObject bullet; //’e
    public int health;  //‘Ì—Í
    public int attack1; //UŒ‚1
    public int attack2; //UŒ‚2
    public int attack3; //UŒ‚3
    public float attack2_cooldown;//UŒ‚2‚Ì’e‚ÌƒN[ƒ‹ƒ^ƒCƒ€
    public float attack2_space;   //UŒ‚2‚Ì’e‚ÌŠÔŠu
    public int attack2_speed;     //UŒ‚2‚Ì’e‚Ì‘¬“x
    public int attack2_max;       //UŒ‚2‚Ì’e‚Ì‰ñ”

    private Vector2 v;        //ˆÊ’u•Û‘¶—p
    private float attack2_x;  //UŒ‚2‚ÌxˆÊ’u
    private float attack2_y;  //UŒ‚2‚ÌyˆÊ’u
    private int attack2_count;//UŒ‚2ƒJƒEƒ“ƒg—p
    private int attack_time;  //UŒ‚ŠÔŠu
    private float next_attack_time;
    private bool a;
    void Start()
    {
        //ƒŠƒZƒbƒg
        next_attack_time = 0;
        attack_time = 0;
        attack2_count = 0;
        attack2_y = transform.position.y - (transform.localScale.y % 2);
        attack2_x = transform.position.x + (-attack2_space * (attack2_max - 2));
        v = new Vector2(attack2_x, attack2_y);
    }

    public void Update()
    {
        if (attack_time > attack1)
        {

        }

        //ŠK’iUŒ‚
        if (attack_time >= attack2)
        {
            if (Time.time >= next_attack_time)
            {
                Shot(v); //’e‚ğ¶¬
                next_attack_time = Time.time + attack2_cooldown; //UŒ‚‚ÌƒN[ƒ‹ƒ^ƒCƒ€
                v.x += attack2_space; //’e‚ÌˆÊ’u‚ğ‚¸‚ç‚·
                attack2_count++;
            }

            if (attack2_count >= attack2_max)
            {
                attack2_count = 0;
                attack_time = 0; //UŒ‚ƒpƒ^[ƒ“‚ğƒŠƒZƒbƒg
                v.x = attack2_x;
            }
        }

        if (attack_time > attack3)
        {

        }

    }

    private void FixedUpdate()
    {
        attack_time++;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Enemy>(out var enemy))
            if (enemy.on_hitting)
            {
                Destroy(collision.gameObject);
                health--;
                if (health == 0) GameManager.Instance.KillBoss(gameObject);
            }
    }

    private void Shot(Vector2 _v)
    {
        int color = Random.Range(0, 2); //’e‚ÌF‚ğŒˆ‚ß‚é
        var e = Instantiate(bullet, _v, Quaternion.identity).GetComponent<ENormal>(); //’e‚ğ¶¬
        e.Init(bullet_data, new Vector2(0 , -1), color, attack2_speed); //’e‚Ìî•ñ‚ğw’è
    }
}
