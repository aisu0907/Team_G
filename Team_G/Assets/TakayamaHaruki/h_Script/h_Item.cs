//h_Item.cs

using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;

public class Item : MonoBehaviour
{
    public Rigidbody2D rb;
    //�A�C�e���̊�b���
    public float item_fall_Velocity = -3.0f;  //�A�C�e���������x
    public int item_id = 0;                   //�A�C�e���̎��
    public int max_item_count = 5;            //�A�C�e���ݐϏ��
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
        
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //�A�C�e���̈ʒu�X�V
        rb.linearVelocity = new Vector2(0, item_fall_Velocity);
    }
    
}
