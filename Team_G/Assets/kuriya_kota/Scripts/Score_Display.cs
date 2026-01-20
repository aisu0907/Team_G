using TMPro;
using UnityEngine;

public class Score_Display : MonoBehaviour
{
    private TMP_Text scoreText;

    void Start()
    {
        GetComponent<TMP_Text>().enabled=false;
        // TextMeshPro のコンポーネント取得
        scoreText = GetComponent<TMP_Text>();

        if (scoreText == null)
        {
            Debug.LogError("TMP_Text が取得できません。K_Score_Box は TextMeshPro オブジェクトにアタッチしてください。");
        }
    }

    void Update()
    {
        // Score_Receiver の static score をそのまま表示する
        scoreText.text = "SCORE " + Score_Receiver.score.ToString();
    }
}
