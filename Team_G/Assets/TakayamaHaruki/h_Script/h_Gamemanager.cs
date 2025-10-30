using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject boss;     //ボスオブジェクト
    public GameObject player;   //プレイヤーオブジェクト
    public GameObject spawner;  //スポナーオブジェクト
    public GameObject item_drop;//アイテムオブジェクト

    private int frame = 0;  //フレーム

    // Start is called once before the first execution of Update after the MonoBehaviour is create

    private void Start()
    {
        spawner.GetComponent<t_Enemy_Spwan>().spawn_switch = true;     //エネミーの出現をON
        item_drop.GetComponent<Item_Drop>().drop_switch = true;        //アイテムドロップをON
    }

    private void Update()
    {
        //プレイヤーの体力が0以下の場合
        if (player.GetComponent<Player>().Health <= 0)
        {
            //ゲームオーバーシーンに移行
            SceneManager.LoadScene("Gameover_Scene");
            Debug.Log("shinu");
        }

        //フレームカウント
        frame++;

        //30秒経つと起動
        if (frame ==1800)
        {
            spawner.GetComponent<t_Enemy_Spwan>().spawn_switch = false;     //エネミーの出現をOFF
            item_drop.GetComponent<Item_Drop>().drop_switch = false;        //アイテムドロップをOFF
            Instantiate(boss, new Vector2(-2, 3), Quaternion.identity);     //ボスを召喚
        }
    }
}
