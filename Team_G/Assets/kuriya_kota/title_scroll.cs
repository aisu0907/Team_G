using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiteleScroll : MonoBehaviour
{
    public float speed = 0.5f;          // スクロール速度
    public float resetPositionY = -10f; // どこまで下がったらリセットするか
    public float startPositionY = 10f;  // 上に戻す位置

    void Update()
    {
        // 下に移動
        transform.position -= new Vector3(0, speed * Time.deltaTime, 0);

        // 一定位置まで下がったら上へ戻す
        if (transform.position.y <= resetPositionY)
        {
            transform.position = new Vector3(transform.position.x, startPositionY, transform.position.z);
        }
    }
}
