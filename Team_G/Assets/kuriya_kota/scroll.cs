using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//スクロール処理です
//画像縦に3枚並べたら使えまｽ
public class TateScroll : MonoBehaviour
{
    private float speed = 1;

    void Update()
    {
        transform.position -= new Vector3(0, Time.deltaTime * speed);

        if (transform.position.y <= -11)
        {
            transform.position = new Vector3(1, 18.1f);
        }
    }
}