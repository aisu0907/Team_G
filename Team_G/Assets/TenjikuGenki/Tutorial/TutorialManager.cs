using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour, IPhazeManager
{
    public int phase { get; set; } = 0;
    public bool is_change_color { get; set; } = false;
    //ゲームオブジェクト
    [SerializeField] public GameObject window;//表示するウィンドウ
    [SerializeField] List<Sprite> window_img; //ウィンドウに表示する画像
    public GameObject enemy;//出現させる敵

    public int enemy_speed;//出現させる敵のスピード
    public int enemy_hit_count;//チュートリアルクリア条件カウント
    public int enemy_hit; //チュートリアルクリア条件
    public Vector2 enemy_spawn_pos;//敵の出現位置

    public int pop_time;   //敵の出現頻度
    public int key_time;   //キーを受け付けない時間
    private bool is_window = true;//ウィンドウ管理フラグ

    //タイムカウント
    [SerializeField] private int pop_time_count;
    [SerializeField] private int key_time_count;

    private Image img;      
    private bool pop_window;//ポップ表示確認用フラグ
    private bool bomb_pop;  //ボム説明ポップ確認用フラグ
    private bool hp_pop;　　//ダメージポップ確認用フラグ
    private int enemy_color;//敵の色
    private int save_hp;　  //プレイヤーHP
    private int pop_id;     //ポップID
    private bool key_switch;//キーを押してるか確認用フラグ
    private bool start_window;//最初にウィンドウを表示する用フラグ

    public static TutorialManager Instance { get; private set; }

    private void Awake()
    {
        //シングルトン
        Instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //リセット
        img = window.GetComponent<Image>();
        img.sprite = window_img[0];
        window.SetActive(false);
        phase = 6;
        enemy_color = 0;
        pop_id = 1;
        save_hp = Player.Instance.health;
        //フラグ
        hp_pop = true;
        bomb_pop = true;
        start_window = true;
        //タイム
        key_time_count = 0;
        pop_time_count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //ウィンドウが表示されていたら
        if (pop_window)
        {
            key_time_count++; //キー入力を受け付けない時間をカウント
        }

        //Enterでチュートリアルをスキップ
        if (Input.GetKeyDown(KeyCode.Return))
            SceneManager.LoadScene("PlayScene");

        //プレイヤーの登場演出が終わったら
        if (!Player.Instance.start_anime && start_window)
        {
            //基礎説明を表示
            window.SetActive(true);
            pop_window = true;
            start_window = false;
        }

        //ウィンドウが表示されていた場合Zキーで進む
        if (Input.GetKeyDown(KeyCode.Z) && pop_window)
        {
            //最後のウィンドウの場合
            if (!is_window && key_switch && key_time < key_time_count)
            {
                window.SetActive(false); //ウィンドウを非表示
                pop_window = false;      //ウィンドウが表示されている状態にする
                is_change_color = true;  //盾の色を変えれるようにする
                //ウィンドウをすべて表示していたら
                if((!hp_pop && !bomb_pop) || (hp_pop && bomb_pop))
                    //チュートリアルを次に進める
                    phase++;

                key_time_count = 0; //キー入力時間をリセット
            }

            //ウィンドウに続きがある場合
            if (is_window && key_switch && key_time < key_time_count)
            {
                img.sprite = window_img[pop_id]; //ウィンドウを表示
                is_window = false;

                key_time_count = 0; //キー入力時間をリセット
            }

            key_switch = false; //キーが押されている
        }
        else
            key_switch = true; //キーが押されていない

        //ウィンドウが非表示の時
        if (phase >= 6 && !pop_window)
        {
            pop_time_count++; //敵の出現時間カウント

            //敵の出現時間になったら
            if (pop_time <= pop_time_count)
            {
                var e = Instantiate(enemy, enemy_spawn_pos, Quaternion.identity).GetComponent<TutorialEnemy>(); //敵を生成
                e.Init(new Vector2(0, -1), enemy_color, enemy_speed); //敵の情報を設定

                //敵の色を毎回変える
                {
                    //敵の色が赤だったら
                    if (enemy_color == 0)
                        enemy_color++;
                    //敵の色が緑だったら
                    else
                        enemy_color--;
                }

                //敵の出現時間をリセット
                pop_time_count = 0;
            }
        }

        //プレイヤーがダメージ受けるかチュートリアルをクリアした時
        if (Player.Instance.health != save_hp || enemy_hit_count >= enemy_hit)
        {
            //ダメージ説明がまだ出ていなかったら
            if (hp_pop)
            {
                img.sprite = window_img[3]; //画像を入れる
                hp_pop = false; 
                is_change_color = false; //盾の色を変えれないように
                is_window = true; 
                window.SetActive(true); //ウィンドを表示
                pop_window = true;  //ウィンドを表示した状態に

                if (bomb_pop && enemy_hit_count >= 3)
                {
                    pop_id = 2;
                    bomb_pop = false;
                }
                else
                    is_window = false;
            }
            else if(bomb_pop && enemy_hit_count >= enemy_hit)
            {
                bomb_pop = false;
                img.sprite = window_img[2];
                window.SetActive(true);
                pop_window = true;
            }

            //プレイヤーの体力を回復
            Player.Instance.health = 3;
        }
    }
}
