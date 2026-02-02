//h_Boss.cs

using System.Diagnostics.CodeAnalysis;
using Unity.Jobs;
using UnityEngine;

public class h_Boss : BossBase
{
    //ゲームオブジェクト
    [Header("▼Object Data")]
    public EnemyData bullet_data;//弾の情報
    public GameObject bullet;    //弾
    public GameObject warning;   //警告
    public GameObject flash;     //出現時演出フラッシュ
    [Header("▼Boss Move")]
    public float left_turn_pos; //右側の反転位置
    public float right_turn_pos;//左側の反転位置
    public float boost_speed;   //加速するスピード
    private float start_speed;  //スピード保存用
    private bool turn;//移動反転用フラグ
    //範囲攻撃
    [Header("▼Range Attack")]
    public int range_attack_interval;//範囲攻撃
    private int range_attack_time;   //範囲攻撃の攻撃間隔
    //階段攻撃
    [Header("▼Stairs Attack")]
    public int stairs_attack_interval;    //階段攻撃
    public float stairs_attack_cooldown;  //階段攻撃の弾のクールタイム
    public float stairs_attack_space;     //階段攻撃の弾の間隔
    public int stairs_attack_speed;       //階段攻撃の弾の速度
    public int stairs_attack_max;         //階段攻撃の弾の回数
    private float stairs_attack_x;        //階段攻撃のx位置
    private float stairs_attack_y;        //階段攻撃のy位置
    private int stairs_attack_count;      //階段攻撃カウント用
    private int stairs_attack_time;       //階段攻撃の攻撃間隔
    private float next_stairs_attack_time;//階段攻撃の弾のクールタイム比較用
    //警告表示
    [Header("▼Warning")]
    private bool warning_switch;    //表示位置切り替え用
    public float warning_x_pos;     //警告表示のx位置
    public float warning_y_top_pos; //警告表示のy位置
    public float warning_y_down_pos;//警告表示のy位置2
    //座標係
    private Vector2 stairs_attack_pos;//位置保存用
    private Vector2 bullet_vec;       //弾の方角
    private Vector2 warning_save;     //警告表示位置一時的に保存用
    private Vector2 warning_top_pos;  //警告表示位置1
    private Vector2 warning_down_pos; //警告表示位置2

    private Rigidbody2D rb;
    public static h_Boss Instance { get; private set; }

    private void Awake()
    {
        //シングルトン
        Instance = this;
    }

    void Start()
    {
        Instantiate(flash, new Vector2(transform.position.x,transform.position.y), Quaternion.identity); //画面全体にフラッシュを生成

        //リセット
        rb = GetComponent<Rigidbody2D>();
        warning_switch = true;
        //タイム関係リセット
        next_stairs_attack_time = 0;
        stairs_attack_time = 0;
        //カウントリセット
        stairs_attack_count = 0;
        //階段攻撃座標
        stairs_attack_y = transform.position.y - (transform.localScale.y % 2);
        stairs_attack_x = transform.position.x + (-stairs_attack_space * (stairs_attack_max - 2));
        stairs_attack_pos = new Vector2(stairs_attack_x, stairs_attack_y);
        bullet_vec = new Vector2(0, -1);
        //警告表示座標
        warning_top_pos = new Vector2(warning_x_pos, warning_y_top_pos);
        warning_down_pos = new Vector2(warning_x_pos, warning_y_down_pos);
        //移動速度設定
        turn = true;
        start_speed = speed;
    }

    public void Update()
    {
        //攻撃のタイムカウント
        stairs_attack_time++;
        range_attack_time++;

        //ボスが生きていたら
        if (health > 0)
        {
            if (turn)
                rb.linearVelocityX = speed;
            else
                rb.linearVelocityX = -speed;

            speed += +boost_speed;

            if ((transform.position.x > right_turn_pos && turn )|| (transform.position.x < left_turn_pos && !turn))
            {
                turn = !turn;
                speed = start_speed;
            }


            //階段攻撃のクールタイムが終わっている場合
            if (stairs_attack_time >= stairs_attack_interval)
            {
                //クールタイムが終わっていた場合
                if (Time.time >= next_stairs_attack_time)
                {
                    Shot(stairs_attack_pos, bullet_vec); //弾を生成
                    next_stairs_attack_time = Time.time + stairs_attack_cooldown; //攻撃のクールタイム
                    stairs_attack_pos.x += stairs_attack_space; //弾の位置をずらす
                    stairs_attack_count++; //攻撃をカウント
                }

                //最大まで攻撃した場合
                if (stairs_attack_count >= stairs_attack_max)
                {
                    stairs_attack_count = 0; //攻撃カウントをリセット
                    stairs_attack_time = 0;  //攻撃パターンをリセット
                    stairs_attack_pos.x = stairs_attack_x; //弾の位置をリセット
                }

            }

            //範囲攻撃のクールタイムが終わっている場合
            if (range_attack_time >= range_attack_interval)
            {
                range_attack_time = 0;
                WarningSpawn();
            }
        }
        else
        {
            rb.linearVelocityX = 0;
            if(gameObject.GetComponent<BossDamageEffect>().alive == true)
                gameObject.GetComponent<BossDamageEffect>().alive = false;
        }
    }

    //ダメージ判定
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(health > 0)
        boss_damage(collision);
    }

    /// <summary>
    /// ボスの階段攻撃用メソッド。階段攻撃の弾の情報と位置を指定します。
    /// </summary>
    /// <param name="sp"> 階段攻撃の弾の座標 </param>
    /// <param name="bv"> 弾の方角 </param>
    private void Shot(Vector2 sp, Vector2 bv)
    {
        int color = Random.Range(0, 2); //弾の色を決める
        var e = Instantiate(bullet, sp, Quaternion.identity).GetComponent<ENormal>(); //弾を生成
        e.Init(bullet_data, bv, color, stairs_attack_speed); //弾の情報を指定
    }

    /// <summary>
    /// ボスの範囲攻撃の警告を表示する用メソッド。 ボスの範囲攻撃前に警告を表示します。
    /// </summary>
    private void WarningSpawn()
    {
        //警告の座標設定
        if (warning_switch)
        {
            warning_save = warning_top_pos; //警告の座標を上に設定
            warning_switch = !warning_switch; 
        }
        else
        {
            warning_save = warning_down_pos; //警告の座標を下に設定
            warning_switch = !warning_switch;
        }
        
        Instantiate(warning, warning_save, Quaternion.identity); //警告を生成
    }
}
