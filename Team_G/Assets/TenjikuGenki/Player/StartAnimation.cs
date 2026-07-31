using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class StartAnimation : MonoBehaviour
{
    Vector3 INITAIL_POS = new(-2.0f, -6.0f);
    const float MOVE_SPEED_Y = 3.0f;
    const float LIMIT_Y = -3.0f;

    public bool start_anime = true; //アニメーション切り替え

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //開始位置
        transform.position = INITAIL_POS;
        start_anime = true;
    }

    public bool StartAnime()
    {
        // 移動処理
        if (transform.position.y < LIMIT_Y)
            transform.position += new Vector3(0.0f, MOVE_SPEED_Y * Time.deltaTime, 0.0f);
        else return true;
        
        // 移動中ならfalse
        return false;
    }
}
