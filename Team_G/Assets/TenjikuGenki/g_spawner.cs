using System.Collections.Generic;
using UnityEngine;

public class t_Enemy_Spwan : MonoBehaviour
{
    public GameObject enemy;    // �����I�u�W�F�N�g
    [SerializeField] Transform pos;                 // �����ʒu
    [SerializeField] Transform pos2;                // �����ʒu
    float minX, maxX, minY, maxY;                   // �����͈�

    int frame = 0;
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

            //�E�C���X���X�|�[��
            //EnemyManager(new Vector2(posX,posY)
        }
    }
}
