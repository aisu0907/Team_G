using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Shield : MonoBehaviour
{
    // ----- プロパティ ----- //
    public COLOR ShieldColor => _color;

    // ----- メンバ変数 ----- //
    [Header("▼ ChangeColor")]
    [SerializeField] SpriteRenderer _img;
    [SerializeField] List<Sprite> _shield_colors;
    COLOR _color = COLOR.RED;
    public bool canChange = false;

    [Header("▼ UI")]
    [SerializeField] Image shield_ui_obj;
    [SerializeField] List<Sprite> shields_ui_img;

    // ----- シングルトン ----- //
    public static Shield Instance;

    void Awake()
    {
        Instance = this;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // 敵機の情報を取得
        if (collision.TryGetComponent<IReflectable>(out var enemy))
        {
            if (enemy.Hitting) return;

            // 接触した敵機と盾の色が同じでかつ、それが妨害ウイルスじゃないなら、
            if (enemy.Color == _color && !enemy.Hitting)
            {
                // ベクトルを反転
                Vector2 d = (collision.transform.position - transform.position).normalized;
                enemy.Reflect(d, true);
                AudioManager.instance.PlaySound("ReflectEnemy", 0.4f);
            }
        }
    }

    // 盾の色を変更する
    public void ChangeShieldColor(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            _color = _color == COLOR.RED ? COLOR.GREEN : COLOR.RED;
            _img.sprite = _shield_colors[(int)_color];
            AudioManager.instance.PlaySound("ShieldChange");
        }
    }
}