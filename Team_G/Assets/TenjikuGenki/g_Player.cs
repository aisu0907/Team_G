using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;


public class Player : MonoBehaviour
{
    Rigidbody2D rbody;
    float axisH, axisV = 0.0f;
    public GameObject sheild;
    public int health = 3;     //�̗�
    public float speed = 3.0f;   //�ړ����x
    public int bom = 0;     //ボムの所持数
    public int bom_time = 0;//ボムのクールタイム
    public int max_bom = 0; //ボム最大所持数
    public int blinks_max; //点滅する回数
    public int damage_time;  //消滅タイミング
    public int save_time;  //表示タイム
    public int timer = 0;

    public int start_x = -2;
    public int start_y = -6;


    //演出用
    public float targetY = -3.0f;
    public bool start_anime = true;

    public GameObject explode;
    public SpriteRenderer img; //画像

    private bool damage_hit;    //ダメージ判定
    private Color save_color;   //通常の色
    private Color damage_color; //ダメージ時の色
    private int color_timer;    //色切り替えタイマー
    private int color_count;    //色切り替え回数
    private int frame = 0;

    public static Player Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = new Vector3(start_x,start_y,0);


        //リセット
        // RigidBody2D��������擾����
        rbody = this.GetComponent<Rigidbody2D>();
        // ���̐���
        Instantiate(sheild);
        frame = 300;
        damage_hit = true;
        //色関係
        color_count = 0;
        color_timer = 0;
        save_color = img.color;
        damage_color = new Color(save_color.r, save_color.g, save_color.b, 0.5f);
        start_anime = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (start_anime)
        {
            if (transform.position.y < targetY)
            {
                transform.position += new Vector3(0, speed * Time.deltaTime, 0);
            }
            else
            {
                start_anime = false;
            }
        }


        if (health > 0&&!start_anime)
        {
            // �L�[�擾
            axisH = Input.GetAxisRaw("Horizontal");
            axisV = Input.GetAxisRaw("Vertical");
            if (0.2 <= transform.position.y) axisV = -0.05f;
            if (transform.position.y <= -4.5) axisV = 0.05f;

            // �Ǐ]����
            Sheild.Instance.transform.position = new Vector2(transform.position.x, transform.position.y + 0.8f);

            //ボムの処理
            if (frame >= bom_time)
            {
                if (Input.GetKey(KeyCode.Space) && bom > 0)
                {
                    frame = 0;
                    // "Enemy"タグがついたすべてのオブジェクトを取得
                    GameObject[] objects = GameObject.FindGameObjectsWithTag("Enemy");

                    // 各オブジェクトを削除
                    foreach (GameObject obj in objects)
                    {
                        Destroy(obj);
                        Instantiate(explode, obj.transform.position, Quaternion.identity);
                    }

                    //bomの数を減らす
                    bom--;
                }
            }

            frame++;

            if (!damage_hit)
            {
                color_timer++;
                
                if (color_timer == save_time)
                {
                    img.color = save_color;//通常の色に変更
                    color_count++;
                }

                if (color_timer >= damage_time)
                {
                    img.color = damage_color;//ダメージ時の色に変更
                    color_count++;
                    color_timer = 0;//タイマーリセット
                }
                //色切り替え回数が最大回数に達したら
                if (color_count >= blinks_max)
                {
                    //リセット
                    color_timer = 0; 
                    color_count = 0;
                    damage_hit = true;
                }
            }
        }
        else
        {
            timer++;
            if (timer >= 120)
            {
                SceneManager.LoadScene("Gameover_Scene");
                DataHolder.GetGameData();
            }
        }
    }

    void FixedUpdate()
    {
        // 移動処理
        rbody.linearVelocity = new Vector2(axisH * speed, axisV * speed);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // 衝突判定
        if (collision.TryGetComponent<IDamageable>(out var hit))
        {
            if (collision.TryGetComponent<Enemy>(out var e) && !e.on_hitting) Damage(1, collision.gameObject);

            //if (collision.TryGetComponent<Gasubura>(out var b)) b.Damage(); 
        }
    }

    public void Damage(int damage, GameObject obj, bool destroy = true)
    {
        if (damage_hit)
        {
            img.color = damage_color;
            health -= damage;
            AudioManager.instance.PlaySound("PlayerDamage");
            damage_hit = false;
        }
        if (destroy) Destroy(obj);

        //プレイヤーの体力が0以下の場合
        if (health <= 0)
        {
            //ゲームオーバーシーンに移行
            Time.timeScale = 0.0f;
            rbody.linearVelocity = Vector2.zero;
        }
    }
}