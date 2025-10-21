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
        if (player.GetComponent<Player>().Health <= 0)
        {
            SceneManager.LoadScene("k_gameover");
        }
        if (player.GetComponent<Player>().Health <= 0)
        {
            Debug.Log("shinu");
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            boss_health--;
            Debug.Log("Boss health:"+boss_health);

            if(boss_health <= 0)
            {
                SceneManager.LoadScene("Result_Scene");
            }

           


        }

    }
}
