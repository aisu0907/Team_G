using System.Threading; 
using TMPro;
using UnityEngine;

public class Result_Manager : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText; // リザルト画面のテキスト
    public static Result_Manager Instance { get; private set; }

    public static int score = 0;

    public int timer = 0;

    public GameObject score_box;

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
        timer++;
        if (timer == 120)
        {
<<<<<<< HEAD
            Instantiate(score_box, transform.position, Quaternion.identity);
=======
           // Instantiate(score_box,, Quaternion.identity);
>>>>>>> a02a2c664dcad0d96a08ab825c981410deb1d0ab
        }
        if (timer == 150)
        {

        }
        if (timer == 240)
        {

        }

    }

}
