using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class TutorialManager : MonoBehaviour, IPhazeManager
{
    public int phase { get; set; } = 0;
    public bool is_change_color { get; set; } = false;
    [SerializeField] GameObject window;
    [SerializeField] List<Sprite> window_img;
    bool is_window = true;
    [SerializeField] List<GameObject> enemy;
    [SerializeField] List<EnemyData> enemy_data;
    [SerializeField] GameObject spawner;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawner.GetComponent<EnemySpawn>().spawn_switch = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (is_window)
            {
                window.SetActive(false);
                phase++;
                is_window = false;
                is_change_color = true;

                if (phase == 1)
                {
                    var e = Instantiate(enemy[0], new Vector2(-2.5f, 4.5f), Quaternion.identity).GetComponent<ENormal>();
                    e.Init(enemy_data[0], new Vector2(0, -1), 0, e.speed);
                }
            }
        }
    }
}
