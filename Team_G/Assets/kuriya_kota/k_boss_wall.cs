using UnityEngine;
using UnityEngine.SceneManagement;

public class k_boss_wall : MonoBehaviour
{
    public int boss_health = 5;
    public string nextSceneName;
    public GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.gameObject);
        }

    }
}
