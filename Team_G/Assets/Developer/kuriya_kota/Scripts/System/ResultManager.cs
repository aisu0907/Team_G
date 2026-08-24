using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Const;
using UnityEngine.UI;
using System.Collections;

public class ResultManager : MonoBehaviour
{
    public static int score = 0;
    
    [SerializeField] Sprite[] rank; //ランクの画像
    
    public int[] rank_score; //ランクのスコア

    public int hp_bonus; //HPボーナス

    public GameObject rank_obj;
    public GameObject score_box;
    public GameObject bonus_box;
    public GameObject text;
    public GameObject game;

    public AudioClip sound1;
    public AudioClip sound2;
    public AudioClip BGMClip;

    private AudioSource sfxSource;
    private AudioSource bgmSource;
   
    private Image rank_img;
    private bool show_end = false; //リザルト終わり
    
    public static ResultManager Instance { get; private set; }

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        rank_obj.SetActive(false);

        DataHolder.DataReset();
        score = ScoreReceiver.score;

        AudioSource[] sources = GetComponents<AudioSource>();

        rank_img = rank_obj.GetComponent<Image>();

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

        text.SetActive(false);
        game.SetActive(false);
        score_box.SetActive(false);
        bonus_box.SetActive(false);

        Debug.Log("受け取ったスコア: " + score);
    }
    
    public void StartResult()
    {
        score = ScoreReceiver.score + (ScoreReceiver.hp * hp_bonus);

        StartCoroutine(ShowResult());
    }

    private IEnumerator ShowResult()
    {
        yield return new WaitForSeconds(2.0f);

        sfxSource.PlayOneShot(sound1);
        score_box.SetActive(true);

        yield return new WaitForSeconds(0.5f);
        
        sfxSource.PlayOneShot(sound1);
        bonus_box.SetActive(true);

        yield return new WaitForSeconds(1.5f);

        sfxSource.PlayOneShot(sound2);
        ShowRank();

        yield return new WaitForSeconds(0.68f);

        text.SetActive(true);
        game.SetActive(true);
        if (!bgmSource.isPlaying) bgmSource.Play();

        show_end = true;
    }

    /// <summary>
    /// 受け取った値を参照してランクを表示する
    /// </summary>
    void ShowRank()
    {
        if (score >= rank_score[(int)ScoreConst.SCORE.S]) rank_img.sprite = rank[(int)ScoreConst.SCORE.S];
        else if (score >= rank_score[(int)ScoreConst.SCORE.A]) rank_img.sprite = rank[(int)ScoreConst.SCORE.A];
        else if (score >= rank_score[(int)ScoreConst.SCORE.B]) rank_img.sprite = rank[(int)ScoreConst.SCORE.B];
        else if (score >= rank_score[(int)ScoreConst.SCORE.C]) rank_img.sprite = rank[(int)ScoreConst.SCORE.C];
        else if (score >= rank_score[(int)ScoreConst.SCORE.D]) rank_img.sprite = rank[(int)ScoreConst.SCORE.D];
        else rank_img.sprite = rank[(int)ScoreConst.SCORE.E];

        rank_obj.SetActive(true);
    }

    public void Interact(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            if (show_end)
            {
                SceneManager.LoadScene(SceneNames.Title);
            }
        }
    }
}
