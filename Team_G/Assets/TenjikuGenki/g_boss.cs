using UnityEngine;

public class g_boss : UnitBase
{
    public GameObject Enemy;
    int timer;
    public static g_boss Instance { get; private set; }

    void Awake()
    {
        Instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // UnitBaseåpè≥
        Speed = 0.0f;   //à⁄ìÆë¨ìx
        Health = 3;     //ëÃóÕ
    }

    // Update is called once per frame
    void Update()
    {
        ++timer;
        if(timer == 120)
        {
            // ìGÇÃå^ÇéÊìæ -> â∫èÄîı
            GameObject shot = Instantiate(Enemy, transform.position, Quaternion.identity);
            g_enemy enemy = shot.GetComponent<g_enemy>();
            Vector2 direction = (Player.Instance.transform.position - enemy.transform.position).normalized;

            // ê∂ê¨
            enemy.Create(GetPos(), direction, 0, 0);
            timer = 0;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && collision.gameObject.GetComponent<g_enemy>().OnHitting)
        {
            --Health;
            Debug.Log("gue-----------");
            Destroy(collision.gameObject);
            if (Health == 0) Destroy(gameObject);
        }
    }
}
