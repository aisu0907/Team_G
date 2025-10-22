using UnityEngine;

public class k_ui_shield : MonoBehaviour
{
    public Sprite shield_red;
    public Sprite shield_green;

    SpriteRenderer img;

    public GameObject shield;
    public GameObject red;
    public GameObject green;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        img = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<Sheild>().SheildColor == 0)//ê‘êFÇÃéû
        {
           // Color_switch = 1;
        }


    }
}
