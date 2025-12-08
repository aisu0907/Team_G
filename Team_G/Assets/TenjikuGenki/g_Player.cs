using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

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
    public int null_time;  //消滅タイミング
    public int save_time;  //表示タイム

    public GameObject explode;
    public SpriteRenderer img; //画像

    private bool damage_hit;
    private Color save_color; //
    private Color null_color; //
    private int color_timer;  //
    private int color_count;
    private int frame = 0;

    public static Player Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
        null_color = new Color(save_color.r, save_color.g, save_color.b, 0.5f);
    }

    // Update is called once per frame
    void Update()
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
                img.color = save_color;
                color_count++;
            }

            if (color_timer >= null_time)
            {
                img.color = null_color;
                color_count++;
                color_timer = 0;
            }



            if (color_count >= blinks_max)
            {
                color_timer = 0;
                color_count = 0;
                damage_hit = true;
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
            img.color = null_color;
            health -= damage;
            AudioManager.instance.PlaySound("PlayerDamage");
            damage_hit = false;
        }
        if (destroy) Destroy(obj);

        //プレイヤーの体力が0以下の場合
        if (health <= 0)
        {
            //ゲームオーバーシーンに移行
            DataHolder.GetGameData();
            SceneManager.LoadScene("Gameover_Scene");
        }
    }
}