using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public class Boss_Damage_Effect : MonoBehaviour
{
    //ゲームオブジェクト
    [Header("▼EffectObjectData")]
    public GameObject flash;  //フラッシュ
    public GameObject explode;//爆発演出
    public SpriteRenderer img;//画像
    //オーディオ関係
    [Header("▼SoundEffect")]
    public AudioClip sound1;//サウンド
    public AudioClip sound2;//サウンド
    private AudioSource audio_source;//オーディオソース
    //ダメージエフェクト
    [Header("▼DamageEffect")]
    public int blinks_max  = 4; //点滅する回数
    public int damage_time = 10;//ダメージエフェクトタイミング
    public int save_time   = 20;//表示タイム
    //フラグ
    public bool alive;//生存判定
    public bool damage_hit;//ダメージ判定
    //ダメージエフェクト
    private Color default_color;//通常の色
    private Color damage_color; //ダメージ時の色
    private int color_timer;    //色切り替えタイマー
    private int color_count;    //色切り替え回数
   
    private int timer = 0;//演出用タイマー

    private void Start()
    {
        //リセット
        //フラグリセット
        damage_hit = false;
        alive = true;
        //カラーリセット
        default_color = img.color;
        damage_color = new Color(200, 40, 40, 1);
        //オーディオソースをアタッチ
        audio_source = gameObject.GetComponent<AudioSource>();
    }
    void Update()
    {
        //ダメージを受けた時
        if (damage_hit)
        {
            //タイムカウント
            color_timer++;

            //0.2秒毎にボスの色を変える
            {
                if (color_timer == damage_time)
                {
                    img.color = damage_color; //ダメージ時の色に変更
                    color_count++; //色切り替えカウント
                }

                if (color_timer >= save_time)
                {
                    img.color = default_color; //通常の色に変更
                    color_count++;   //色切り替えカウント
                    color_timer = 0; //タイマーリセット
                }
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

        //ボスが死んでいたら
        if (!alive)
        {
            //残っているボスの攻撃を削除
            if(timer == 0)
            {
        
                //"Enemy"タグがついたすべてのオブジェクトを取得
                GameObject[] objects = GameObject.FindGameObjectsWithTag("Enemy");
                //各エネミーを削除
                foreach (GameObject obj in objects)
                {
                    Destroy(obj);
                    Instantiate(explode, obj.transform.position, Quaternion.identity); //削除したエネミーの位置に死亡エフェクトを生成
                }
            }

            timer++;

            //爆発エフェクトを生成
            if (timer == 10) RandomPos();
            if (timer == 30) RandomPos();
            if (timer == 40) RandomPos();
            if (timer == 50) RandomPos();
            if (timer == 80) RandomPos();
            if (timer == 110) RandomPos();
            if (timer == 120) RandomPos();
            if (timer == 180)
            {
                audio_source.PlayOneShot(sound1); //SEを再生
            }
            if (timer == 220)
            {
                Score_Manager.Instance.score_switch = true; //スコアが入るようにする
                StartCoroutine(DelayedFlash());
                alive = true; //
            }
        }
    }

    /// <summary>
    /// 爆発エフェクトを生成するメソッド。 爆発エフェクトの生成する位置をランダムに決めて生成します。
    /// </summary>
    private void RandomPos()
    {
        int x = Random.Range(-3, 4); //x位置をランダムに決める
        int y = Random.Range(-3, 4); //y位置をランダムに決める
        Instantiate(explode, new Vector2(transform.position.x + x,transform.position.y + y), Quaternion.identity); //爆発エフェクトを生成
    }


    /// <summary>
    /// ボス死亡後の演出用メソッド。 ボス死亡時にコルーチンを使用して演出を生成します。
    /// </summary>
    /// <returns></returns>
    private IEnumerator DelayedFlash()
    {
        int waitFrames1 = 60; // 待ちたいフレーム数

        for (int i = 0; i < waitFrames1; i++)
        {
            yield return null; // 1フレーム待つ
        }
        audio_source.PlayOneShot(sound2); //SEをを再生
        Instantiate(flash, new Vector2(transform.position.x,transform.position.y), Quaternion.identity); //画面全体にフラッシュを生成
        GameManager.Instance.boss_die = false;  
        Destroy(gameObject);
    }
}
