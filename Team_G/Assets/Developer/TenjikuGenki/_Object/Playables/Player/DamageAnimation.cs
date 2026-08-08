using UnityEngine;

public class DamageAnimation : MonoBehaviour
{
    public bool CanHit => _canHit;
    [SerializeField] SpriteRenderer _spriteRenderer;
    [SerializeField] GameObject _camera;
    int blinks_max = 13;  //点滅する回数
    int damage_time = 10; //消滅タイミング
    int save_time = 5;   //表示タイム
    int shake_max = 8;   //画面の振動回数
    bool _canHit = true;        //ダメージ判定
    Color save_color;       //通常の色
    Color damage_color;     //ダメージ時の色
    int color_timer;        //色切り替えタイマー
    int color_count;        //色切り替え回数
    int shake_count;        //振動した回数
    float tmp_pos;
    bool right = true;

    void Start()
    {

        //被弾
        color_count = 0;
        color_timer = 0;
        shake_count = 0;
        save_color = new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, _spriteRenderer.color.a);
        damage_color = new Color(save_color.r, save_color.g, save_color.b, 0.5f);
    }

    public void Anime()
    {
        if (_canHit) return;

        color_timer++;

        if (shake_count < shake_max)
        {
            tmp_pos = _camera.transform.position.x;
            _camera.transform.position = new Vector3(right == true ? tmp_pos + 0.15f : tmp_pos - 0.15f, 0, -10);
            right = !right;
            shake_count++;
        }

        if (color_timer == save_time)
        {
            _spriteRenderer.color = save_color;//通常の色に変更
            color_count++;
        }

        if (color_timer >= damage_time)
        {
            _spriteRenderer.color = damage_color;//ダメージ時の色に変更
            color_count++;
            color_timer = 0;//タイマーリセット
        }

        //色切り替え回数が最大回数に達したら
        if (color_count >= blinks_max)
        {
            _spriteRenderer.color = save_color;//通常の色に変更
                                               //リセット
            color_timer = 0;
            color_count = 0;
            shake_count = 0;
            _canHit = true;
        }
    }

    public void Damaged()
    {
        _canHit = false;
    }
}
