//h_GameManager.cs

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
public class GameManager : MonoBehaviour
{

    public List<BossSpawnTable> boss;
    public GameObject spawner;  //スポナーオブジェクト
    public List<GameObject> item_drop;//アイテムオブジェクト

    private int frame = 0;  //フレーム
    public int faze = 0;    //フェーズ

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is create

    private void Start()
    {
        spawner.GetComponent<t_Enemy_Spwan>().spawn_switch = true;      //エネミーの出現をOFF
        for (int i = 0; i < item_drop.Count; i++)
            item_drop[i].GetComponent<Item_Drop>().drop_switch = true;  //アイテムドロップをOFF
        SpawnBoss();
    }

    private void Update()
    {
        //プレイヤーの体力が0以下の場合
        if (Player.Instance.health <= 0)
        {
            //ゲームオーバーシーンに移行
            SceneManager.LoadScene("Gameover_Scene");
        }

        if ((faze + 1) % 2 != 0)
        {
            //フレームカウント
            frame++;

            //30秒経つと起動
            if (frame == boss[faze / 2].timer) SpawnBoss();
        }
    }

    void SpawnBoss()
    {
        ModeChange(false);
        Instantiate(boss[faze / 2].prefab, new Vector2(-2, 3), Quaternion.identity);  //ボスを召喚
    }

    public void KillBoss(GameObject obj)
    {
        Destroy(obj);
        ModeChange(true);
        frame = 0;
    }

    void ModeChange(bool mode)
    {
        spawner.GetComponent<t_Enemy_Spwan>().spawn_switch = mode;      //エネミーの出現をOFF
        for (int i = 0; i < item_drop.Count; i++)
            item_drop[i].GetComponent<Item_Drop>().drop_switch = mode;  //アイテムドロップをOFF
        faze++;
    }
}