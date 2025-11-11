using UnityEngine;
using System.Collections.Generic;

public class DisplayItem : MonoBehaviour
{
    [SerializeField] List<Sprite> spr;

    private void Awake()
    {
        ;
    }

    // Update is called once per frame
    void Start()
    {
        ;
    }

    public void SummonDisplay(Sprite _img)
    {
        var img = GetComponent<SpriteRenderer>();
        img.sprite = _img;
        Destroy(gameObject, 3);
    }
}
