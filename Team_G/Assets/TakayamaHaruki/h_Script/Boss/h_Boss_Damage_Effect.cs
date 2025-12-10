using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public class Boss_Damage_Effect : MonoBehaviour
{
    //ゲームオブジェクト
    public GameObject flash;        //フラッシュ
    public GameObject explode;      //爆発演出
    public GameObject boss_explode; //死亡演出

    public SpriteRenderer img; //画像
 
    public float size = 0.0f;    //サイズ
    public float max_size = 3.0f;//最大サイズ
    public float add_size = 0.5f;//追加するサイズ
    public bool alive;
    //オーディオ関係
    public AudioClip sound1;//サウンド
    public AudioClip sound2;//サウンド

    //ダメージエフェクト
    public bool damage_hit;
    public int blinks_max; //点滅する回数
    public int damage_time;  //消滅タイミング
    public int save_time;  //表示タイム
    private Color save_color;   //通常の色
    private Color damage_color; //ダメージ時の色
    private int color_timer;    //色切り替えタイマー
    private int color_count;    //色切り替え回数

    private int timer = 0;
    private AudioSource audioSource;

    private void Start()
    {
        save_color = img.color;
        damage_color = new Color(200, 40, 40, 1);
    }
    void Update()
    {
        if (!damage_hit)
        {
            color_timer++;

            if (color_timer == damage_time)
            {
                img.color = damage_color;//ダメージ時の色に変更
                color_count++;
            }

            if (color_timer >= save_time)
            {
                img.color = save_color;//通常の色に変更
                color_count++;
                color_timer = 0;//タイマーリセット
            }

            //色切り替え回数が最大回数に達したら
            if (color_count >= blinks_max)
            {
                //リセット
                color_timer = 0;
                color_count = 0;
                damage_hit = true;
            }
        }

        if (!alive)
        {
            timer++;
            if (size <= max_size) size += add_size;
            transform.localScale = new Vector2(size, size);
            //爆発エフェクトを生成
            if (timer == 10) random();
            if (timer == 30) random();
            if (timer == 40) random();
            if (timer == 50) random();
            if (timer == 80) random();
            if (timer == 110) random();
            if (timer == 120) random();
            if (timer == 180)
            {
                audioSource.PlayOneShot(sound1);//SEを再生
            }
            if (timer == 220) StartCoroutine(DelayedFlash());

            //シーン見込み予定
            if (timer > 400)
            {
                Destroy(gameObject);
            }
        }
    }

    private void random()
    {
        int x = Random.Range(-3, 4);
        int y = Random.Range(-3, 4);
        Instantiate(explode, new Vector2(transform.position.x + x,transform.position.y + y), Quaternion.identity);
    }


    private IEnumerator DelayedFlash()
    {
        int waitFrames1 = 60; // 待ちたいフレーム数

        int waitFrames2 = 120;

        for (int i = 0; i < waitFrames1; i++)
        {
            yield return null; // 1フレーム待つ
        }
        audioSource.PlayOneShot(sound2);
        //screenFlash.Flash();
        Instantiate(flash, new Vector2(transform.position.x,transform.position.y), Quaternion.identity);
        Destroy(gameObject);

        for (int i = 0; i < waitFrames2; i++)
        {
            yield return null; // 1フレーム待つ
        }

        //最後のボスを倒したらリザルトに移行
        if (GameManager.Instance.faze >= 5)
            SceneManager.LoadScene("K_Result");
    }
}
