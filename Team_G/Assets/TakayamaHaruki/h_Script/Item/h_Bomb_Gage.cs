//h_Bomb_Gage.cs

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class h_Bomb_Gage : MonoBehaviour
{
    public float bomb_gage_max = 0;//ボムゲージ最大値
    public float bomb_gage_up = 0; //時間経過で進むボムゲージ
    public int bomb_time = 0;      //ボムゲージが進む頻度
    public Slider bomb_gage;       //スライダーを取得

    private int frame = 0;//フレーム

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bomb_gage.value = 0;　//スライダーの位置をリセット
    }

    // Update is called once per frame
    void Update()
    {
        frame++;

        //1秒間にためるゲージ
        if(frame >= bomb_time)
        {
            //frameをリセット
            frame = 0;
            //ゲージを増やす
            bomb_gage.value += bomb_gage_up;
        }

        //ゲージがMAXの場合
        if (bomb_gage.value >= bomb_gage_max)
        {
            bomb_add();
        }
    }

    //ボムを増やす
    public void bomb_add()
    {
        //ゲージをリセット
        bomb_gage.value = 0;
        //プレイヤーのボム所持数が最大じゃない場合
        if (Player.Instance.bom < Player.Instance.max_bom)
            //ボムを1個増やす
            Player.Instance.bom++;
    }
}
