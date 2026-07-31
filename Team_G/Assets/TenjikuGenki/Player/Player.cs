using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class Player : ObjBase
{
    [Header("▼ GameObject")]
    public GameObject explode;
    public GameObject shield;
    public GameObject flash;
    public SpriteRenderer img; //画像
    public GameObject bgm;

    [Header("▼ PlayerStatus")]
    public int health = 3;      //体力

    [Header("▼ Bom")]
    public int bom = 0;     //ボムの所持数
    public int max_bom = 0; //ボム最大所持数

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

    bool isStop = false;

    [Header("▼ Phisics")]
    [SerializeField] Transform TopMoveLimit;
    [SerializeField] Transform BottomMoveLimit;
    bool IsMoveLimit(Vector2 vec) {
        return transform.position.y > TopMoveLimit.position.y && vec.y > 0.0f ||
            transform.position.y < BottomMoveLimit.position.y && vec.y < 0.0f;
    }
    public StartAnimation sa;

    public static Player Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();

        _speed = 3.0f;

        //被弾
        color_count = 0;
        color_timer = 0;
        shake_count = 0;
        save_color = new Color(img.color.r, img.color.g, img.color.b, img.color.a);
        damage_hit = true;
        damage_color = new Color(save_color.r, save_color.g, save_color.b, 0.5f);
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (!sa.StartAnime()) return;

        if (health <= 0)
        {
            // ちょっと待つ
            if (++timer >= 120)
                SceneManager.LoadScene("GameoverScene");
            return;
        }

        if (!damage_hit)
        {
            color_timer++;

            if (shake_count < shake_max)
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
                img.color = save_color;//通常の色に変更
                //リセット
                color_timer = 0;
                color_count = 0;
                shake_count = 0;
                damage_hit = true;
            }
        }

        //ESCでタイトルに戻る
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            SceneManager.LoadScene("TitleScene");
        }

        // 中断
        if (isStop) return;

        // 盾の位置更新
        Shield.Instance.transform.position = new Vector2(transform.position.x, transform.position.y + 0.8f);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IHitable>(out var e))
        {
            e.Hit();
            Damage(e.Damage);
        }

        //アイテムに当たった場合
        if (collision.TryGetComponent<Item>(out var i))
        {
            //音を鳴らす
            AudioManager.instance.PlaySound("GetItem");
            Shield_Item.Instance.ItemGet(i, i.item_id);

            //アイテムを削除
            Destroy(i.gameObject);
        }
    }

    void FixedUpdate()
    {
        // これ以上移動できないなら、移動を中断
        if (IsMoveLimit(_rb.linearVelocity))
            _rb.linearVelocityY = 0;
    }

    public void Move(InputAction.CallbackContext ctx)
    {
        if (!(health > 0) && sa.StartAnime()) return;

        // 移動処理
        Vector2 vec = ctx.ReadValue<Vector2>();

        // 移動に限界を設定する
        if (IsMoveLimit(vec)) vec.y = 0.0f;
        _rb.linearVelocity = vec * _speed;
    }

    public void Bom(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            if (!(health > 0)) return;

            if (bom > 0)
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
                Instantiate(flash, new Vector2(transform.position.x, transform.position.y), Quaternion.identity); //画面全体にフラッシュを生成

                //bomの数を減らす
                bom--;
            }
        }
    }

    /// <summary>
    /// プレイヤーにダメージを与える処理
    /// </summary>
    /// <param name="damage"></param>
    /// <param name="obj"></param>
    /// <param name="destroy"></param>
    public void Damage(int damage, GameObject obj = null, bool destroy = true)
    {
        // ダメージのクールタイム中なら中断
        if (!damage_hit) return;

        // ビジュアル
        img.color = damage_color;
        AudioManager.instance.PlaySound("PlayerDamage");

        // ヒット処理
        health -= damage;
        damage_hit = false;
        if (destroy) Destroy(obj);
    }

    public void Stop()
    {
        _rb.linearVelocity = Vector2.zero;
        isStop = true;
    }
}