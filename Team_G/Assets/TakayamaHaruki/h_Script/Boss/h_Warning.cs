using UnityEngine;

public class Warning : MonoBehaviour
{
    //ゲームオブジェクト
    [Header("▼WarningObjectData")]
    public GameObject range_attack;//範囲攻撃
    public AudioClip warning_sound;//警告音
    public SpriteRenderer img;     //画像
    private AudioSource audio_source;//サウンド
    //警告表示
    [Header("▼WarningSetting")]
    public int warning_timing1;//警告タイミング1
    public int warning_timing2;//警告タイミング2
    public int max_warning;    //最大警告回数
    private int warning_count;//警告回数
    private int warning_time; //警告時間
    //色
    private Color default_color;//色を保存
    private Color null_color;   //透明色

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //リセット
        warning_count = 0; 
        default_color = img.color;
        null_color = new Color(img.color.g, img.color.b, img.color.r, 0);
        audio_source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //タイムカウント
        warning_time++;

        //ボスが生きていたら
        if (h_Boss.Instance.health > 0)
        {
            //警告表示が最大回数になるまで表示する
            {
                if (warning_time == warning_timing1)
                    img.color = null_color; //色を消す
                else if (warning_time >= warning_timing2)
                {
                    audio_source.PlayOneShot(warning_sound);
                    warning_time = 0;//警告タイマーをリセット
                    warning_count++; //警告回数を増やす
                    img.color = default_color; //元の色に戻す
                }
            }
        }
        else
        {
            Destroy(gameObject);//ボスが死んだ場合警告表示を消す
        }

        //最大警告まで行った場合
        if (warning_count >= max_warning)
        {
            Instantiate(range_attack, transform.position, Quaternion.identity); //範囲攻撃生成
            Destroy(gameObject); //警告を削除
        }
    }
}
