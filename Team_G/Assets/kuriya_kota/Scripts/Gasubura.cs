using UnityEngine;
using UnityEngine.Audio;

public class Gasubura : MonoBehaviour,IDamageable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public AudioClip sound1;
    public AudioClip sound2;
    AudioSource audioSource;

    // Update is called once per frame
    void star()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(sound1);
    }
    
    
    
    
    void Update()
    {
        Destroy(gameObject,1);
    }
    public void Damage()
    {
        Player.Instance.health-=1;
    }
}
