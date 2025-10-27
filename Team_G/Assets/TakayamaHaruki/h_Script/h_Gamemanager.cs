using UnityEngine;
using UnityEngine.SceneManagement;

public class h_GameManager : MonoBehaviour
{
    public int boss_health = 5;
    public string nextSceneName;
    public GameObject player;
    public GameObject spawner;
    public GameObject item_drop;

    private int frame = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Update()
    {
        if (player.GetComponent<Player>().Health <= 0)
        {
            SceneManager.LoadScene("Gameover_Scene");
        }
        if (player.GetComponent<Player>().Health <= 0)
        {
            Debug.Log("shinu");
        }

        frame++;

        if (frame >= 36000)
        {
            spawner.GetComponent<t_Enemy_Spwan>().spawn_switch = false;
            item_drop.GetComponent<Item_Drop>().drop_switch = false;
        }

        if (boss_health <= 0)
        {
            SceneManager.LoadScene("Result_Scene");
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {

    }
}
