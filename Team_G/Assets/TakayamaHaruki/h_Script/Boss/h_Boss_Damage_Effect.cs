using System.Drawing;
using Unity.VisualScripting;
using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public class h_Boss_Damage_Effect : MonoBehaviour
{
    //ゲームオブジェクト
    public GameObject flash;        //フラッシュ
    public GameObject explode;      //爆発演出
    public GameObject boss_explode; //死亡演出

    public float size = 0.0f;    //サイズ
    public float max_size = 0.8f;//最大サイズ
    public float add_size = 0.1f;//追加するサイズ
    //オーディオ関係
    public AudioClip sound1;//サウンド
    public AudioClip sound2;//サウンド

    public bool alive;

    private int timer = 0;
    private AudioSource audioSource;
    void Update()
    {
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
        Instantiate(flash, new Vector2(k_boss.Instance.transform.position.x, k_boss.Instance.transform.position.y), Quaternion.identity);
        Destroy(k_boss.Instance.gameObject);

        for (int i = 0; i < waitFrames2; i++)
        {
            yield return null; // 1フレーム待つ
        }

        //最後のボスを倒したらリザルトに移行
        if (GameManager.Instance.faze >= 5)
            SceneManager.LoadScene("K_Result");
    }
}
