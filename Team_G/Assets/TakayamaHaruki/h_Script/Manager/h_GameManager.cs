//h_GameManager.cs

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
public class GameManager : MonoBehaviour
{

    public List<BossSpawnTable> boss;
    public GameObject spawner;  //スポナーオブジェクト
    public List<GameObject> item_drop;//アイテムオブジェクト


    public int frame = 0;  //フレーム
    public int faze = 0;    //フェーズ

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
            if(DataHolder.game_phaze % 2 != 0) Instantiate(boss[faze / 2].prefab, new Vector2(-2, 3), Quaternion.identity);
        }
        else
            ModeChange(true);
    }

    private void Update()
    { 
        // プレイヤーの体力が0以下の場合
        if (Player.Instance.health <= 0)
        {
            // ゲームオーバーシーンに移行
            SceneManager.LoadScene("Gameover_Scene");
        }

        if ((faze + 1) % 2 != 0)
        {
            // フレームカウント
            frame++;

            // 指定フレーム経過するとボスを出現させる
            if (frame == boss[faze / 2].timer) SpawnBoss();
        }
    }

    // ボスを呼び出す関数
    void SpawnBoss()
    {
        ModeChange(false);
        Instantiate(boss[faze / 2].prefab, new Vector2(-2, 3), Quaternion.identity);  //ボスを召喚
        faze++;
    }

    // ボスが倒された際に呼び出される関数
    public void KillBoss(GameObject obj)
    {
        Destroy(obj);
        ModeChange(true);
        frame = 0;
        faze++;
    }

    // アイテムと敵の出現を切り替える関数
    void ModeChange(bool mode)
    {
        spawner.GetComponent<t_Enemy_Spwan>().spawn_switch = mode;      //エネミーの出現をOFF
        for (int i = 0; i < item_drop.Count; i++)
            item_drop[i].GetComponent<Item_Drop>().drop_switch = mode;  //アイテムドロップをOFF
    }
}