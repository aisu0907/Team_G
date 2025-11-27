using UnityEngine;

public class h_waring : MonoBehaviour
{
    public GameObject range_attack; //範囲攻撃
    public SpriteRenderer img; //画像
    public int waring1; //警告タイミング1
    public int waring2; //警告タイミング2
    public int max_warnig; //最大警告回数

    private int waring_time; //警告時間
    private int waring_count; //警告回数
    private Color save_color; //色を保存
    private Color null_color; //透明色

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //リセット
        waring_count = 0; 
        save_color = img.color;
        null_color = new Color(img.color.g, img.color.b, img.color.r, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //タイムカウント
        waring_time++;

        if (waring_time == waring1)
            img.color = null_color; //色を消す
        else if (waring_time >= waring2)
        {
            waring_time = 0; //警告タイマーをリセット
            waring_count++; //警告回数を増やす
            img.color = save_color; //元の色に戻す
        }

        //最大警告まで行った場合
        if(waring_count >= max_warnig)
        {
            Instantiate(range_attack, transform.position, Quaternion.identity); //範囲攻撃生成
            Destroy(gameObject); //自分を削除
        }
    }
}
