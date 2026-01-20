using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class test_flash : MonoBehaviour
{
    SpriteRenderer img;

    public AudioClip sound1;
    public AudioClip sound2;
    AudioSource audioSource;


    public float flashInTime = 0.05f;   // îíÇ≠Ç»ÇÈÇ‹Ç≈
    public float fadeOutTime = 0.9f;    // è¡Ç¶ÇÈÇ‹Ç≈ÇÃéûä‘

    public bool isFlashing = false;

    void Start()
    {
        img = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        FlashAndDisappear();
    }


    public void FlashAndDisappear()
    {
        StartCoroutine(FlashCoroutine());
    }

    IEnumerator FlashCoroutine()
    {
        
        isFlashing = true;

        float t = 0f;
        while (t < flashInTime)
        {
            t += Time.deltaTime;
            float lerp = t / flashInTime;
            img.color = Color.Lerp(img.color, Color.white, lerp);
            yield return null;
        }
        audioSource.PlayOneShot(sound1);
        audioSource.PlayOneShot(sound2);
        img.color = Color.white;  

        t = 0f;
        Color startColor = img.color;

        while (t < fadeOutTime)
        {
            t += Time.deltaTime;
            float lerp = t / fadeOutTime;

           
            Color c = startColor;
            c.a = Mathf.Lerp(1f, 0f, lerp);
            img.color = c;

            yield return null;
        }
        img.color = new Color(1, 1, 1, 0);    
    }
}
