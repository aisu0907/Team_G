using UnityEngine;

public class Gasubura : MonoBehaviour
//IDamageable
{
    [Header("Audio Clips")]
    [SerializeField] private AudioClip sound1;
    [SerializeField] private AudioClip sound2;

    private AudioSource audioSource;

    private void Start()
    {
        // AudioSourceéÊìæ
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogWarning($"{name}: AudioSource Ç™å©Ç¬Ç©ÇËÇ‹ÇπÇÒÅBé©ìÆÇ≈í«â¡ÇµÇ‹Ç∑ÅB");
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // sound1Ççƒê∂
        if (sound1 != null)
        {
            audioSource.PlayOneShot(sound1);
        }

        // 1ïbå„Ç…é©ìÆîjâÛ
        Destroy(gameObject, 1);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player"&&k_boss.Instance.health>5)
        {
            Player.Instance.health--;
        }
        else if (collision.gameObject.tag == "Player" && k_boss.Instance.health <= 5)
        {
            Player.Instance.health-=2;
        }
    }


}
