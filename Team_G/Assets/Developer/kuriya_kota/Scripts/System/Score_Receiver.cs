using UnityEngine;

public class ScoreReceiver : MonoBehaviour
{

    public static ScoreReceiver Instance { get; private set; }

    //public static int score = 0;
    public static int score = 0;
    public static int hp = 0;


    void Awake()
    {
        Instance = this;
    }
   
    // Update is called once per frame
    void Update()
    {
        score=Score.Instance.total_score;
        hp = Player.Instance.health;
    }
}
