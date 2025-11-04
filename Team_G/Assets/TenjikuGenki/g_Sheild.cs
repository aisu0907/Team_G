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
        if (collision.TryGetComponent<Enemy>(out var obj))
        {
            if (IsHitFallingEnemy(obj))
            {
                // Dicide Vector
                Vector2 d = (collision.transform.position - transform.position).normalized;
                obj.vec = d;
                obj.on_hitting = true;
            }
        }
    }

    bool IsHitFallingEnemy(Enemy obj)
    {
        if (!obj.on_hitting && obj.color == color) return true;
        return false;
    }
}