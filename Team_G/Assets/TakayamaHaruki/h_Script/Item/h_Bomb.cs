using System.Net;
using UnityEngine;

public class h_Bomb : MonoBehaviour
{
    public GameObject bomb_gage; //ボムゲージ
    public GameObject bomb; //ボム
    public float bomb_x;  //初期位置
    public float bomb_y;  //初期位置
    public float bomb_space; //ボム間隔

    private Vector2 v;  //ボム座標
    private int bomb_save;  //ボム数保存用
    private GameObject[] bomb_num; //ボムカウント用
    public static h_Bomb Instance { get; private set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //リセット
        bomb_save = 0;
        bomb_y = bomb_y + bomb_gage.transform.position.y;
        bomb_x = bomb_x + bomb_gage.transform.position.x;
        v = new Vector2(bomb_x, bomb_y);
        bomb_num = new GameObject[Player.Instance.max_bom];
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.Instance.health > 0)
        {
            if (Player.Instance.bom != bomb_save)
            {
                //ボム表示リセット
                Delete_Bomb();

                //所持してる分だけボムを表示
                for (int i = 0; i < Player.Instance.bom; i++)
                {
                    bomb_num[i] = Instantiate(bomb, v, Quaternion.identity); //ボムの生成
                    v.x += bomb_space; //ボム同士の間隔を開ける
                }

                v.x = bomb_x; //位置リセット
            }

            bomb_save = Player.Instance.bom; //情報を更新
        }
    }

    //ボムの表示を消す
    public void Delete_Bomb()
    {
        for (int i = 0; i < bomb_num.Length; i++)
        {
            //ボムを消去
            Destroy(bomb_num[i]);
        }
    }
}
