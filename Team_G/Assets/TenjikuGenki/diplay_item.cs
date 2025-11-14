using UnityEngine;
using System.Collections.Generic;

public class DisplayItem : MonoBehaviour
{
    // •Ï”
    float size = 1;

    void Update()
    {
        // ƒTƒCƒY‚ğ•Ï‚¦‚é
        if (size <= DisplayItemConst.MAX_SIZE) size += DisplayItemConst.ADD_SIZE;
        transform.localScale = new Vector2(size, size);
    }

    // ¢Š«
    public void SummonDisplay(Sprite _img)
    {
        var img = GetComponent<SpriteRenderer>();
        img.sprite = _img;
        Destroy(gameObject, 1);
    }
}
