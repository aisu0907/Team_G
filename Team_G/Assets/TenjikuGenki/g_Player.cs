using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    Rigidbody2D rbody;
    float axisH, axisV = 0.0f;
    public GameObject sheild;
    public int health = 3;     //�̗�
    public float speed = 3.0f;   //�ړ����x
    public int bom = 0;     //ボムの所持数
    public int bom_time = 0;//ボムのクールタイム
    public int max_bom = 0;
    private int frame = 0;

    public static Player Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // RigidBody2D��������擾����
        rbody = this.GetComponent<Rigidbody2D>();
        // ���̐���
        Instantiate(sheild);
        frame = 300;
    }

    // Update is called once per frame
    void Update()
    {
        // �L�[�擾
        axisH = Input.GetAxisRaw("Horizontal");
        axisV = Input.GetAxisRaw("Vertical");
        if (0.2 <= transform.position.y) axisV = -0.05f;
        if (transform.position.y <= -4.5) axisV = 0.05f;

        // �Ǐ]����
        Sheild.Instance.transform.position = new Vector2(transform.position.x, transform.position.y + 0.8f);

        //ボムの処理
        if (frame >= bom_time)
        {
            if (Input.GetKey(KeyCode.Space) && bom > 0)
            {
                frame = 0;
                // "Enemy"タグがついたすべてのオブジェクトを取得
                GameObject[] objects = GameObject.FindGameObjectsWithTag("Enemy");

                // 各オブジェクトを削除
                foreach (GameObject obj in objects)
                {
                    Destroy(obj);
                }

                //bomの数を減らす
                bom--;
            }
        }
        frame++;
    }

    void FixedUpdate()
    {
        // 移動処理
        rbody.linearVelocity = new Vector2(axisH * speed, axisV * speed);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // 衝突判定
        if (collision.TryGetComponent<IDamageable>(out var hit))
        {
            if (collision.TryGetComponent<Enemy>(out var e) && !e.on_hitting) hit.Damage();
            if (collision.TryGetComponent<Gasubura>(out var b)) b.Damage(); 
        }
    }

    void Hit()
    {
        //プレイヤーの体力が0以下の場合
        if (Player.Instance.health <= 0)
        {
            //ゲームオーバーシーンに移行
            SceneManager.LoadScene("Gameover_Scene");
        }
    }
}