using UnityEngine;
using UnityEngine.UI;

public class ScreenFlash : MonoBehaviour
{
    public Image flashImage;         // 白いImageをここに入れる
    public float flashInSpeed = 0.2f;  // フラッシュが明るくなる速さ
    public float flashOutSpeed = 1.0f; // 元に戻る速さ（ゆっくり）
    public float maxAlpha = 1f;        // 最大の明るさ

    private bool isFlashing = false;

    public void Flash()
    {
        if (!isFlashing)
            StartCoroutine(DoFlash());
    }

    private System.Collections.IEnumerator DoFlash()
    {
        isFlashing = true;

        // フェードイン（明るく）
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime / flashInSpeed;
            SetAlpha(Mathf.Lerp(0, maxAlpha, t));
            yield return null;
        }

        // フェードアウト（ゆっくり暗く）
        t = 0;
        while (t < 1)
        {
            t += Time.deltaTime / flashOutSpeed;
            SetAlpha(Mathf.Lerp(maxAlpha, 0, t));
            yield return null;
        }

        isFlashing = false;
    }

    private void SetAlpha(float a)
    {
        var c = flashImage.color;
        c.a = a;
        flashImage.color = c;
    }
}
