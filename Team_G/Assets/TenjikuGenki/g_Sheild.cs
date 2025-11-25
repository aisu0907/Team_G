using System.Collections.Generic;
using UnityEngine;

public class Sheild : MonoBehaviour
{
    // 変数
    SpriteRenderer img;
    public int color = 0;
    [SerializeField] List<Sprite> Img;   //�摜
    public AudioClip sound1;
    public AudioSource audioSource;
    public static Sheild Instance { get; private set; }

    private void Awake()
    {
        // シングルトンの定義
        Instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        img = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // 盾の色を変更
        if (Input.GetKey(KeyCode.Z))
            ChangeSheildColor(COLOR.RED);
        if (Input.GetKey(KeyCode.X))
            ChangeSheildColor(COLOR.GREEN);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        // 敵機の情報を取得
        if (collision.TryGetComponent<Enemy>(out var obj))
        {
            // 接触した敵機と盾の色が同じでかつ、それが妨害ウイルスじゃないなら、
            if (IsHitFallingEnemy(obj) && obj.type != (int)EnemyConst.TYPE.JAMMER)
            {
                // ベクトルを反転
                Vector2 d = (collision.transform.position - transform.position).normalized;
                obj.vec = d;
                obj.on_hitting = true;
            }
        }
    }

    // 接触した敵機と盾の色が同じでかつ、それが敵機が降下中でないかどうか判定する
    bool IsHitFallingEnemy(Enemy obj)
    {
        if (!obj.on_hitting && obj.color == color)
            return true;
        return false;
    }

    // 盾の色を変更する
    void ChangeSheildColor(COLOR n)
    {
        img.sprite = Img[(int)n];
        color = (int)n;
        audioSource.Play();
    }
}