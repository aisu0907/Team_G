using TMPro;
using UnityEngine;

public class Get_Item : MonoBehaviour
{
    public TMP_Text itemText; // Inspector‚ÅƒZƒbƒg

    public bool delete_swich=false;

    int timer = 0;

    public static Get_Item Instance { get; private set; }
    void Awake()
    {
        Instance = this;
    }

    public void SetText(string text)
    {
        transform.position = new Vector3(Player.Instance.transform.position.x, Player.Instance.transform.position.y, 0);
        itemText.text = text;
        delete_swich = true;
    }
    private void Update()
    {
        if(delete_swich)timer++;
        if(timer>60)
        {
            delete_swich = false;
            timer = 0;
            Destroy(gameObject);
        }
    }
}
