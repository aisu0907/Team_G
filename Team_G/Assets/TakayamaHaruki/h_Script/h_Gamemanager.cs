using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject boss;
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

        if (frame == 600)
        {
            spawner.GetComponent<t_Enemy_Spwan>().spawn_switch = false;
            item_drop.GetComponent<Item_Drop>().drop_switch = false;
            Instantiate(boss, new Vector2(-2, 3), Quaternion.identity);
        }

        if (g_boss.Instance.Health <= 0)
        {
            SceneManager.LoadScene("Result_Scene");
        }
    }
}
