using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] List<AudioClip> audio_list;
    Dictionary<string, AudioClip> audio_dic = new Dictionary<string, AudioClip>();
    AudioSource se; 

    void Awake()
    {
        instance = this;

        se = GetComponent<AudioSource>();
        foreach (var clip in audio_list)
        {
            audio_dic[clip.name] = clip;
        }
    }

    public void PlaySound(string sound, float volume = 1.0f)
    {
        se.PlayOneShot(audio_dic[sound],volume);
    }

}
