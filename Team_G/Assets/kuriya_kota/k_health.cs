//HP‚Ì•\Ž¦

using System.Collections.Generic;
using UnityEngine;

public class k_health : MonoBehaviour
{
    public Sprite HP3;
    public Sprite HP2;
    public Sprite HP1;
    public Sprite HP0;

    SpriteRenderer img;

    public GameObject health;
   

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        img = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
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
