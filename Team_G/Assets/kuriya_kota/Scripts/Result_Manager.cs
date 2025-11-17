using System.Threading; 
using TMPro;
using UnityEngine;

public class Result_Manager : MonoBehaviour
{
    public static Result_Manager Instance { get; private set; }

    public static int score = 0;

    public int timer = 0;

    public GameObject score_box;

    public GameObject bonus_box;

    public GameObject S;
    public GameObject A;
    public GameObject B;
    public GameObject C;
    public GameObject D;
    public GameObject E;


    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        // メインシーンのスコアを取得
        score = Score_Receiver.score;
        // デバッグ用
        Debug.Log("受け取ったスコア: " + score);
    }

    private void Update()
    {
        score=Score_Receiver.score+(Score_Receiver.hp*10000);

        timer++;
        if (timer == 120)
        {
           score_box.GetComponent<TMP_Text>().enabled = true;
        }
        if (timer == 180)
        {
            bonus_box.GetComponent<TMP_Text>().enabled = true;
        }
        if (timer == 240)
        {
            if (score >= 0)
            {
                Instantiate(S, transform.position, Quaternion.identity);
            }




        }

    }

}
