using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [Header("▼ Shield")]
    SpriteRenderer img;
    public int color = 0;
    [SerializeField] List<Sprite> Img;   //�摜
    [SerializeField] GameObject go;
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
            if (pm.is_change_color == true)
                ChangeShieldColor(color == (int)COLOR.RED ? COLOR.GREEN : COLOR.RED);
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
                AudioManager.instance.PlaySound("ReflectEnemy",0.4f);
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
    void ChangeShieldColor(COLOR n)
    {
        img.sprite = Img[(int)n];
        color = (int)n;
        AudioManager.instance.PlaySound("ShieldChange");
    }
}