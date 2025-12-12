using UnityEngine;

public class PlayerGameover : MonoBehaviour
{
    [Header("< Gameover >")]
    public GameObject error;
    public GameObject ui;
    public GameObject white;
    bool isGameover = false;

    // Update is called once per frame
    void Update()
    {
        if (isGameover) return;

        //プレイヤーの体力が0以下の場合
        if (TryGetComponent<Player>(out var player))
            if (player.health == 0)
            {
                //ゲームオーバーシーンに移行
                isGameover = true;
                Time.timeScale = 0.0f;
                player.rbody.linearVelocity = Vector2.zero;
                GameObject newImage = Instantiate(white);
                newImage.transform.SetParent(ui.transform, false);
                Instantiate(error, new Vector2(0, 0), Quaternion.identity);
        }
    }
}
