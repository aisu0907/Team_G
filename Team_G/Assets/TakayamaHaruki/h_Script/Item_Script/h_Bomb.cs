using UnityEngine;

public class h_Bomb : MonoBehaviour
{
    private int bomb_num;
    private int bomb_save;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bomb_num = Player.Instance.bom;
        bomb_save = bomb_num;
    }

    // Update is called once per frame
    void Update()
    {
        bomb_num = Player.Instance.bom;

        if(bomb_num != bomb_save)
        {

        }

        bomb_save = bomb_num;
    }
}
