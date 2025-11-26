using UnityEngine;
using UnityEngine.Audio;

public class Stage_BGM : MonoBehaviour
{
    public bool bgm_stop=false;
    public AudioClip bgm;
    AudioSource audioSource;

    public static Stage_BGM Instance { get; private set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        //BGMóp
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = bgm;
        audioSource.loop = true;
        audioSource.playOnAwake = false;
        if (audioSource == null)
        {
            Debug.LogWarning($"{name}: AudioSource Ç™å©Ç¬Ç©ÇËÇ‹ÇπÇÒÅBé©ìÆÇ≈í«â¡ÇµÇ‹Ç∑ÅB");
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!bgm_stop) {
                if (!audioSource.isPlaying)
                {
                    audioSource.Play();

                }            
        }
        else audioSource.Stop();

    }
}
