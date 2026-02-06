using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [Header("▼ SpawnPosition")]
    [SerializeField] Transform pos;                 //スポナー位置
    [SerializeField] Transform pos2;                //スポナー位置

    [Header("▼ SpawnComponent")]
    [SerializeField] List<PopEnemyList> enemy_list; //スポーンする敵
    [SerializeField] int generateFrame;             //生成速度
    public List<GameObject> prefab;
    public int jammer_spawn; //邪魔ウイルス生成速度
    public bool spawn_switch = true;
    float minX, maxX, minY, maxY;                   //生成位置
    public int counter = 0;

    List<Sprite> Img = new List<Sprite>();
    private int frame; //ウイルス生成タイマー
    private int jammer_timer; //邪魔ウイルスタイマー

    public static EnemySpawn Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        //リセット
        //タイマー
        frame = 0;
        jammer_timer = 0;
        //座標
        minX = Mathf.Min(pos.position.x, pos2.position.x);
        maxX = Mathf.Max(pos.position.x, pos2.position.x);
        minY = Mathf.Min(pos.position.y, pos2.position.y);
        maxY = Mathf.Max(pos.position.y, pos2.position.y);
    }

    void Update()
    {
        // Add Timer
        ++frame;

        if (spawn_switch)
        {
            if (frame > enemy_list[GameManager.Instance.phase / 2].spawn_timer)
            {
                // Decide Pos
                float posX = Random.Range(minX, maxX);
                float posY = Random.Range(minY, maxY);
                Vector2 pos = new Vector2(posX, posY);

                // Spawn Enemy
                int type = GameManager.Instance.phase == 0 ? 0 : Random.Range(0, 2);
                int color = Random.Range(0, 2);
                if (type == 0) { var e = Instantiate(prefab[type], pos, Quaternion.identity).GetComponent<ENormal>();  e.Init(enemy_list[GameManager.Instance.phase / 2].list[type], new Vector2(0, -1), color, enemy_list[GameManager.Instance.phase / 2].list[type].speed); }
                if (type == 1) { var e = Instantiate(prefab[type], pos, Quaternion.identity).GetComponent<EReflect>(); e.Init(enemy_list[GameManager.Instance.phase / 2].list[type], new Vector2(0, -1), color, enemy_list[GameManager.Instance.phase / 2].list[type].speed); }

                // Reset
                frame = 0;
                //counter++;
            }

            // 妨害ウイルスを出現
            if (GameManager.Instance.phase == 4)
            {
                jammer_timer++;
                if (jammer_timer >= jammer_spawn)
                {
                    float posX = Random.Range(minX, maxX);
                    float posY = Random.Range(minY, maxY);
                    Vector2 pos = new Vector2(posX, posY);

                    var e = Instantiate(prefab[2], pos, Quaternion.identity).GetComponent<EJammer>();
                    e.Init(enemy_list[2].list[2], new Vector2(0, -1), enemy_list[2].list[2].speed);
                    jammer_timer = 0;
                    //counter++;
                }
            }
        }
    }
}