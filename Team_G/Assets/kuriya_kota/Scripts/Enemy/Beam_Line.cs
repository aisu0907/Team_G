using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class BeamLine : MonoBehaviour
{
    public GameObject prefab;   // 呼び出すオブジェクト
    public float blinkInterval = 0.05f; // 点滅間隔
    public float lifeTime = 3f; // 生成までの時間

    [Header("Audio Clips")]
    [SerializeField] private AudioClip sound1;
    [SerializeField] private AudioClip sound2;
    private AudioSource audioSource;

    private SpriteRenderer sr;
    private bool visible = true;

    void Start()
    {
        if (LastBoss.Instance.health <= 0)
        {
            Destroy(gameObject);
        }

        sr = GetComponent<SpriteRenderer>();
        StartCoroutine(BlinkAndSpawn()); 
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(sound1);
    }
    /// <summary>
    /// timerがlifeTime未満ならblinkInterval間隔で点滅させる
    /// </summary>
    /// <returns></returns>
    IEnumerator BlinkAndSpawn()
    {
        float timer = 0f;

        while (timer < lifeTime)
        {
            visible = !visible;
            sr.enabled = visible;
            yield return new WaitForSeconds(blinkInterval);
            timer += blinkInterval;
        }

        Instantiate(prefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
