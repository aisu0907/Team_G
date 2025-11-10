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
        // 盾の色を変更
        if (Input.GetKey(KeyCode.Z)) ChangeSheildColor(0);
        if (Input.GetKey(KeyCode.X)) ChangeSheildColor(1);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        // 敵機の情報を取得
        if (collision.TryGetComponent<Enemy>(out var obj))
        {
            // 接触した敵機と盾の色が同じでかつ、それが妨害ウイルスじゃないなら、
            if (IsHitFallingEnemy(obj) && obj.type != 2)
            {
                // ベクトルを反転
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

    void ChangeSheildColor(int n)
    {
        img.sprite = Img[n];
        color = n;
    }
}