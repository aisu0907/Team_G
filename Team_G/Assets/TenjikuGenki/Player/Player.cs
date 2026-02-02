using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;


public class Player : MonoBehaviour
{
    [Header("▼ GameObject")]
    public GameObject explode;
    public GameObject shield;
    public SpriteRenderer img; //画像
    public Rigidbody2D rbody;

    [Header("▼ PlayerStatus")]
    public int health = 3;      //体力
    public float speed = 3.0f;  //移動速度
    float axisH, axisV = 0.0f;  //移動ベクトル

    [Header("▼ Bom")]
    public int bom = 0;     //ボムの所持数
    public int max_bom = 0; //ボム最大所持数
    bool bomb_switch;

    [Header("▼ DamageEffect")]
    public GameObject shake;
    public int blinks_max;  //点滅する回数
    public int damage_time; //消滅タイミング
    public int save_time;   //表示タイム
    public int timer = 0;   //タイマー
    public int shake_max;   //画面の振動回数
    bool damage_hit;        //ダメージ判定
    Color save_color;       //通常の色
    Color damage_color;     //ダメージ時の色
    int color_timer;        //色切り替えタイマー
    int color_count;        //色切り替え回数
    int shake_count;        //振動した回数
    float tmp_pos;
    bool right = true;

    [Header("▼ StartPosition")]
    public float start_x = -2;  //X座標
    public float start_y = -6;  //Y座標
    
    [Header("▼ Direction")]
    public float targetY = -3.0f;   //出現位置
    public bool start_anime = true; //アニメーション切り替え


    public static Player Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Instantiate(shield);


        //RigidBody
        rbody = this.GetComponent<Rigidbody2D>();

        //被弾
        damage_hit = true;
        color_count = 0;
        color_timer = 0;
        shake_count = 0;
        save_color = img.color;
        damage_color = new Color(save_color.r, save_color.g, save_color.b, 0.5f);

        //開始位置
        transform.position = new Vector3(start_x,start_y,0);
        start_anime = true;
    }

    // Update is called once per frame
    void Update()
    {
        //画面下から出現
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
            // 移動
            axisH = Input.GetAxisRaw("Horizontal");
            axisV = Input.GetAxisRaw("Vertical");
            if (0.2 <= transform.position.y) 
                if(axisV > 0.0f) axisV = 0.00f;
            if (transform.position.y <= -5.5)
                if(axisV < 0.0f) axisV = 0.00f;

            // 盾の位置更新
            Shield.Instance.transform.position = new Vector2(transform.position.x, transform.position.y + 0.8f);

            //ボムの処理
            if (Input.GetKey(KeyCode.Space))
            {

                if (bom > 0 && bomb_switch)
                {

                    AudioManager.instance.PlaySound("bom", 1f);
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


                bomb_switch = false; 
            }
            else
            {
                bomb_switch = true;
            }

            // 被弾演出
            if (!damage_hit)
            {
                color_timer++;

                if(shake_count < shake_max)
                {
                    tmp_pos = shake.transform.position.x;
                    shake.transform.position = new Vector3(right == true ? tmp_pos + 0.15f : tmp_pos - 0.15f, 0, -10);
                    right = !right;
                    shake_count++;
                }

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
                    shake_count = 0;
                    damage_hit = true;
                }
            }
        }
        else
        {
            timer++;
            if (timer >= 120)
            {
                SceneManager.LoadScene("GameoverScene");
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
            if (collision.TryGetComponent<Enemy>(out var e) && !e.on_hitting)
            {
                Damage(1, collision.gameObject);
            }

            //if (collision.TryGetComponent<Gasubura>(out var b)) b.Damage(); 
        }
    }

    /// <summary>
    /// プレイヤーにダメージを与える処理
    /// </summary>
    /// <param name="damage"></param>
    /// <param name="obj"></param>
    /// <param name="destroy"></param>
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
    }
}