using System.Collections.Generic;
using UnityEngine;

public class Sheild : MonoBehaviour
{
    SpriteRenderer img;
    public int SheildColor = 0;
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
            SheildColor = 0;
        }
        if (Input.GetKey(KeyCode.X))
        {
            img.sprite = Img[1];
            SheildColor = 1;
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject enemy = collision.gameObject;
            if (!enemy.GetComponent<g_enemy>().OnHitting)
            {
                if (enemy.GetComponent<g_enemy>().EnemyColor == SheildColor)
                    if (enemy.GetComponent<g_enemy>().EnemyType != 2)
                    {
                        //���ˏ���
                        Vector2 d = enemy.transform.position - transform.position;
                        enemy.GetComponent<g_enemy>().vec = d;
                        enemy.GetComponent<g_enemy>().OnHitting = true;
                    }
            }
        }
        //else if (enemy.tag == "Boss")
        //    if (enemy.GetComponent<g_boss>().EnemyColor == SheildColor)
        //        enemy.GetComponent<g_boss>().reflect = true;
    }

