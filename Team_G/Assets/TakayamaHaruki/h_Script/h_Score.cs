//Score.cs

using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public int total_score = 0; //合計スコア
    private TMP_Text scoreText; //テキストオブジェクト

    public static Score Instance { get; private set; }
    private void Awake()
    {
        //シングルトン
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        total_score = 0; //スコアをリセット

        if (!(DataHolder.game_phaze < 0))
            total_score = DataHolder.save_score;

        scoreText = GetComponent<TMP_Text>(); //コンポーネントを取得
        scoreText.text = "SCORE:" + total_score.ToString(); //初期スコア表示
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "SCORE:" + total_score.ToString(); //最新のスコアを表示
    }
}

