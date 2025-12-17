using System.Threading;
using TMPro;
using UnityEngine;

public class ResultText : MonoBehaviour
{

    TextMeshProUGUI tips;
    RectTransform rect;
    public GameObject uiPrefab;
    GameObject obj;
    float timer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tips = GetComponent<TextMeshProUGUI>();
        rect = GetComponent<RectTransform>();
        transform.SetAsLastSibling();
    }

    // Update is called once per frame
    void Update()
    {
        if (rect.anchoredPosition.x < -200)
        {
            rect.anchoredPosition += new Vector2(700, 0f) * Time.deltaTime;
        }
        else
        {
            tips.text = "ボスを倒した！\n\nタイム:" + timer.ToString("N1") + "\n\n\nPress Z Key";
            if (Input.GetKeyDown(KeyCode.Z))
            {
                Destroy(dark.Instance.gameObject);
                Destroy(gameObject);
                GameManager.Instance.ModeChange(true);
                GameManager.Instance.frame = 0;
                GameManager.Instance.faze++;
            }
        }
    }

    public void init(float _timer)
    {
        timer = _timer;
    }
}
