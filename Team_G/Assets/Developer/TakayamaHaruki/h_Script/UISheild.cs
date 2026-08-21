using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UISheild : MonoBehaviour
{
    [SerializeField] List<Sprite> shield_color; 
    
    private Image shield;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        shield = GetComponent<Image>();
        shield.sprite = shield_color[(int)Shield.Instance.ShieldColor];
    }

    // Update is called once per frame
    void Update()
    {
        //ëŒâûÇµÇΩêFÇ…èÇÇÃêFÇïœÇ¶ÇÈ
        if (Shield.Instance.ShieldColor == COLOR.RED)
            shield.sprite = shield_color[(int)COLOR.RED];
        else if(Shield.Instance.ShieldColor == COLOR.GREEN)
            shield.sprite = shield_color[(int)COLOR.GREEN];
    }
}
