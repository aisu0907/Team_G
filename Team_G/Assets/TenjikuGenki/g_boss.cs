using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class g_boss : CharacterBase
{
    Rigidbody2D rb;
    public int timer;
    int mode = 0;
    bool down = true;
    public bool reflect = false;
    public int EnemyColor = 0;
    SpriteRenderer img;
    public List<Sprite> Img;
    public GameObject Mark;

    int _Health;
    bool once = true;
    float n = 0;
    public bool did_vib = false;
    public static g_boss Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // CharacterBase�p��
        Health = 1;     //�̗�
        Speed = 0.0f;   //�ړ����x

        rb = GetComponent<Rigidbody2D>();
        img = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (mode)
        {
            case 0:
                timer++;
                if (timer >= 200) mode = Random.Range(1, 2);
                break;
            case 1:
                rush();
                break;
            // case 2:
            //     target(1);
            //     break;
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && collision.gameObject.GetComponent<g_enemy>().OnHitting)
        {
            --Health;
            Destroy(collision.gameObject);
            if (Health == 0)
            {
                Destroy(gameObject);
                SceneManager.LoadScene("Result_Scene");
            }
        }
    }

    void rush()
    {
        if (once)
        {
            _Health = Player.Instance.Health;
            timer = 0;
            once = false;
        }

        if (!did_vib) vibration(transform.position, 60);
        else
        {
            n += 0.05f;
            if (!reflect)
            {
                rb.linearVelocity = new Vector2(0, -Speed - n);
                if (Player.Instance.Health != _Health) reflect = true;
            }
            else
            {
                rb.linearVelocity = new Vector2(0, 3);
                if (transform.position.y >= 3)
                {
                    rb.linearVelocity = new Vector2(0, 0);
                    n = 0;
                    mode = 0;
                    timer = 0;
                    once = true;
                    reflect = false;
                    did_vib = false;
                }
            }
        }
    }

    void vibration(Vector2 BasePos, int Time, float range = 0.5f)
    {
        ++timer;
        if (timer < Time)
        {
            if (timer % 10 == 0)
            {
                if (timer % 2 == 0)
                    transform.position = new Vector2(BasePos.x + range / 2, BasePos.y);
                else
                    transform.position = new Vector2(BasePos.x - range / 2, BasePos.y);
            }
        }
        else
        {
            timer = 0;
            transform.position = BasePos;
            did_vib = true;
            EnemyColor = Random.Range(0, 2);
            img.sprite = Img[EnemyColor];
        }
    }

    // void target(int num)
    // {
    //     if (once)
    //     {
    //         timer = 0;
    //         once = false;
    //     }
    //     else
    //     {
    //         ++timer;
    //         if(timer % 20 == 0)
    //         {
    //             //Instantiate(Mark);
    //             //Debug.Log("tes");
    //             //GameObject Target = Instantiate(Mark);
    //             // target t = Target.GetComponent<target>();
    //             // t.init(200);
    //             // mode = 0;
    //             // once = true;
    //             // timer = 0;
    //         }
    //     }
    // }
}
