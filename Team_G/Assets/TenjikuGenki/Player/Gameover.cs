using System;
using UnityEngine;

public class PlayerGameover : MonoBehaviour
{
    [Header("▼ Gameover")]
    [SerializeField] GameObject error;
    [SerializeField] GameObject ui;
    [SerializeField] CanvasGroup white;
    public static event Action OnPlayerDead;

    // 変数
    bool isGameover = false;

    // Update is called once per frame
    void Update()
    {
        //プレイヤーの体力が0以下の場合
        if (TryGetComponent<Player>(out var player))
        {
            // 色を段々と変える
            if(isGameover)
            {
                if(white.alpha < 120.0f / 256.0f)
                    white.alpha += 0.015f;
                return;
            }

            // 一度だけの処理
            if (player.health == 0)
            {
                // 白画面とウィンドウ出す
                white = Instantiate(white);
                white.transform.SetParent(ui.transform, false);
                Instantiate(error, new Vector2(0, 0), Quaternion.identity);

                // 諸々
                OnPlayerDead?.Invoke();
                isGameover = true;
                Time.timeScale = 0.0f;
                player.rbody.linearVelocity = Vector2.zero;
            }
        }
    }
}
