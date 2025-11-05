using UnityEngine;
public class h_Bomb_Gage : MonoBehaviour
{
    public float bomb_gage_up = 0; //時間経過で進むボムゲージ
    public int bomb_time = 0;    //ボムゲージが進む頻度

    private int frame = 0;
    private Vector2 v = new Vector2(0, 30);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.transform.localScale = v;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.localScale = v;

        frame++;
        if(frame == bomb_time)
        {
            frame = 0;
            v.x = bomb_gage_up;
        }

        if (v.x > 200)
        {
            v.x = 0;
        }
        
    }
}
