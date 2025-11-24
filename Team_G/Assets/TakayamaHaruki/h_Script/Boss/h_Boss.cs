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
    public int attack2_num;   //UŒ‚2‚Ì’e‚Ì”
    public int attack2_space; //UŒ‚2‚Ì’e‚ÌŠÔŠu
    public int attack2_speed; //UŒ‚2‚Ì’e‚Ì‘¬“x

    private Vector2 v;  //ˆÊ’u•Û‘¶—p
    private float attack2_x; //UŒ‚2‚ÌxˆÊ’u
    private float attack2_y; //UŒ‚2‚ÌyˆÊ’u
    private int attack_time; //UŒ‚ŠÔŠu
    void Start()
    {
        //ƒŠƒZƒbƒg
        attack_time = 0;
        attack2_x = this.transform.position.y + this.transform.localScale.y;
        attack2_y = this.transform.position.y + (attack2_num * -attack2_space);
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
            for (int i = 0; i < attack2_num; i++)
            {
                Shot();
                Direy();
                v.y += attack2_space;
            }

            attack_time = 0;
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

    private async void Direy()
    {
        await Task.Delay(1000);
    }

    private void Shot()
    {
        int color = Random.Range(0, 1); //’e‚ÌF‚ğŒˆ‚ß‚é
        var e = Instantiate(bullet, v, Quaternion.identity).GetComponent<ENormal>(); 
        e.Init(bullet_data, v, color, attack2_speed);
    }
}
