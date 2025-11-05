using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class h_Bomb_Gage : MonoBehaviour
{
    public float bomb_gage_up = 0; //時間経過で進むボムゲージ
    public int bomb_time = 0;    //ボムゲージが進む頻度
    public Slider bomb_gage;

    private int frame = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bomb_gage.value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        frame++;

        if(frame == bomb_time)
        {
            frame = 0;
            bomb_gage.value = bomb_gage_up;
        }

        if (bomb_gage.value > 200)
        {
            bomb_gage.value = 0;
        }
        
    }
}
