using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public class Boss_Damage_Effect : MonoBehaviour
{
    //ゲームオブジェクト
    public GameObject flash;   //フラッシュ
    public GameObject explode; //爆発演出
    public SpriteRenderer img; //画像
    //オーディオ関係
    public AudioClip sound1;//サウンド
    public AudioClip sound2;//サウンド
    //ダメージエフェクト
    public int blinks_max  = 4; //点滅する回数
    public int damage_time = 10;//ダメージエフェクトタイミング
    public int save_time   = 20;//表示タイム
    //フラグ
    public bool alive; //生存判定
    public bool damage_hit;//ダメージ判定
    //死亡時エフェクト関係
    private Vector2 size;    //サイズ
    private float max_size_x;//最大サイズX
    private float max_size_y;//最大サイズY
    private float add_size_x;//追加するサイズX
    private float add_size_y;//追加するサイズY
    //ダメージエフェクト
    private Color save_color;   //通常の色
    private Color damage_color; //ダメージ時の色
    private int color_timer;    //色切り替えタイマー
    private int color_count;    //色切り替え回数

    private int timer = 0;
    private AudioSource audio_source;

    private void Start()
    {
        damage_hit = false;
        alive = false;
        save_color = img.color;
        damage_color = new Color(200, 40, 40, 1);
        audio_source = gameObject.GetComponent<AudioSource>();
        //サイズ関係
        //max_size_x = transform.localScale.x;
        //max_size_y = transform.localScale.y;
        //add_size_x = (transform.localScale.x / 30);
        //add_size_y = (transform.localScale.y / 30);
        //size = new Vector2(0, 0);
    }
    void Update()
    {
        //ダメージ演出
        if (damage_hit)
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
                damage_hit = false;
            }
        }

        //死亡演出
        if (alive)
        {
            if(timer == 0)
            {
        
                // "Enemy"タグがついたすべてのオブジェクトを取得
                GameObject[] objects = GameObject.FindGameObjectsWithTag("Enemy");
                // 各オブジェクトを削除
                foreach (GameObject obj in objects)
                {
                    Destroy(obj);
                    Instantiate(explode, obj.transform.position, Quaternion.identity);
                }
            }
            timer++;

            //if (size.x <= max_size_x && size.y <= max_size_y)
            //{
            //    size.x += add_size_x;
            //    size.y += add_size_y;
            //    transform.localScale = size;
            //}

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
                audio_source.PlayOneShot(sound1);//SEを再生
            }
            if (timer == 220)
            {
                Score_Manager.Instance.score_switch = true;
                StartCoroutine(DelayedFlash());
                alive = false;
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

        for (int i = 0; i < waitFrames1; i++)
        {
            yield return null; // 1フレーム待つ
        }
        audio_source.PlayOneShot(sound2);
        Instantiate(flash, new Vector2(transform.position.x,transform.position.y), Quaternion.identity);
        GameManager.Instance.boss_die = false;
        Destroy(gameObject);
    }
}
