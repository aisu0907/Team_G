using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    static AudioManager instance;
    private Dictionary<string, AudioClip> bgmDict;

    void Awake()
    {
        instance = this;
    }

    public void PlaySound(GameObject obj, string sound)
    {
        AudioSource AS = obj.AddComponent<AudioSource>();
        AS.clip = clip
    }

}
