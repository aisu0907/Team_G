//HP�̕\��

using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    //HP�̃O���t�B�b�N�ݒ�
    public Sprite HP3;
    public Sprite HP2;
    public Sprite HP1;
    public Sprite HP0;

    SpriteRenderer img;


    //HP���擾�����̃I�u�W�F�N�g(�v���C���[)�����锠���쐬
    public GameObject health;
   

    void Start()
    {
        img = GetComponent<SpriteRenderer>();
    }
    void Update()
    {

        //���݂g�o�̗ʂɂ���ĕ\������摜�������ւ���
        int hp = Player.Instance.Health;

        switch (hp) {
            case 3:
                img.sprite = HP3;
                break;
            case 2:
                img.sprite = HP2;
                break;
            case 1:
                img.sprite = HP1;
                break;
            case 0:
                img.sprite = HP0;
                break;
        }
    }
}
