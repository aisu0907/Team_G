using UnityEngine;
using System.Collections.Generic;

public class DisplayItem : MonoBehaviour
{
    [SerializeField] List<Sprite> spr;
    SpriteRenderer img;

    // Update is called once per frame
    void Start()
    {
        img = GetComponent<SpriteRenderer>();
    }

    void SummonDisplay(Sprite _img)
    {
        img.sprite = _img;
    }
}
