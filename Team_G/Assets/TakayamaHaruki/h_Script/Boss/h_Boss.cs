using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class h_Boss : MonoBehaviour
{
    public EnemyData bullet_data; //íeÇÃèÓïÒ
    public GameObject bullet; //íe
    public int health;  //ëÃóÕ
    public int attack1; //çUåÇ1
    public int attack2; //çUåÇ2
    public int attack3; //çUåÇ3
    public int attack2_num;   //çUåÇ2ÇÃíeÇÃêî
    public int attack2_space; //çUåÇ2ÇÃíeÇÃä‘äu
    public int attack2_speed; //çUåÇ2ÇÃíeÇÃë¨ìx

    private Vector2 v;  //à íuï€ë∂óp
    private float attack2_x; //çUåÇ2ÇÃxà íu
    private float attack2_y; //çUåÇ2ÇÃyà íu
    private int attack_time; //çUåÇä‘äu
    void Start()
    {
        //ÉäÉZÉbÉg
        attack_time = 0;
        attack2_y = transform.position.y + transform.localScale.y;
        attack2_x = 0 + (attack2_num * -attack2_space);
        v = new Vector2(attack2_x, attack2_y);
    }

    public void Update()
    {
        if (attack_time > attack1)
        {

        }

        //äKíiçUåÇ
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
        int color = Random.Range(0, 1); //íeÇÃêFÇåàÇﬂÇÈ
        var e = Instantiate(bullet, v, Quaternion.identity).GetComponent<ENormal>(); 
        e.Init(bullet_data, new Vector2(), color, attack2_speed);
    }
}
