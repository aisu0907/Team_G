using Const;
using UnityEngine;

public class h_AudioManager : MonoBehaviour
{
    [SerializeField] AudioData[] audio_data; //サウンドデータ

    [SerializeField] AudioSource bgm_audio;//BGMを鳴らす用
    [SerializeField] AudioSource se_audio; //SEを鳴らす用

    public static h_AudioManager Instance;

    void Awake()
    {
        Instance = this; //シングルトン

        bgm_audio.loop = true; //BGMのループをON
    }


    public void PlayBGM(AudioConst.BGM_ID bgm_id, float vlome)
    {
        //受け取った数値をint型に変換
        int id = (int)bgm_id;

        //BGMがなっていたら止める
        if (bgm_audio.clip != null)
            bgm_audio.Stop();

        bgm_audio.volume = vlome; //音量を設定

        bgm_audio.clip = audio_data[AudioConst.BGM].audio[id]; //BGM設定

        bgm_audio.Play(); //対応したBGMを流す
    }

    public void StopBGM()
    {
        bgm_audio.Stop();
    }

    /// <summary>
    /// SEを鳴らす用メソッド
    /// </summary>
    /// <param name="se_id"></param>
    /// <param name="vlome"></param>
    public void PlaySE(AudioConst.SE_ID se_id, float vlome)
    {
        //受け取った数値をint型に変換
        int id = (int)se_id;

        Debug.Log("SEを鳴らしました");
        se_audio.volume = vlome; //音量を設定
        se_audio.PlayOneShot(audio_data[AudioConst.SE].audio[id]); //対応したSEを流す

    }


}