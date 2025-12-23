//h_Item.cs

using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;

public class Item : MonoBehaviour
{

    //アイテムの基礎情報
    public float item_fall_Velocity = -3.0f;  //アイテム落下速度
    public int item_id = 0;                   //アイテムの種類
    public int max_item_count = 5;            //アイテム累積上限
   
    private Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
        
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //アイテムの位置更新
        rb.linearVelocity = new Vector2(0, item_fall_Velocity);
    }
    
}
