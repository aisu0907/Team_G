using UnityEngine;

public class g_boss : CharacterBase
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // CharacterBase�p��
        Health = 3;     //�̗�
        Speed = 3.0f;   //�ړ����x
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && collision.gameObject.GetComponent<g_enemy>().OnHitting)
        {
            --Health;
            Debug.Log("gue-----------");
            Destroy(collision.gameObject);
            if (Health == 0) Destroy(gameObject);
        }
    }
}
