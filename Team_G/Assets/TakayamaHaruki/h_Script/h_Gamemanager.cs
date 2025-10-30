using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject boss;     //�{�X�I�u�W�F�N�g
    public GameObject player;   //�v���C���[�I�u�W�F�N�g
    public GameObject spawner;  //�X�|�i�[�I�u�W�F�N�g
    public GameObject item_drop;//�A�C�e���I�u�W�F�N�g

    private int frame = 0;  //�t���[��

    // Start is called once before the first execution of Update after the MonoBehaviour is create

    private void Start()
    {
        spawner.GetComponent<t_Enemy_Spwan>().spawn_switch = true;     //�G�l�~�[�̏o����ON
        item_drop.GetComponent<Item_Drop>().drop_switch = true;        //�A�C�e���h���b�v��ON
    }

    private void Update()
    {
        //�v���C���[�̗̑͂�0�ȉ��̏ꍇ
        if (player.GetComponent<Player>().Health <= 0)
        {
            //�Q�[���I�[�o�[�V�[���Ɉڍs
            SceneManager.LoadScene("Gameover_Scene");
            Debug.Log("shinu");
        }

        //�t���[���J�E���g
        frame++;

        //30�b�o�ƋN��
        if (frame ==1800)
        {
            spawner.GetComponent<t_Enemy_Spwan>().spawn_switch = false;     //�G�l�~�[�̏o����OFF
            item_drop.GetComponent<Item_Drop>().drop_switch = false;        //�A�C�e���h���b�v��OFF
            Instantiate(boss, new Vector2(-2, 3), Quaternion.identity);     //�{�X������
        }
    }
}
