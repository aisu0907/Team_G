using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiteleScroll : MonoBehaviour
{
    public float speed = 0.5f;          // �X�N���[�����x
    public float resetPositionY = -10f; // �ǂ��܂ŉ��������烊�Z�b�g���邩
    public float startPositionY = 10f;  // ��ɖ߂��ʒu

    void Update()
    {
        // ���Ɉړ�
        transform.position -= new Vector3(0, speed * Time.deltaTime, 0);

        // ���ʒu�܂ŉ����������֖߂�
        if (transform.position.y <= resetPositionY)
        {
            transform.position = new Vector3(transform.position.x, startPositionY, transform.position.z);
        }
    }
}
