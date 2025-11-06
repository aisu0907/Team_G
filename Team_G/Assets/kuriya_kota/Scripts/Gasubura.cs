using UnityEngine;

public class Gasubura : MonoBehaviour, IDamageable
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

    public void Damage()
    {
        if (Player.Instance != null)
        {
            Player.Instance.health -= 1;
        }
        else
        {
            Debug.LogWarning("Player.Instance が存在しません。Damage()を実行できません。");
        }
    }
}
