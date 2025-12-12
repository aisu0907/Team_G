//h_GameManager.cs

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
public class GameManager : MonoBehaviour
{

    //ゲームオブジェクト
    public List<BossSpawnTable> boss; //ボスリスト
    public List<GameObject> item_drop;//アイテムオブジェクト
    public GameObject spawner;  //スポナーオブジェクト
    //リザルト関係
    public GameObject uiPrefab; //簡易リザルト
    public GameObject text;     //簡易リザルトテキスト
    public GameObject dark;     //フェードアウト
    //座標
    public float boss_position_x; //ボス位置X
    public float boss_position_y; //ボス位置Y

    public int faze = 0;    //フェーズ
    public bool boss_die;

    //タイマー
    private float boss_timer = 0;
    public  int frame = 0;  //フレーム
    //座標
    private Vector2 boss_position;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is create

    private void Start()
    {
        // アイテムと敵の出現をONにする
        if(!(DataHolder.game_phaze <= 0))
        {
            faze = DataHolder.game_phaze;
            ModeChange(DataHolder.game_phaze % 2 == 0 ? true : false);
            if(DataHolder.game_phaze % 2 != 0) Instantiate(boss[faze / 2].prefab, boss_position , Quaternion.identity);
        }
        else
            ModeChange(true);
        
        //ボスの初期位置設定
        boss_position = new Vector2 (boss_position_x, boss_position_y);
    }

    private void Update()
    {
        if (Player.Instance.health > 0)
        {
            // プレイヤーの体力が0以下の場合
            //if (Player.Instance.health <= 0)
            //{
            //    // ゲームオーバーシーンに移行
            //    SceneManager.LoadScene("Gameover_Scene");
            //}

            if ((faze + 1) % 2 != 0)
            {
                // フレームカウント
                frame++;

                // 指定フレーム経過するとボスを出現させる
                if (frame == boss[faze / 2].timer) SpawnBoss();
            }

            if ((faze + 1) % 2 == 0)
            {
                boss_timer += Time.deltaTime;
            }
        }
    }

    // ボスを呼び出す関数
    void SpawnBoss()
    {
        ModeChange(false);
        Instantiate(boss[faze / 2].prefab, boss_position, Quaternion.identity);  //ボスを召喚
        faze++;
    }

    // ボスが倒された際に呼び出される関数
    public void KillBoss()
    {
        if (faze >= 5)
            SceneManager.LoadScene("K_Result");
        else
        {    //Destroy(obj);
            Instantiate(dark, uiPrefab.transform);
            var ui = Instantiate(text, uiPrefab.transform).GetComponent<ResultText>();
            ui.init(boss_timer);
        }
    }

    // アイテムと敵の出現を切り替える関数
    public void ModeChange(bool mode)
    {
        for (int i = 0; i < item_drop.Count; i++)
            item_drop[i].GetComponent<Item_Drop>().drop_switch = mode;  //アイテムドロップをOFF
        spawner.GetComponent<t_Enemy_Spwan>().spawn_switch = mode;      //エネミーの出現をOFF
    }
}