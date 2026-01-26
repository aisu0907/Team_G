using UnityEngine;

public class Beam : MonoBehaviour
//IDamageable
{
    [Header("Audio Clips")]
    [SerializeField] private AudioClip sound1;
    [SerializeField] private AudioClip sound2;

    private AudioSource audioSource;

    private void Start()
    {
        // AudioSource取得
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogWarning($"{name}: AudioSource が見つかりません。自動で追加します。");
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // sound1を再生
        if (sound1 != null)
        {
            audioSource.PlayOneShot(sound1);
        }

        // 1秒後に自動破壊
        Destroy(gameObject, 1);
    }
    /// <summary>
    /// ボスの体力が5以下ならビームのダメージを２にする
    /// </summary>
    /// <param name="collision"></param>
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player"&&LastBoss.Instance.health>5)
        {
            Player.Instance.Damage(1,gameObject,false);
        }
        else if (collision.gameObject.tag == "Player" && LastBoss.Instance.health <= 5)
        {
            Player.Instance.Damage(2,gameObject,false);
        }
    }
}
