using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Const;
using System.Runtime.CompilerServices;
using UnityEngine.InputSystem;

public class TutorialManager : MonoBehaviour, IPhazeManager
{
    [SerializeField] StartAnimation sa;

    public int phase { get; set; } = 6;

    //ゲームオブジェクト
    [SerializeField] public GameObject window;//表示するウィンドウ
    [SerializeField] List<Sprite> window_img; //ウィンドウに表示する画像
    public GameObject enemy;//出現させる敵

    public int enemy_speed;//出現させる敵のスピード
    public int enemy_hit_count;//チュートリアルクリア条件カウント
    public int enemy_hit; //チュートリアルクリア条件
    public Vector2 right_enemy_spawn_pos;//敵の右出現位置
    public Vector2 left_enemy_spawn_pos; //敵の左出現位置
    public Vector2 mid_enemy_spawn_pos;  //敵の真ん中出現位置

    [HideInInspector]
    public bool damage_help = false;

    public float pop_time; //敵の出現頻度
    public float next_pop_time; //
    public float key_time; //キーを受け付けない時間
    private bool is_window = true;//ウィンドウ管理フラグ

    [Range(0f, 1f)]
    public float bgm_vlome; //BGM音量

    //タイムカウント
    private float pop_time_count = 0;
    private float key_time_count = 0;

    private Image img;
    private h_AudioManager tutorial_audio;
    private Vector2 enemy_spawn_pos;//敵の出現位置
    private int save_hp;　  //プレイヤーHP
    private float enemy_pop_count = 0;   //敵の出現した回数
    private int enemy_color = 0;//敵の色
    private int pop_id = 1;     //ポップID
    private bool pop_window = false;//ポップ表示確認用フラグ
    private bool bomb_pop = true;  //ボム説明ポップ確認用フラグ
    private bool hp_pop = true;　　//ダメージポップ確認用フラグ
    private bool lane   = true;      //敵の出現位置切り替えようフラグ
    private bool key_switch ;//キーを押してるか確認用フラグ
    private bool start_window = true;//最初にウィンドウを表示する用フラグ

    public static TutorialManager Instance { get; private set; }

    private void Awake()
    {
        //シングルトン
        Instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tutorial_audio = h_AudioManager.Instance;

        //リセット
        img = window.GetComponent<Image>();
        img.sprite = window_img[0];

        window.SetActive(false);
        save_hp = Player.Instance.health;
        enemy_spawn_pos = right_enemy_spawn_pos;

        tutorial_audio.PlayBGM(AudioConst.BGM_ID.TUTRIAL_BGM, bgm_vlome); 
    }

