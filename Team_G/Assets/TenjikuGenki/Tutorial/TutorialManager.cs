using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static UnityEditor.PlayerSettings;

public class TutorialManager : MonoBehaviour, IPhazeManager
{
    public int phase { get; set; } = 0;
    public bool is_change_color { get; set; } = false;
    [SerializeField] public GameObject window;
    [SerializeField] List<Sprite> window_img;
    bool is_window = true;
    [SerializeField] List<EnemyData> enemy_data;

    public GameObject enemy;
    public int enemy_speed;
    public int pop_time;
    public int key_time;
    public Vector2 enemy_spawn_pos;
    public int enemy_hit_count;

    //ƒ^ƒCƒ€Œn
    [SerializeField] private int pop_time_count;
    [SerializeField] private int key_time_count;

    private int enemy_color;
    private int save_hp;
    private int pop_num;
    private Image img;
    private bool pop_window;
    private bool hp_pop;
    private bool bomb_pop;
    private bool key_swicth;
    private bool start_window;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        img = window.GetComponent<Image>();
        img.sprite = window_img[0];
        window.SetActive(false);
        start_window = true;
        hp_pop = true;
        bomb_pop = true;
        phase = 0;
        enemy_color = 0;
        pop_num = 1;
        save_hp = Player.Instance.health;
    }

    // Update is called once per frame
    void Update()
    {
        if (pop_window)
            key_time_count++;

        if (Input.GetKeyDown(KeyCode.Y))
            SceneManager.LoadScene("PlayScene");

        if (!Player.Instance.start_anime && start_window)
        {
            window.SetActive(true);
            pop_window = true;
            start_window = false;
        }

        if (Input.GetKeyDown(KeyCode.Z) && pop_window)
        {
            if (!is_window && key_swicth && key_time < key_time_count)
            {
                window.SetActive(false);
                pop_window = false;
                is_change_color = true;
                phase++;
                key_time_count = 0;
            }

            if (is_window && key_swicth && key_time < key_time_count)
            {
                img.sprite = window_img[pop_num];
                is_window = false;
                key_time_count = 0;
            }

            key_swicth = false;
        }
        else
            key_swicth = true;

        if (phase >= 1)
        {
            pop_time_count++;
            if (pop_time <= pop_time_count)
            {
                var e = Instantiate(enemy, enemy_spawn_pos, Quaternion.identity).GetComponent<TutorialEnemy>();
                e.Init(new Vector2(0, -1), enemy_color, enemy_speed);

                if (enemy_color == 0)
                    enemy_color++;
                else
                    enemy_color--;

                pop_time_count = 0;
            }
        }

        if (Player.Instance.health != save_hp || enemy_hit_count >= 3)
        {
            if (hp_pop)
            {
                img.sprite = window_img[1];
                hp_pop = false;
                is_change_color = false;
                is_window = true;
                window.SetActive(true);
                pop_window = true;
                if (bomb_pop && enemy_hit_count >= 3)
                {
                    pop_num = 2;
                    bomb_pop = false;
                }
                else
                    is_window = false;
            }
            else if(bomb_pop && enemy_hit_count >= 3)
            {
                window.SetActive(true);
                pop_window = true;
                pop_num = 2;
                bomb_pop = false;
            }
        }
    }
}
