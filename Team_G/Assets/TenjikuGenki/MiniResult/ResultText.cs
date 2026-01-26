using System.Threading;
using TMPro;
using UnityEngine;

public class ResultText : MonoBehaviour
{
    // リザルトの表示
    TextMeshProUGUI tips;
    RectTransform rect;
    public GameObject uiPrefab;
    GameObject obj;
    float timer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // 初期設定
        tips = GetComponent<TextMeshProUGUI>();
        rect = GetComponent<RectTransform>();
        transform.SetAsLastSibling();
    }

    // Update is called once per frame
    void Update()
    {
        // テキストの移動
        if (rect.anchoredPosition.x < -200)
        {
            rect.anchoredPosition += new Vector2(700, 0f) * Time.deltaTime;
        }
        // 一定位置まで移動したらリザルトを表示
        else
        {
            tips.text = "ボスを倒した！\n\nタイム:" + timer.ToString("N1") + "\n\n\nPress Z Key";
            
            // Zキーでゲームを再開
            if (Input.GetKeyDown(KeyCode.Z))
            {
                Destroy(Dark.Instance.gameObject);
                Destroy(gameObject);
                GameManager.Instance.ModeChange(true);
                GameManager.Instance.frame = 0;
                GameManager.Instance.phase++;
                DataHolder.GetGameData();
            }
        }
    }

    // 初期化
    public void init(float _timer)
    {
        timer = _timer;
    }
}
