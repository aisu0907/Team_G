using UnityEngine;

public class ResultPlayerAnimation : MonoBehaviour
{
    public Vector2 start_pos; //初期位置
    public float target_x = 400;   // 到達したい位置
    public float speed = 5f;         // 右方向の移動速度

    private RectTransform rect;
    private bool set_pos = false;

    private void Start()
    {
        rect = GetComponent<RectTransform>();

        rect.anchoredPosition = start_pos;
    }

    void Update()
    {
        // テキストの移動
        if (rect.anchoredPosition.x < target_x)
        {
            rect.anchoredPosition += new Vector2(speed, 0f) * Time.deltaTime;
        }
        else if(!set_pos)
        {
            rect.anchoredPosition = new Vector2(target_x, rect.anchoredPosition.y);
            ResultManager.Instance.StartResult();
            set_pos = true;
        }
    }
}
