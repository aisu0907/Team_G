//Bomb.cs

using UnityEngine;

public class Bomb : MonoBehaviour
{
    //ゲームオブジェクト
    [Header("▼Object Data")]
    public GameObject bomb_gage;//ボムゲージ
    public GameObject bomb;     //ボム
    public GameObject canvas;   //キャンバス
    //ボムの座標
    [Header("▼Bomb Setting")]
    public float bomb_space;//ボム間隔
    public float bomb_pos_x;//初期位置x
    public float bomb_pos_y;//初期位置y

    private Vector2 bomb_pos;//ボム座標
    private int bomb_count;//ボム数保存用
    private GameObject[] bomb_num;//ボムカウント用

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //リセット
        bomb_count = 0; //ボムの数を0に
        bomb_pos = new Vector2(bomb_pos_x, bomb_pos_y);
        bomb_num = new GameObject[Player.Instance.max_bom];
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.Instance.health > 0)
        {
            if (Player.Instance.bom != bomb_count)
            {
                //ボム表示リセット
                DeleteBomb();

                //所持してる分だけボムを表示
                for (int i = 0; i < Player.Instance.bom; i++)
                {
                    bomb_num[i] = (GameObject)Instantiate(bomb);
                    bomb_num[i].transform.SetParent(canvas.transform, false);//ボムの生成
                    
                    //ボムの座標を設定
                    RectTransform rect = bomb_num[i].GetComponent<RectTransform>();
                    rect.anchoredPosition = bomb_pos;

                    bomb_pos.x += bomb_space; //ボム同士の間隔を開ける
                }

                bomb_pos.x = bomb_pos_x; //位置リセット
            }

            bomb_count = Player.Instance.bom; //情報を更新
        }
    }

    /// <summary>
    /// ボム表示を消す用のメソッド。 表示されているボムの回数分消します。
    /// </summary>
    public void DeleteBomb()
    {
        for (int i = 0; i < bomb_num.Length; i++)
        {
            //ボムを消去
            Destroy(bomb_num[i]);
        }
    }
}
