using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class g_boss : MonoBehaviour
{
    public int health;
    IBossState currentState;
    [System.Serializable] public class enemy_list { public EnemyBase db; public GameObject pf; };
    public List<enemy_list> list = new List<enemy_list>();
    public SpriteRenderer img;
    public List<Sprite> sprites;
    public AudioClip sound1;
    public AudioSource audioSource;
    float Timer;

    private void Update()
    {
        Timer += Time.deltaTime;

        // ˆê’èŠÔ‚²‚Æ‚É’e‚ğ”­Ë
        if (Timer >= 1.0f)
        {
            ShootBullet();
            Timer = 0f;
        }
    }
    private void Start()
    {
        // Å‰‚Ìó‘Ô‚ğPhase1‚Éİ’è
        img = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Enemy>(out var enemy))
            if (enemy.on_hitting)
            {
                Destroy(collision.gameObject);
                health--;
                if (health == 0) GameManager.Instance.KillBoss(gameObject);
            }
    }

    void ShootBullet()
    {
        // ÀÛ‚É‚Í‚±‚±‚Å’ePrefab‚ğInstantiate‚µ‚½‚èAObjectPool‚ğg‚Á‚½‚è‚µ‚Ü‚·
        int color = Random.Range(0, list.Count);
        img.sprite = sprites[color];
        Vector2 d = (Player.Instance.transform.position - transform.position).normalized;
        var e = Instantiate(list[0].pf, transform.position, Quaternion.identity).GetComponent<ENormal>(); e.Init(list[0].db, d, color, 5);
        audioSource.PlayOneShot(sound1);
    }
}