using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class RedFlash : MonoBehaviour
{
    SpriteRenderer img;

    public AudioClip sound1;
    AudioSource audioSource;


    public float flashInTime = 0.05f;   // 白くなるまで
    public float fadeOutTime = 0.9f;    // 消えるまでの時間

    public bool isFlashing = false;

    void Start()
    {
        img = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        FlashAndDisappear();
    }


    /// <summary>
    /// 外部からコルーチンを呼び出す
    /// </summary>
    public void FlashAndDisappear()
    {
        StartCoroutine(FlashCoroutine());
    }

    /// <summary>
    /// 画像を一瞬白くフラッシュさせてから透明にフェードアウトする処理
    /// </summary>
    /// <returns></returns>
    IEnumerator FlashCoroutine()
    {
        isFlashing = true;

        // 即赤くする
        img.color = Color.red;

        // 1フレーム待つ（「一瞬」表示）
        yield return null;

        audioSource.PlayOneShot(sound1);

        float t = 0f;
        Color startColor = img.color;

        while (t < fadeOutTime)
        {
            t += Time.deltaTime;
            float lerp = t / fadeOutTime;

            Color c = startColor;
            c.a = Mathf.Lerp(1f, 0f, lerp);
            img.color = c;

            yield return null;
        }

        img.color = new Color(1, 0, 0, 0); // 赤のまま透明
        isFlashing = false;
    }

}
