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
    public AudioClip sound3;

    AudioSource audioSource;



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
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (timer == 320)
        {
            audioSource.PlayOneShot(sound3);
            text.GetComponent<TMP_Text>().enabled = true;
        }
            if (timer > 320 && Input.GetKey(KeyCode.Z))
        {
            SceneManager.LoadScene("Title");
        }
        score =Score_Receiver.score+(Score_Receiver.hp*10000);

        timer++;
        if (timer == 120)
        {
            audioSource.PlayOneShot(sound1);
           score_box.GetComponent<TMP_Text>().enabled = true;
        }
        if (timer == 150)
        {
            audioSource.PlayOneShot(sound1);
            bonus_box.GetComponent<TMP_Text>().enabled = true;
        }
        if (timer == 240)
        {
            audioSource.PlayOneShot(sound2);
            if (score >= 100000)
            {
                Instantiate(S, transform.position, Quaternion.identity);
            }
            else if (score >= 90000)
            {
                Instantiate(A, transform.position, Quaternion.identity);
            }
            else if (score >= 80000)
            {
                Instantiate(B, transform.position, Quaternion.identity);
            }
            else if (score >= 70000)
            {
                Instantiate(C, transform.position, Quaternion.identity);
            }
            else if (score >= 60000)
            {
                Instantiate(D, transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(E, transform.position, Quaternion.identity);
            }
        }
    }
}
