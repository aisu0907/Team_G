using TMPro;
using UnityEngine;

public class Get_Item : MonoBehaviour
{
    [SerializeField] GameObject textPrefab; // Textプレハブ
    Transform ui;                           // Canvas
    RectTransform uiRect;                   // Canvas の RectTransform

    void Start()
    {
        // Canvas を自動取得
        var canvasObj = GameObject.Find("Canvas");
        ui = canvasObj.transform;
        uiRect = canvasObj.GetComponent<RectTransform>();
    }

    public void CreateTextAt(Vector3 worldPos, string text)
    {
        // ワールド座標 → スクリーン座標
        Vector2 screenPos = Camera.main.WorldToScreenPoint(worldPos);

        // スクリーン座標 → Canvasローカル座標
        Vector2 uiPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            uiRect, screenPos, Camera.main, out uiPos
        );

        // Text UI 生成
        var obj = Instantiate(textPrefab, ui, false);

        // 位置をセット
        obj.GetComponent<RectTransform>().anchoredPosition = uiPos;

        // テキスト内容をセット
        obj.GetComponent<TextMeshProUGUI>().text = text;
    }
}