    // Update is called once per frame
    void Update()
    {
        //ウィンドウが表示されていたら
        if (pop_window)
        {
            Player.Instance.player_inoperative = true;
            Shield.Instance.shield_inoperative = true;
            key_time_count += Time.deltaTime; //キー入力を受け付けない時間をカウント
        }
        else
        {
            Player.Instance.player_inoperative = false;
            Shield.Instance.shield_inoperative = false;
        }

        //Enterでチュートリアルをスキップ
        //if (Input.GetKeyDown(KeyCode.Return))
        //{
        //    SceneManager.LoadScene("PlayScene");
        //}
        //プレイヤーの登場演出が終わったら
        if (sa.StartAnime() && start_window)
        {
            //基礎説明を表示
            window.SetActive(true);
            pop_window = true;
            start_window = false;
        }

        //ウィンドウが表示されていた場合Zキーで進む
        //if (Input.GetKeyDown(KeyCode.Z) && pop_window)
        //{
        //    //最後のウィンドウの場合
        //    if (!is_window && key_switch && key_time < key_time_count)
        //    {
        //        window.SetActive(false); //ウィンドウを非表示
        //        pop_window = false;      //ウィンドウが表示されている状態にする

        //        //ウィンドウをすべて表示していたら
        //        if((!hp_pop && !bomb_pop) || (hp_pop && bomb_pop))
        //            //チュートリアルを次に進める
        //            phase++;

        //        key_time_count = 0; //キー入力時間をリセット
        //    }

        //    //ウィンドウに続きがある場合
        //    if (is_window && key_switch && key_time < key_time_count)
        //    {
        //        img.sprite = window_img[pop_id]; //ウィンドウを表示
        //        is_window = false;

        //        key_time_count = 0; //キー入力時間をリセット
        //    }

        //    key_switch = false; //キーが押されている
        //}
        //else
        //    key_switch = true; //キーが押されていない

        //ウィンドウが非表示の時
        if (phase >= 6 && !pop_window)
        {
            pop_time_count += Time.deltaTime; //敵の出現時間カウント

            //敵の出現時間になったら
            if (pop_time_count >= pop_time)
            {
                if (phase == 7)
                {
                    //敵が2回出現したら位置を変える
                    if (enemy_pop_count >= 2)
                    {
                        if (!lane)
                        {
                            enemy_spawn_pos = left_enemy_spawn_pos;
                            enemy_color = 1;
                        }
                        else
                        {
                            enemy_spawn_pos = right_enemy_spawn_pos;
                            enemy_color = 0;
                        }

                        lane = !lane;
                        enemy_pop_count = 0;
                    }

                    Enemyspawn(enemy_spawn_pos);

                    Enemycolorchange();

                    pop_time_count = 0;//敵の出現時間をリセット
                }

                if (phase >= 8)
                {
                    if (enemy_pop_count == 0)
                    {
                        Enemyspawn(mid_enemy_spawn_pos);

                        Enemycolorchange();
                    }
                    else if (pop_time_count >= next_pop_time)
                    {
                        Enemyspawn(right_enemy_spawn_pos);
                        Enemyspawn(left_enemy_spawn_pos);
                        pop_time_count = 0;//敵の出現時間をリセット
                        enemy_pop_count = 0; //敵の数をリセット
                        
                        Enemycolorchange();
                    }

                }

            }
        }

        //プレイヤーがダメージ受けるかチュートリアルをクリアした時
        if (damage_help || enemy_hit_count >= enemy_hit)
        {
            //ダメージ説明がまだ出ていなかったら
            if (hp_pop)
            {
                img.sprite = window_img[3]; //画像を入れる
                hp_pop = false; 
                is_window = true; 
                window.SetActive(true); //ウィンドを表示
                pop_window = true;  //ウィンドを表示した状態に

                //チュートリアルをクリアしていたら
                if (bomb_pop && enemy_hit_count >= 3)
                {
                    pop_id = 2; 
                    bomb_pop = false;
                    pop_time_count = 0;
                    enemy_pop_count = 0;
                }
                else
                    is_window = false;
            }
            //ボム説明が出ていなくてかつチュートリアルをクリアしていたら
            else if(bomb_pop && enemy_hit_count >= enemy_hit)
            {
                bomb_pop = false;
                img.sprite = window_img[2];
                window.SetActive(true);
                pop_window = true;
                pop_time_count = 0;
                enemy_pop_count = 0;
            }

            //プレイヤーの体力を回復
            Player.Instance.health = save_hp;
        }
    }

    /// <summary>
    /// エネミー召喚用メソッド
    /// </summary>
    /// <param name="pos">出現位置</param>
    private void Enemyspawn(Vector2 pos)
    {
        enemy_spawn_pos = pos;//位置を設定
        var e = Instantiate(enemy, enemy_spawn_pos, Quaternion.identity).GetComponent<TutorialEnemy>(); //敵を生成
        e.Init(new Vector2(0, -1), (COLOR)enemy_color, enemy_speed); //敵の情報を設定
        enemy_pop_count++;//敵の出現数をカウント
    }

    /// <summary>
    /// 敵の色変更用メソッド
    /// </summary>
    private void Enemycolorchange()
    {
        //敵の色が赤だったら
        if (enemy_color == 0)
            enemy_color++;
        //敵の色が緑だったら
        else
            enemy_color--;
    }

    public void Interact(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;

        //ウィンドウが表示されていた場合Zキーで進む
        if (pop_window)
        {
            //最後のウィンドウの場合
            if (!is_window && key_time < key_time_count)
            {
                window.SetActive(false); //ウィンドウを非表示
                pop_window = false;      //ウィンドウが表示されている状態にする

                //ウィンドウをすべて表示していたら
                if ((!hp_pop && !bomb_pop) || (hp_pop && bomb_pop))
                    //チュートリアルを次に進める
                    phase++;

                key_time_count = 0; //キー入力時間をリセット
            }

            //ウィンドウに続きがある場合
            if (is_window && key_time < key_time_count)
            {
                img.sprite = window_img[pop_id]; //ウィンドウを表示
                is_window = false;

                key_time_count = 0; //キー入力時間をリセット
            }
        }
    }
}
