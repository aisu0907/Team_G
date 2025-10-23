using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class g_boss : CharacterBase
{
    Rigidbody2D rb;
    int timer;
    int mode = 0;
    bool down = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // CharacterBase�p��
        Health = 1;     //�̗�
        Speed = 3.0f;   //�ړ����x

        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (mode)
        {
            case 0:
                timer++;
                break;
            case 1:
                rush();
                break;
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && collision.gameObject.GetComponent<g_enemy>().OnHitting)
        {
            --Health;
            Destroy(collision.gameObject);
            if (Health == 0)
            {
                Destroy(gameObject);
                SceneManager.LoadScene("Result_Scene");
            }
        }
    }

    void rush()
    {
        
    }
}
