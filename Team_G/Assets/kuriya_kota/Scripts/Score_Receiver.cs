using UnityEngine;

public class Score_Receiver : MonoBehaviour
{

    public static Score_Receiver Instance { get; private set; }

    //public static int score = 0;
    public static int score = 0;

    void Awake()
    {
        Instance = this;
    }
   
    // Update is called once per frame
    void Update()
    {
        score=Score.Instance.total_score;
    }
}
