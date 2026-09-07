using Const;
using UnityEngine;
using System.Collections; 
public class h_AudioManager : MonoBehaviour
{
    [SerializeField] AudioData[] audio_data; //サウンドデータ

    [SerializeField] AudioSource bgm_audio;//BGMを鳴らす用
    [SerializeField] AudioSource se_audio; //SEを鳴らす用

    public float fade_time = 0.5f; //消えるまでの時間

    public static h_AudioManager Instance;

    private Coroutine fade_bgm;
    void Awake()
    {
        Instance = this; //シングルトン

        bgm_audio.loop = true; //BGMのループをON
    }


    public void PlayBGM(AudioConst.BGM_ID bgm_id, float volume)
    {
        //受け取った数値をint型に変換
        int id = (int)bgm_id;

        bgm_audio.volume = volume; //音量を設定
        Debug.Log(bgm_audio.volume);

        bgm_audio.clip = audio_data[AudioConst.BGM].Audio[id]; //BGM設定

        bgm_audio.Play(); //対応したBGMを流す
    }

    public void FadeStopBGM()
    {
        if (fade_bgm == null)
        {
            fade_bgm = StartCoroutine(FadeBGM());
        }
    }

    public void StopBGM()
    {
        //音を止める
        bgm_audio.Stop();
    }


    /// <summary>
    /// SEを鳴らす用メソッド
    /// </summary>
    /// <param name="se_id"></param>
    /// <param name="volume"></param>
    public void PlaySE(AudioConst.SE_ID se_id, float volume)
    {
        //受け取った数値をint型に変換
        int id = (int)se_id;

        Debug.Log("SEを鳴らしました");
        se_audio.volume = volume; //音量を設定
        se_audio.PlayOneShot(audio_data[AudioConst.SE].Audio[id]); //対応したSEを流す

    }

    private IEnumerator FadeBGM()
    {
        float start_volume = bgm_audio.volume;
        float current_time = 0; //経過時間


        while (current_time < fade_time)
        {
            current_time += Time.deltaTime;

            bgm_audio.volume = Mathf.Lerp(start_volume, 0f, current_time / fade_time);
            Debug.Log(bgm_audio.volume);

            yield return null;
        }

        //音量を0に合わせて止める
        bgm_audio.volume = 0f;
        bgm_audio.Stop();

        fade_bgm = null;
    }

}