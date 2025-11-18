using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public class Explode : MonoBehaviour
{
    public float size =0.0f;
    
    public float max_size=0.4f;

    public float add_size = 0.1f;

    public int timer = 0;

    public AudioClip sound;
    AudioSource audioSource;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(sound);
    }

    // Update is called once per frame
    void Update()
    {
        timer++;
        if (size <= max_size) size += add_size;
        transform.localScale = new Vector2(size, size);

        if (timer > 60) Destroy(gameObject);


    }
}
