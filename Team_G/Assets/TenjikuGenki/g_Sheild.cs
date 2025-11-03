using System.Collections.Generic;
using UnityEngine;

public class Sheild : MonoBehaviour
{
    SpriteRenderer img;
    public int color = 0;
    [SerializeField] List<Sprite> Img;   //�摜
    public static Sheild Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        img = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // �F�ύX����
        if (Input.GetKey(KeyCode.Z))
        {
            img.sprite = Img[0];
            color = 0;
        }
        if (Input.GetKey(KeyCode.X))
        {
            img.sprite = Img[1];
            color = 1;
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        // Definition "collision" 
        if(collision.tag == "Enemy")
        {
            var enemy = collision.gameObject.GetComponent<Enemy>();
            if (!enemy.GetComponent<Enemy>().on_hitting)
            {
                if (enemy.GetComponent<Enemy>().color == color)
                {
                    // Dicide Vector
                    Vector2 d = (collision.transform.position - transform.position).normalized;
                    enemy.GetComponent<Enemy>().vec = d;
                    enemy.GetComponent<Enemy>().on_hitting = true;
                }
            }
        }
    }
        //else if (enemy.tag == "Boss")
        //    if (enemy.GetComponent<g_boss>().color == color)
        //        enemy.GetComponent<g_boss>().reflect = true;
}