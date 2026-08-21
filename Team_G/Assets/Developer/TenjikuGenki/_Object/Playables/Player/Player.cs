using Const;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : ObjBase
{
    // ----- プロパティ ----- //
    bool IsMoveLimit(Vector2 vec)
    {
        return transform.position.y > TopMoveLimit.position.y && vec.y > 0.0f ||
            transform.position.y < BottomMoveLimit.position.y && vec.y < 0.0f;
    }

    // ----- メンバ変数 ----- //
    [Header("▼ GameObject")]
    [SerializeField] GameObject explode;
    [SerializeField] GameObject flash;
    [SerializeField] Shield shield;

    [Header("▼ PlayerStatus")]
    public int health = 3;      //体力

    [Header("▼ Bom")]
    public int bom = 0;     //ボムの所持数
    public int max_bom = 0; //ボム最大所持数

    [Header("▼ Phisics")]
    [SerializeField] Transform TopMoveLimit;    // 移動制限（上）
    [SerializeField] Transform BottomMoveLimit; // 移動制限（下）
    bool isStop = false;
    public float item_up_speed;

    [Header("▼ Animations")]
    [SerializeField] StartAnimation _startAnime;
    [SerializeField] DamageAnimation _damageAnime;
    int timer = 0;

    public static Player Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();

        _states.Add(new MoveSpeed(3.0f));
    }

    // Update is called once per frame
    protected override void Update()
    {
        // ゲーム開始時のアニメーション中なら操作を中断
        if (!_startAnime.StartAnime()) return;

        // 体力が0以下なら終了
        if (health <= 0)
        {
            // ちょっと待つ
            if (++timer >= 120)
                SceneManager.LoadScene(SceneNames.Gameover);
            return;
        }

        // ダメージのアニメーション
        _damageAnime.Anime();

        //ESCでタイトルに戻る
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            SceneManager.LoadScene(SceneNames.Title);
        }

        // 中断
        if (isStop) return;

        // 盾の位置更新
        shield.transform.position = new Vector2(transform.position.x, transform.position.y + 0.8f);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IHitable>(out var e))
        {
            if (collision.TryGetComponent<IReflectable>(out var r) && r.Hitting) return;

            e.Hit();

            // ダメージのクールタイム中なら中断
            if (!_damageAnime.CanHit) return;

            // ビジュアル
            AudioManager.instance.PlaySound("PlayerDamage");

            // ヒット処理
            health -= e.Damage;
            _damageAnime.Damaged();
        }

        //アイテムに当たった場合
        if (collision.TryGetComponent<Item>(out var i))
        {
            //音を鳴らす
            AudioManager.instance.PlaySound("GetItem");
            Shield_Item.Instance.ItemGet(i, i.item_id);

            //アイテムを削除
            Destroy(i.gameObject);
        }
    }

    void FixedUpdate()
    {
        // これ以上移動できないなら、移動を中断
        if (IsMoveLimit(_rb.linearVelocity))
            _rb.linearVelocityY = 0;
    }

    public void Move(InputAction.CallbackContext ctx)
    {
        if (!(health > 0) && _startAnime.StartAnime()) return;

        // 移動処理
        Vector2 vec = ctx.ReadValue<Vector2>();

        // 移動に限界を設定する
        if (IsMoveLimit(vec)) vec.y = 0.0f;
        Vector2 speed = Vector2.one * (_states[(int)StateName.Speed].CurrentState + item_up_speed);
        _rb.linearVelocity = vec * speed;
    }

    public void Bom(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            if (!(health > 0)) return;

            if (bom > 0)
            {
                AudioManager.instance.PlaySound("bom", 1f);
                // "Enemy"タグがついたすべてのオブジェクトを取得
                GameObject[] objects = GameObject.FindGameObjectsWithTag("Enemy");

                // 各オブジェクトを削除
                foreach (GameObject obj in objects)
                {
                    Destroy(obj);
                    Instantiate(explode, obj.transform.position, Quaternion.identity);
                }
                Instantiate(flash, new Vector2(transform.position.x, transform.position.y), Quaternion.identity); //画面全体にフラッシュを生成

                //bomの数を減らす
                bom--;
            }
        }
    }

    /// <summary>
    /// プレイヤーにダメージを与える処理
    /// </summary>
    /// <param name="damage"></param>
    /// <param name="obj"></param>
    /// <param name="destroy"></param>
    public void Damage(int damage, GameObject obj = null, bool destroy = true)
    {
        // ダメージのクールタイム中なら中断
        if (!_damageAnime.CanHit) return;

        // ビジュアル
        AudioManager.instance.PlaySound("PlayerDamage");

        // ヒット処理
        health -= damage;
        _damageAnime.Damaged();
        if (destroy) Destroy(obj);
    }

    public void Stop()
    {
        _rb.linearVelocity = Vector2.zero;
        isStop = true;
    }
}