//h_Sheild_Item.cs

using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;

public class Sheild_Item : ItemBase
{
    public GameObject enemy_ref;

    //�A�C�e�����ʂ̏㏸��
    public int puls_bom = 1;             //�{���̎擾��
    public int heal_hp = 1;              //�񕜗�
    public float up_speed = 0.5f;        //�X�s�[�h�㏸��
    public float up_sheild = 0.5f;       //�V�[���h�͈͏㏸��
    public float up_reflect_speed = 0.5f;//���˃X�s�[�h�㏸��

    private int max_health = 3;//�ő�̗�  
    private int[] item_count;                       //�A�C�e���擾��   
    private int max_bom = 3;                        //�{���ő及����
    private Vector3 sheild_size;                    //�V�[���h�T�C�Y

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //�ϐ����Z�b�g
        item_count = new int[3];
        sheild_size = new Vector2(3, 3);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //�A�C�e���ɓ��������ꍇ
        if (collision.gameObject.tag == "Item")
        {
            //�A�C�e�����폜
            Destroy(collision.gameObject);

            //�X�s�[�h
            if (collision.GetComponent<Item>().item_id == speed_item)
                //�ݐϏ���ɒB���Ă��Ȃ������ꍇ
                if (item_count[speed_item] < collision.GetComponent<Item>().max_item_count)
                {
                    //�v���C���[�̈ړ��X�s�[�h���グ��
                    Player.Instance.Speed += up_speed;
                    Debug.Log(Player.Instance.Speed);
                    //�ݐσJ�E���g
                    item_count[speed_item]++;
                }

            //���˃X�s�[�h
            if (collision.GetComponent<Item>().item_id == reflect_item)
                //�ݐϏ���ɒB���Ă��Ȃ������ꍇ
                if (item_count[reflect_item] < collision.GetComponent<Item>().max_item_count)
                {
                    //���˃X�s�[�hup
                    enemy_ref.GetComponent<g_enemy>().speed += up_reflect_speed;
                    Debug.Log(enemy_ref.GetComponent<g_enemy>().speed);
                    //�ݐσJ�E���g
                    item_count[reflect_item]++;
                }

            //���˔͈�
            if (collision.GetComponent<Item>().item_id == sheild_item)
                //�ݐϏ���ɒB���Ă��Ȃ������ꍇ
                if (item_count[sheild_item] < collision.GetComponent<Item>().max_item_count)
                {
                    //�V�[���h�����ɑ傫������
                    sheild_size.x += up_sheild;
                    Sheild.Instance.transform.localScale = sheild_size;
                    Debug.Log(Sheild.Instance.transform.localScale);
                    //�ݐσJ�E���g
                    item_count[sheild_item]++;
                }

            //��
            if (collision.GetComponent<Item>().item_id == life_item)
                //�v���C���[�̗̑͂��ő傶��Ȃ��ꍇ
                if (max_health > Player.Instance.Health)
                    //�v���C���[�̗̑͂𑝂₷
                    Player.Instance.Health += heal_hp;

            //�{��
            if (collision.GetComponent<Item>().item_id == bomb_item)
                //�{�����������ő傶��Ȃ��ꍇ
                if (max_bom > Player.Instance.bom)
                {

                }
        }

    }
}

