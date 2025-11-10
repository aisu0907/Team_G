using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public int total_score = 0;
    private TMP_Text scoreText;

    public static Score Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        total_score = 0;
        scoreText = GetComponent<TMP_Text>();
        scoreText.text = "SCORE:" + total_score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "SCORE:" + total_score.ToString();
    }
}

