using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class g_boss : CharacterBase
{
    int timer;
    int mode = 0;
    bool down = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // CharacterBaseŒp³
        Health = 1;     //‘Ì—Í
        Speed = 3.0f;   //ˆÚ“®‘¬“x
    }

    // Update is called once per frame
    void Update()
    {
        //if (mode == 0)
        //{
        //    ++timer;
        //    if (timer >= 600) mode = 1;
        //}
        //else if (mode == 1)
        //{
        //    if(down)
        //    {
        //        Vector2 v = new Vector2(transform.position.x, transform.position.y - 30);
        //        transform.position = v;
        //        if(transform.position.y <= 100) down = false;
        //    }
        //    else
        //    {
        //        Vector2 v = new Vector2(transform.position.x, transform.position.y + 30);
        //        transform.position = v;
        //        if (transform.position.y >= 100) down = false;
        //    }
        //}
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
}
