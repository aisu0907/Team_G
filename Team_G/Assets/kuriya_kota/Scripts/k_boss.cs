using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

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
    public GameObject explode;

    private bool isDying = false;
    public int health;
    int _health;
    bool once = true;
    bool beam_once = true;
    float n = 0;
    GameObject t;
    int[] colors = new int[3];

    bool move = true;

    //サウンド
    public AudioClip sound1;
    public AudioClip sound2;
    AudioSource audioSource;

    public static k_boss Instance { get; private set; }

    public ScreenFlash screenFlash;

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

        if(move==true) Move();


        if (move == true && health <= 5)
        {
            img.color = Color.Lerp(
            Color.white,
            Color.red,
            Mathf.Sin(Time.time * 2f) * 0.5f + 0.5f);
        }


        switch (mode)
        {
            case 0:
                if (move == true)  timer++;
                if (timer >= 180)
                {
                    mode = Random.Range(1, 5); // 1から4を選ぶ
                }
                break;
            case 1:
                if (beam_once && health <= 5)
                {
                    beam_once = false; 
                    StartCoroutine(kill_gasybura());//healthが一定以下なら連射
                }
                else beam();
                    break;
            case 2:
                spiralShot();
                if(health<=5) beam();
                break;
            case 3:
                summon_jama();
                if (health <= 5) beam();
                break;
            case 4:
                spiralShot();
                if (health <= 5) beam();
                break;
        }

        if (health <= 0&&!isDying)
        {
            //StartCoroutine(Die());
            Die();
        }
    }

    private void Move()
    {
        float moveX = Mathf.Sin(Time.time) * 1f - 2f;
        transform.position = new Vector2(moveX, transform.position.y);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && collision.gameObject.GetComponent<Enemy>().on_hitting)
        {
            --health;
            Instantiate(explode, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            Destroy(collision.gameObject);
        }
    }

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
                e.Init(enemy, d, Random.Range(0, 2), 3.5f);
        }
        //while (timer < 300) timer++;
        once = true;
        mode = 0;
    }

    private void beam()
    {
        if (once)
        {
            once = false;
            timer = 0;

            // プレイヤーの座標にプレハブを生成
            Instantiate(kill, Player.Instance.transform.position, Quaternion.identity);
        }

        // 攻撃後、一定時間待ってモードを戻す
        if (timer > 120)
        {
            once = true;
            timer = 0;
            mode = 0;
        }
    }
    private IEnumerator beamCoroutine()
    {
        if (!once) yield break;
        once = false;
        timer = 0;

        // プレイヤーの座標にビーム生成
        Instantiate(kill, Player.Instance.transform.position, Quaternion.identity);

        yield return new WaitForSeconds(2f);

        once = true;
    }

    private void summon_jama()
    {
        if (once)
        {
            timer = 0;
            once = false;
        }
        var e2 = Instantiate(prefab2, new Vector2(transform.position.x, transform.position.y), Quaternion.identity).GetComponent<EJammer>();
        e2.Init(enemy2, new Vector2(0, -0.5f), enemy2.speed);
      
            once = true;
            timer = 0;
            mode = 0;   
    }
    private IEnumerator kill_gasybura()
    {
        timer = 0;

        if (beam_once) {
            beam_once = false;
            StartCoroutine(beamCoroutine());
            yield return new WaitForSeconds(1f);


            StartCoroutine(beamCoroutine());
            yield return new WaitForSeconds(1f);

            StartCoroutine(beamCoroutine());
            yield return new WaitForSeconds(1f);
        }
        beam_once = true;
     
        mode = 0;
    }



    //private System.Collections.IEnumerator Die()
    //{
    //    timer = 0;
    //    move = false;

    //    Destroy(gameObject.GetComponent<Enemy>());
    //    Destroy(gameObject.GetComponent<Gasubura>());
    //    Destroy(gameObject.GetComponent<k_target>());

    //    if (isDying) yield break;
    //    isDying = true;

    //    audioSource.PlayOneShot(sound1);
    //    // フラッシュ演出
    //    screenFlash.Flash();
    //    // 2秒待つ（演出時間）
    //    yield return new WaitForSeconds(3f);

    //    // ボス削除
    //    Destroy(gameObject);

    //    // シーン切り替え
    //    SceneManager.LoadScene("Result_Scene");
    //}
    public void Die()
    {
        if (!isDying)
        {
            StartCoroutine(DieRoutine());
            Debug.Log("null");
        }
    }

    private IEnumerator DieRoutine()
    {
        isDying = true;
        move = false;

        // 1. フラッシュ
        screenFlash.Flash();

        // 2. 当たり判定の除去
        Destroy(GetComponent<Enemy>());
        Destroy(GetComponent<Gasubura>());
        Destroy(GetComponent<k_target>());

        // 3. 爆発エフェクト
        Instantiate(explode, transform.position, Quaternion.identity);

        // 4. サウンド
        audioSource.PlayOneShot(sound1);

        // 3秒待つ（演出）
        yield return new WaitForSeconds(3f);

        // 5. ボスを削除
        Destroy(gameObject);

        // 6. シーン遷移
        SceneManager.LoadScene("Result_Scene");
    }

}
