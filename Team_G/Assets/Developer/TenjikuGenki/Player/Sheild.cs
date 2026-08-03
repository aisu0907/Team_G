using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shield : MonoBehaviour
{
    [Header("▼ Shield")]
    SpriteRenderer img;
    public int color = 0;
    [SerializeField] List<Sprite> Img;   //�摜
    [SerializeField] GameObject go;
    [SerializeField] Image shield_ui_obj;
    [SerializeField] List<Sprite> shields_ui_img;
    IPhazeManager pm;

    public static Shield Instance { get; private set; }

    private void Awake()
    {
        // シングルトンの定義
        Instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        img = GetComponent<SpriteRenderer>();
        pm = go.GetComponent<IPhazeManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // 盾の色を変更
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (pm.is_change_color == true)
            {
                ChangeShieldColor(color == (int)COLOR.RED ? COLOR.GREEN : COLOR.RED);
                shield_ui_obj.sprite = shields_ui_img[(int)color];
            }
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        // 敵機の情報を取得
        if (collision.TryGetComponent<IReflectable>(out var enemy))
        {
            if (enemy.Hitting) return;

            // 接触した敵機と盾の色が同じでかつ、それが妨害ウイルスじゃないなら、
            if (enemy.Color == (COLOR)color && !enemy.Hitting)
            {
                // ベクトルを反転
                Vector2 d = (collision.transform.position - transform.position).normalized;
                enemy.Reflect(d, true);
                AudioManager.instance.PlaySound("ReflectEnemy", 0.4f);
            }
        }
    }

    // 盾の色を変更する
    void ChangeShieldColor(COLOR n)
    {
        img.sprite = Img[(int)n];
        color = (int)n;
        AudioManager.instance.PlaySound("ShieldChange");
    }
}