//h_GameManager.cs

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
public class GameManager : MonoBehaviour, IPhazeManager
{

    //ゲームオブジェクト
    [Header("▼Object Data")]
    public GameObject spawner;//スポナーオブジェクト
    public List<BossSpawnTable> boss; //ボスリスト
    public List<GameObject> item_drop;//アイテムオブジェクト
    //リザルト関係
    public GameObject uiprefab;//簡易リザルト
    public GameObject text;    //簡易リザルトテキスト
    public GameObject dark;    //フェードアウト
    //座標
    [Header("▼Boss Start Posion")]
    public Vector2 boss_position;

    //
    [Header("▼Game Manager Setting")]

    public bool boss_die = true;//ボス死亡判定

    public int phase { get; set; } = 0;//フェーズ

    public int set_phase_num; //フェーズ設定

    //タイマー
    public  float result_time = 0;
    public  float spawn_time = 0;
    private float boss_timer = 0;
    private float result_timer = 0;//リザルト
    private float spawn_timer = 0;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        //シングルトン
        Instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is create
    private void Start()
    {
        phase = set_phase_num;

        // アイテムと敵の出現をONにする
        if(!(DataHolder.game_phaze <= 0))
        {
            phase = DataHolder.game_phaze;
            ModeChange(DataHolder.game_phaze % 2 == 0 ? true : false);
            if(DataHolder.game_phaze % 2 != 0) Instantiate(boss[phase / 2].prefab, boss_position , Quaternion.identity);
        }
        else
            ModeChange(true);
    }

    private void Update()
    {
        //プレイヤーが生きている場合
        if (Player.Instance.health > 0)
        {
            if ((phase + 1) % 2 != 0)
            {
                //フレームカウント
                boss_timer += Time.deltaTime;

                //指定フレーム経過するとボスを出現させる
                if (boss_timer >= boss[phase / 2].timer)
                {
                    spawner.GetComponent<EnemySpawn>().spawn_switch = false;

                    //画面にエネミーが残っていない場合
                    if (EnemySpawn.Instance.counter == 0)
                    {
                        spawn_timer += Time.deltaTime;
                        if(spawn_timer >= spawn_time)
                        {
                            SpawnBoss(); //ボスを出現
                            boss_timer = 0; //フレームをリセット
                            spawn_timer = 0;
                        }
                    }
                }
            }
            
            //ボスが倒されたら
            if(!boss_die)
            {
                result_timer += Time.deltaTime;

                if(result_timer >= result_time)
                {
                    Result();
                    boss_die = true;
                    result_timer = 0;
                }
            }
        }
    }

    /// <summary>
    /// ボス出現用メソッド。ボスを出現させ、スコアの取得モードの変更、フェーズ進行を行います。
    /// </summary>
    void SpawnBoss()
    {
        ModeChange(false);
        Instantiate(boss[phase / 2].prefab, boss_position, Quaternion.identity); //ボスを召喚
        phase++; //フェーズを進める
        DataHolder.GetGameData(); //データを保存
    }

    /// <summary>
    /// リザルト表示用メソッド。 ボス撃破後にゲームリザルかミニリザルトを表示します
    /// </summary>
    public void Result()
    {
        //ラスボス撃破後の場合ゲームリザルトに
        if (phase >= 5)
            SceneManager.LoadScene("ResultScene");
            //道中ボスの場合ミニリザルトに
        else
        {
            Instantiate(dark, uiprefab.transform);
            var ui = Instantiate(text, uiprefab.transform).GetComponent<ResultText>();
            ui.init(boss_timer);
        }
    }

    /// <summary>
    /// 出現、取得を切り替える用のメソッド。アイテム、エネミー、スコアの出現、取得を切り替えます。 
    /// </summary>
    /// <param name="mode">trueの場合on falseの場合off</param>
    public void ModeChange(bool mode)
    {
        for (int i = 0; i < item_drop.Count; i++)
            item_drop[i].GetComponent<ItemDrop>().drop_switch = mode;  //アイテムドロップ
        spawner.GetComponent<EnemySpawn>().spawn_switch = mode;     //エネミーの出現
        gameObject.GetComponent<ScoreManager>().score_switch = mode;  //スコア取得     
    }
}