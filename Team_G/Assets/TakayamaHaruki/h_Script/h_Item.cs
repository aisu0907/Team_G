using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UIElements;

public class Item : MonoBehaviour
{
    public Rigidbody rb;
    public float fall_Velocity = 3.0f;
    public int item_id = 0;
    public float reflect_speed = 0;

    private int [] item_count = { 0 };   
    private Vector2 v;
    public static Item Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = new Vector2 (0, fall_Velocity);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        //シールドに当たった場合
        if(collision.gameObject.tag == "Red")
        {
            //アイテムを削除
            Destroy(gameObject);

            //スピードアップ
            if (item_id == 0)
            {

            }
            //反射スピードアップ
            else if (item_id == 1)
            {
                if (item_count[0] < 5)
                {
                    item_count[0]++;
                    reflect_speed += 0.5f;
                    g_enemy.Instance.enemy_speed += reflect_speed;
                    
                }
            }
            else if (item_id == 2)
            {

            }
        }
    }
}
