using TMPro;
using UnityEngine;

public class HideText : MonoBehaviour
{
    TMP_Text text;
    float speed = 2f;  // 虹色の変化スピード

    void Start()
    {
        text = GetComponent<TMP_Text>();
        text.enabled = false;
    }

    void Update()
    {
        // 0〜1 の値を時間でループさせる
        float h = Mathf.Repeat(Time.time * speed, 1f);

        // HSV（色相）→ RGB に変換
        Color rainbow = Color.HSVToRGB(h, 1f, 1f);

        // テキストに適用
        text.color = rainbow;
    }
}
