using UnityEngine;

public class h_waring : MonoBehaviour
{
    //ゲームオブジェクト
    public GameObject range_attack; //範囲攻撃
    public SpriteRenderer img; //画像
    public AudioClip warning_sound;  //警告音
    //
    public int warning1; //警告タイミング1
    public int warning2; //警告タイミング2
    public int max_warning; //最大警告回数

    private int warning_time;  //警告時間
    private int warning_count; //警告回数
    private Color save_color; //色を保存
    private Color null_color; //透明色
    private AudioSource audio_source; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //リセット
        warning_count = 0; 
        save_color = img.color;
        null_color = new Color(img.color.g, img.color.b, img.color.r, 0);
        //null_color = new Color(135, 3, 3,img.color.a);
        audio_source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //タイムカウント
        warning_time++;

        if (h_Boss.Instance.health > 0)
        {
            if (warning_time == warning1)
                img.color = null_color; //色を消す
            else if (warning_time >= warning2)
            {
                audio_source.PlayOneShot(warning_sound);
                warning_time = 0; //警告タイマーをリセット
                warning_count++; //警告回数を増やす
                img.color = save_color; //元の色に戻す
            }
        }
        else
        {
            Destroy(gameObject);
        }

        //最大警告まで行った場合
        if (warning_count >= max_warning)
        {
            Instantiate(range_attack, transform.position, Quaternion.identity); //範囲攻撃生成
            Destroy(gameObject); //警告を削除
        }
    }
}
