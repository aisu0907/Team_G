using UnityEngine;

public class Sheild : MonoBehaviour
{
    public Sprite RED;
    public Sprite GREEN;
    SpriteRenderer img;
    public int SheildColor = 0;

    public GameObject follow;
    Vector2 vec;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        img = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // í«è]èàóù
        vec = follow.transform.position;
        vec.y += 0.7f;
        transform.position = vec;

        // êFïœçXèàóù
        if (Input.GetKey(KeyCode.Z))
        {
            img.sprite = RED;
            SheildColor = 0;
        }
        if (Input.GetKey(KeyCode.X))
        {
            img.sprite = GREEN;
            SheildColor = 1;
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
            if(!collision.gameObject.GetComponent<g_enemy>().OnHitting)
                if (collision.gameObject.GetComponent<g_enemy>().EnemyColor == SheildColor)
                {
                    if (collision.gameObject.GetComponent<g_enemy>().EnemyType != 2)
                    {
                        //îΩéÀèàóù
                        Vector2 d = collision.gameObject.transform.position - transform.position;
                        collision.gameObject.GetComponent<g_enemy>().v = d;
                        collision.gameObject.GetComponent<g_enemy>().OnHitting = true;
                    }
                }
    }
}
