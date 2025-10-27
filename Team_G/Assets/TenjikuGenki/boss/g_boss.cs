using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class g_boss : CharacterBase
{
    Rigidbody2D rb;
    public int timer;
    int mode = 0;
    public bool reflect = false;
    public int EnemyColor = 0;
    SpriteRenderer img;
    public List<Sprite> Img;
    public GameObject Mark;
    public GameObject enemy;

    int _Health;
    bool once = true;
    float n = 0;
    int j = 0;
    public bool did_vib = false;
    GameObject[] Target;
    GameObject t;
    int[] colors = new int[3];
    public static g_boss Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // CharacterBase�p��
        Health = 2;     //�̗�
        Speed = 3.0f;   //�ړ����x

        rb = GetComponent<Rigidbody2D>();
        img = GetComponent<SpriteRenderer>();
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        switch (mode)
        {
            case 0:
                timer++;
                if (timer >= 200) mode = 3;//Random.Range(1, 4);
                break;
            case 1:
                rush();
                break;
            case 2:
                tracking();
                break;
            case 3:
                rockon();
                break;
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
            once = false;
                timer = 0;
        }

        if (!did_vib) vibration(transform.position, 60);
        else
        {
            n += 0.5f;
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
            Debug.Log("test");
        }
    }

    void tracking()
    {
        if (once)
        {
            timer = 0;
            once = false;
            Target = new GameObject[3];
        }

        timer++;
        if (j < 3)
        {
            if (timer % 40 == 0)
            {
                Target[j] = Instantiate(Mark, Player.Instance.transform.position, Quaternion.identity);
                j++;
            }
        }
        else if (timer >= 180)
        {
            if (timer % 40 == 0 && j < 6)
            {
                // Spawn Enemy
                GameObject Enemy = Instantiate(enemy, transform.position, Quaternion.identity);
                g_enemy e = Enemy.GetComponent<g_enemy>();
                Vector2 direction = (Target[j - 3].transform.position - Enemy.transform.position).normalized;
                e.Create(transform.position, direction, 0, EnemyColor, 8);

                // Remove It
                Destroy(Target[j - 3]);
                j++;

            }

            //Reset
            if (timer == 500)
            {
                once = true;
                mode = 0;
                timer = 0;
                Target = null;
                j = 0;
            }
        }
    }

    void rockon()
    {
        if (once)
        {
            timer = 0;
            once = false;
            t = Instantiate(Mark, Player.Instance.transform.position, Quaternion.identity);
        }

        timer++;
        if (timer <= 240)
        {
            t.transform.position = Player.Instance.transform.position;
        }
        if (180 <= timer && timer <= 240)
        {
            if (timer == 180)
            {
                if (EnemyColor == 0) { colors[0] = 1; img.sprite = Img[colors[0]]; }
                if (EnemyColor == 1) { colors[0] = 0; img.sprite = Img[colors[0]]; }
            }
            if (timer == 210) { colors[1] = Random.Range(0, 2); img.sprite = Img[colors[1]]; }
            if (timer == 240) { colors[2] = Random.Range(0, 2); img.sprite = Img[colors[2]]; }
        }
        else if (270 <= timer && timer <= 330)
        {
            if (timer % 30 == 0)
            {
                GameObject Enemy = Instantiate(enemy, transform.position, Quaternion.identity);
                g_enemy e = Enemy.GetComponent<g_enemy>();
                Vector2 direction = (t.transform.position - Enemy.transform.position).normalized;
                if (timer == 270) e.Create(transform.position, direction, 0, colors[0], 8);
                if (timer == 300) e.Create(transform.position, direction, 0, colors[1], 8);
                if (timer == 330) e.Create(transform.position, direction, 0, colors[2], 8);
            }
        }
        else if (timer == 360)
        {
            once = true;
            mode = 0;
            timer = 0;
            Destroy(t);
        }
    }
}
