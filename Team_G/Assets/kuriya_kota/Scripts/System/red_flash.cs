using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class RedFlash : MonoBehaviour
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
    }
}
