using System.Collections.Generic;
using UnityEngine;

public class t_Enemy_Spwan : MonoBehaviour
{
    public GameObject enemy;    // �����I�u�W�F�N�g
    [SerializeField] Transform pos;                 // �����ʒu
    [SerializeField] Transform pos2;                // �����ʒu
    float minX, maxX, minY, maxY;                   // �����͈�
    public float Enemy_speed;

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
    }

    void Update()
    {
        ++frame;

        if (frame > generateFrame)
        {
            frame = 0;

            // �����_���Ŏ�ނƈʒu�����߂�
            float posX = Random.Range(minX, maxX);
            float posY = Random.Range(minY, maxY);

            GameObject Enemy = Instantiate(enemy);
            g_enemy e = Enemy.GetComponent<g_enemy>();
            //e.RandCreate(new Vector2(posX, posY), new Vector2(0, -3), 3);

            // Add Components
            Debug.Log(posX + "+" + +posY);
            e.EnemyType = Random.Range(0, 2);
            e.EnemyColor = Random.Range(0, 2);
            e.speed = -Enemy_speed;
            e.pos = new Vector2(posX, posY);
            e.vec = new Vector2(0, -3);
        }
    }
}
