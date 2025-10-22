using System.Collections.Generic;
using UnityEngine;

public class t_Enemy_Spwan : MonoBehaviour
{
    public GameObject enemy;    // 生成オブジェクト
    [SerializeField] Transform pos;                 // 生成位置
    [SerializeField] Transform pos2;                // 生成位置
    float minX, maxX, minY, maxY;                   // 生成範囲

    int frame = 0;
    [SerializeField] int generateFrame = 30;        // 生成する間隔

    void Start()
    {
        //スポーン位置設定
        minX = Mathf.Min(pos.position.x, pos2.position.x);
        maxX = Mathf.Max(pos.position.x, pos2.position.x);
        minY = Mathf.Min(pos.position.y, pos2.position.y);
        maxY = Mathf.Max(pos.position.y, pos2.position.y);
    }

    void Update()
    {
        ++frame;

        if (frame > generateFrame)
        {
            frame = 0;

            // ランダムで種類と位置を決める
            float posX = Random.Range(minX, maxX);
            float posY = Random.Range(minY, maxY);

            //ウイルスをスポーン
            //EnemyManager(new Vector2(posX,posY)
        }
    }
}
