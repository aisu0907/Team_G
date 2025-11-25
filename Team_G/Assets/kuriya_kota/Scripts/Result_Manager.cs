using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Result_Manager : MonoBehaviour
{
    public static Result_Manager Instance { get; private set; }

    public static int score = 0;
    public int timer = 0;

    public GameObject score_box;
    public GameObject bonus_box;
    public GameObject text;

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
        score = Score_Receiver.score;

        AudioSource[] sources = GetComponents<AudioSource>();
        if (sources.Length >= 2)
        {
            sfxSource = sources[0];   
            bgmSource = sources[1];   
        }
        else
        {
            Debug.LogError("AudioSourceが2つ必要です！");
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
            if (!bgmSource.isPlaying) bgmSource.Play();
        }

        if (timer > 320 && Input.GetKey(KeyCode.Z))
        {
            SceneManager.LoadScene("Title");
        }
        score = Score_Receiver.score + (Score_Receiver.hp * 10000);
    }

    void ShowRank()
    {
        GameObject rankObj = null;

        if (score >= 100000) rankObj = S;
        else if (score >= 90000) rankObj = A;
        else if (score >= 80000) rankObj = B;
        else if (score >= 70000) rankObj = C;
        else if (score >= 60000) rankObj = D;
        else rankObj = E;

        Instantiate(rankObj, transform.position, Quaternion.identity);
    }
}
