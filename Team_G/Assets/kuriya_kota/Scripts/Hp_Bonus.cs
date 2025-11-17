using TMPro;
using UnityEngine;

public class Hp_Bonus : MonoBehaviour
{
    private TMP_Text hpText;

    void Start()
    {
        GetComponent<TMP_Text>().enabled = false;
        // TextMeshPro のコンポーネント取得
        hpText = GetComponent<TMP_Text>();

        if (hpText == null)
        {
            Debug.LogError("TMP_Text が取得できません。K_Score_Box は TextMeshPro オブジェクトにアタッチしてください。");
        }
    }

    void Update()
    {
        // Score_Receiver の static score をそのまま表示する
        hpText.text = "HP_BONUS " + Score_Receiver.hp.ToString();
    }
}
