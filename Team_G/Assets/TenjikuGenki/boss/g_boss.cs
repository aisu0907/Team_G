using System.Collections.Generic;
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

    public void ChangeState(IBossState newState)
    {
        currentState?.Exit(this);
        currentState = newState;
        currentState.Enter(this);
    }

    private void Update()
    {
        currentState?.Main(this);
    }
    private void Start()
    {
        // ç≈èâÇÃèÛë‘ÇPhase1Ç…ê›íË
        ChangeState(new g_BossPhase1());
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
            }
    }
}