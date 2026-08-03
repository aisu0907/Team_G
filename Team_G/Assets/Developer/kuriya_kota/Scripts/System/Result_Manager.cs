using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultManager : MonoBehaviour
{
    public static ResultManager Instance { get; private set; }

    public static int score = 0;
    public int timer = 0;
    public int hp_bonus;
    public int s_rank;
    public int a_rank;
    public int b_rank;
    public int c_rank;
    public int d_rank;

    public GameObject score_box;
    public GameObject bonus_box;
    public GameObject text;
    public GameObject game;

    public GameObject S;
    public GameObject A;
    public GameObject B;
    public GameObject C;
    public GameObject D;
    public GameObject E;

    public AudioClip sound1;
    public AudioClip sound2;
    public AudioClip BGMClip; 

    private AudioSource sfxSource;
    private AudioSource bgmSource;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        DataHolder.DataReset();
        score = ScoreReceiver.score;

        AudioSource[] sources = GetComponents<AudioSource>();
        if (sources.Length >= 2)
        {
            sfxSource = sources[0];   
            bgmSource = sources[1];   
        }
        else
        {
            Debug.LogError("AudioSourceが2つ必要です");
        }

       
        bgmSource.clip = BGMClip;
        bgmSource.loop = true;
        bgmSource.playOnAwake = false;

        text.GetComponent<TMP_Text>().enabled = false;
        score_box.GetComponent<TMP_Text>().enabled = false;
        bonus_box.GetComponent<TMP_Text>().enabled = false;

        Debug.Log("受け取ったスコア: " + score);
    }

    void Update()
    {
        if (timer < 340) timer++;

        if (timer == 120)
        {
            sfxSource.PlayOneShot(sound1);
            score_box.GetComponent<TMP_Text>().enabled = true;
        }
        if (timer == 150)
        {
            sfxSource.PlayOneShot(sound1);
            bonus_box.GetComponent<TMP_Text>().enabled = true;
        }
        if (timer == 240)
        {
            sfxSource.PlayOneShot(sound2);
            ShowRank();
        }
        if (timer == 340)
        {
            text.GetComponent<TMP_Text>().enabled = true;
            game.GetComponent<TMP_Text>().enabled = true;
            if (!bgmSource.isPlaying) bgmSource.Play();
        }

        if (timer > 320 && Input.GetKey(KeyCode.Z))
        {
            SceneManager.LoadScene("TitleScene");
        }
        score = ScoreReceiver.score + (ScoreReceiver.hp * hp_bonus);
    }
    /// <summary>
    /// 受け取った値を参照してランクを表示する
    /// </summary>
    void ShowRank()
    {
        GameObject rankObj = null;

        if (score >= s_rank) rankObj = S;
        else if (score >= a_rank) rankObj = A;
        else if (score >= b_rank) rankObj = B;
        else if (score >= c_rank) rankObj = C;
        else if (score >= d_rank) rankObj = D;
        else rankObj = E;

        Instantiate(rankObj, transform.position, Quaternion.identity);
    }
}
