 //using UnityEngine;
//using UnityEngine.UI;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossExplode : MonoBehaviour
{
    public float size = 0.0f;
    public float max_size = 0.8f;
    public float add_size = 0.1f;

    public int timer = 0;

    public AudioClip sound1;
    public AudioClip sound2;

    public GameObject explode;
    public GameObject flash;

    AudioSource audioSource;



    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        timer++;
        if (size <= max_size) size += add_size;
        transform.localScale = new Vector2(size, size);



        if (timer == 10) Random_Explode();
        if (timer == 30) Random_Explode();
        if (timer == 40)  Random_Explode();
        if (timer == 50)  Random_Explode();
        if (timer == 80)  Random_Explode();
        if (timer == 110) Random_Explode();
        if (timer == 120) Random_Explode();
        if (timer == 180)
        {
            audioSource.PlayOneShot(sound1);
        }
        if(timer ==220) StartCoroutine(DelayedFlash());

        //シーン見込み予定
        if (timer > 400) Destroy(gameObject);

    }
    /// <summary>
    /// 爆破位置のXとYを-3から4のランダムな値で決めて、その部分に爆発演出を生成
    /// </summary>
    private void Random_Explode()
    {
        int x = Random.Range(-3, 4);
        int y = Random.Range(-3, 4);
        Instantiate(explode, new Vector2(LastBoss.Instance.transform.position.x + x, LastBoss.Instance.transform.position.y + y), Quaternion.identity);
    }

    /// <summary>
    /// 爆発演出待機後、画面をフラッシュさせてフェイズが5以上ならリザルト画面に移行する
    /// </summary>
    /// <returns></returns>
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
        Instantiate(flash, new Vector2(LastBoss.Instance.transform.position.x, LastBoss.Instance.transform.position.y), Quaternion.identity);
        Destroy(LastBoss.Instance.gameObject);

        for (int i = 0; i < waitFrames2; i++)
        {
            yield return null; // 1フレーム待つ
        }
    }
}
