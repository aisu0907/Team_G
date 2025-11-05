using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class k_boss : MonoBehaviour
{
    Rigidbody2D rb;
    public int timer;
    int mode = 0;
    public int color = 0;
    SpriteRenderer img;
    public List<Sprite> Img;
    public GameObject Mark;
    public EnemyBase enemy;
    public GameObject prefab;
    public EnemyBase enemy2;
    public GameObject prefab2;

    public GameObject kill;


    public float speed;
    public int health;
    int _health;
    bool once = true;
    float n = 0;
    GameObject t;
    int[] colors = new int[3];


    //サウンド
    public AudioClip sound1;
    public AudioClip sound2;
    AudioSource audioSource;

    public static k_boss Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // CharacterBase�p��
        rb = GetComponent<Rigidbody2D>();
        img = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        switch (mode)
        {
            case 0:
                timer++;
                if (timer >= 200)
                {
                    mode = Random.Range(1, 3); // 1か2を選ぶ
                    timer = 0;
                }
                break;
            case 1:
                spiralShot();
                break;
            case 2:
                //rockon();
                beam();
                break;
        }

        if (health <= 0)
        {
            Destroy(gameObject);
            SceneManager.LoadScene("Result_Scene");
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && collision.gameObject.GetComponent<Enemy>().on_hitting)
        {
            --health;
            Destroy(collision.gameObject);
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
                if (color == 0) { colors[0] = 1; img.sprite = Img[colors[0]]; }
                if (color == 1) { colors[0] = 0; img.sprite = Img[colors[0]]; }
                audioSource.PlayOneShot(sound1);
            }
            if (timer == 210) { colors[1] = Random.Range(0, 2); img.sprite = Img[colors[1]]; audioSource.PlayOneShot(sound1); }
            if (timer == 240) { colors[2] = Random.Range(0, 2); img.sprite = Img[colors[2]]; audioSource.PlayOneShot(sound1); }
        }
        else if (270 <= timer && timer <= 330)
        {
            if (timer % 30 == 0)
            {
                Vector2 d = (t.transform.position - transform.position).normalized;
                if (timer == 270) { var e = Instantiate(prefab,transform.position,Quaternion.identity).GetComponent<ENormal>(); e.Init(enemy, d, colors[0], 5); }
                if (timer == 300) { var e = Instantiate(prefab,transform.position,Quaternion.identity).GetComponent<ENormal>(); e.Init(enemy, d, colors[1], 5); }
                if (timer == 330) { var e = Instantiate(prefab,transform.position,Quaternion.identity).GetComponent<ENormal>(); e.Init(enemy, d, colors[2], 5); }
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
    //void movePattern()
    //{
    //    float moveX = Mathf.Sin(Time.time) * 0.3f;
    //    transform.position = new Vector2(moveX, transform.position.y);
    //}

    void spiralShot()
    {
        if (once)
        {
            timer = 0;
            once = false;
        }

        //if (timer % 10 == 0)
        //{
            for (int i = 0; i < 3; i++)
            {
                float angle = (-90f + (i-1) * 70f) * Mathf.Deg2Rad;
                i++;
                Vector2 d = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
                var e = Instantiate(prefab, transform.position, Quaternion.identity).GetComponent<ENormal>();
                e.Init(enemy, d, Random.Range(0, 2), 2.5f);
            }    
            once = true;
            timer = 0;
            mode = 0;
    }
    private void Move()
    {
        float moveX = Mathf.Sin(Time.time) * 2f-2f;
        transform.position = new Vector2(moveX, transform.position.y);
    }

    private void beam()
    {
        if (once)
        {
            once = false;
            timer = 0;

            // プレイヤーの座標にプレハブを生成
            Instantiate(kill, Player.Instance.transform.position, Quaternion.identity);

            // サウンドを鳴らすなら
            audioSource.PlayOneShot(sound2);
        }

        timer++;

        // 攻撃後、一定時間待ってモードを戻す
        if (timer > 120)
        {
            once = true;
            timer = 0;
            mode = 0;
        }
    }

}
