using System.Collections.Generic;
using UnityEngine;

public class t_Enemy_Spwan : MonoBehaviour
{
    [SerializeField] Transform pos;                 // �����ʒu
    [SerializeField] Transform pos2;                // �����ʒu
    float minX, maxX, minY, maxY;                   // �����͈�
    public List<GameObject> prefab;
    public List<EnemyData> enemy;
    public bool spawn_switch = true;

    int frame = 0;
    List<Sprite> Img = new List<Sprite>();
    [SerializeField] int generateFrame = 30;        // ��������Ԋu

    void Start()
    {
        //�X�|�[���ʒu�ݒ�
        minX = Mathf.Min(pos.position.x, pos2.position.x);
        maxX = Mathf.Max(pos.position.x, pos2.position.x);
        minY = Mathf.Min(pos.position.y, pos2.position.y);
        maxY = Mathf.Max(pos.position.y, pos2.position.y);
        spawn_switch = true;
    }

    void Update()
    {
        // Add Timer
        ++frame;

        if (spawn_switch)
        {
            if (frame > generateFrame)
            {
                // Decide Pos
                float posX = Random.Range(minX, maxX);
                float posY = Random.Range(minY, maxY);
                Vector2 pos = new Vector2(posX, posY);

                // Spawn Enemy
                int type = Random.Range(0, GameManager.Instance.faze == 0 ? 1 : 2);
                int color = Random.Range(0, 2);
                if (type == 0) { var e = Instantiate(prefab[type], pos, Quaternion.identity).GetComponent<ENormal>(); e.Init(enemy[type], new Vector2(0, -1), color, enemy[type].speed); }
                if (type == 1) { var e = Instantiate(prefab[type], pos, Quaternion.identity).GetComponent<EReflect>(); e.Init(enemy[type], new Vector2(0, -1), color, enemy[type].speed); }

                // Reset
                frame = 0;
            }
        }

        if(GameManager.Instance.frame % 400 == 1 && GameManager.Instance.frame >= 400)
        {
            float posX = Random.Range(minX, maxX);
            float posY = Random.Range(minY, maxY);
            Vector2 pos = new Vector2(posX, posY);

            var e = Instantiate(prefab[2], pos, Quaternion.identity).GetComponent<EJammer>();
            e.Init(enemy[2], new Vector2(0, -1),enemy[2].speed);
        }
    }
}