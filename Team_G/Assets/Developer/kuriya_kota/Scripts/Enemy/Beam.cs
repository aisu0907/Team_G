using UnityEngine;

public class Beam : MonoBehaviour, IHitable
//IDamageable
{
    // ダメージ処理
    public int Damage => _damage;
    int _damage = 0;

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
        if (sound1 != null&& LastBoss.Instance.health > 0)
        {
            audioSource.PlayOneShot(sound1);
        }

        // 1秒後に自動破壊
        Destroy(gameObject, 1);
    }

    private void Update()
    {
        if (LastBoss.Instance.health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Hit()
    {
        // 体力によって攻撃力を変更する
        if (LastBoss.Instance.health > 5) _damage = 1;
        else _damage = 0;
    }
}
