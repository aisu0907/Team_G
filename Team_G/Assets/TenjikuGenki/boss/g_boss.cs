using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class g_boss : MonoBehaviour
{
    public int health;
    [System.Serializable] public class enemy_list { public EnemyData db; public GameObject pf; };
    public List<enemy_list> list = new List<enemy_list>();
    public SpriteRenderer img;
    public List<Sprite> sprites;
    public AudioClip sound1;
    public AudioSource audioSource;
    [SerializeField] int Timer;
    bool once;
    Vector2 tmp_pos;
    Rigidbody2D rb; // î≠éÀä‘äu
    bool left_move = true;
    public GameObject explode;

    private void Update()
    {
        Timer += 1;

        // àÍíËéûä‘Ç≤Ç∆Ç…íeÇî≠éÀ
        if (health > 0)
        {
            if (Timer >= 120)
            {
                Timer = 60;
                ShootBullet();
            } 
        }
        // éÄñSââèo
        else
        {
            if(once)
            {
                Timer = 0;
                once = false;
            }
            if(Timer % 5 == 0)
                transform.position = new Vector2(tmp_pos.x + 0.1f, transform.position.y);
            else
                transform.position = new Vector2(tmp_pos.x - 0.1f, transform.position.y);
            rb.linearVelocityY = 1.0f;
            if(Timer >= 330) if (health <= 0) GameManager.Instance.KillBoss(gameObject);
        }

        // ç∂âEà⁄ìÆ
        if(left_move)
        {
            rb.linearVelocityX = 1.0f;
        }
        else
        {
            rb.linearVelocityX = -1.0f;
        }
    }
    private void Start()
    {
        // ç≈èâÇÃèÛë‘ÇPhase1Ç…ê›íË
        img = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        tmp_pos = transform.position;
        rb = GetComponent<Rigidbody2D>();
        transform.position = new Vector2(transform.position.x - 0.5f,transform.position.y);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Enemy>(out var enemy))
            if (enemy.on_hitting)
            {
                Destroy(enemy.gameObject);
                health--;
                Instantiate(explode, new Vector2(enemy.transform.position.x, enemy.transform.position.y + 0.5f), Quaternion.identity);
            }
        if(collision.GetComponent<Side_Wall>()) left_move = !left_move;
    }

    void ShootBullet()
    {
        // é¿ç€Ç…ÇÕÇ±Ç±Ç≈íePrefabÇInstantiateÇµÇΩÇËÅAObjectPoolÇégÇ¡ÇΩÇËÇµÇ‹Ç∑
        int color = Random.Range(0, list.Count);
        img.sprite = sprites[color];
        Vector2 d = (Player.Instance.transform.position - transform.position).normalized;
        var e = Instantiate(list[0].pf, transform.position, Quaternion.identity).GetComponent<ENormal>(); e.Init(list[0].db, d, color, 5);
        audioSource.PlayOneShot(sound1);
    }
}