using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UIElements;
using UnityEngine.Animations;

public class Player : MonoBehaviour
{
    Rigidbody2D rbody; 
    float axisH = 0.0f; //横ベクトル
    float axisV = 0.0f; //縦ベクトル

    public GameObject PreSheild;  //盾
    GameObject Sheild;  //盾

    public float speed = 3.0f;  //移動速度
    public int Health = 3;      //体力

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // RigidBody2Dから情報を取得する
        rbody = this.GetComponent<Rigidbody2D>();

        // 盾の生成
        Sheild = Instantiate(PreSheild, transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        // キー取得
        axisH = Input.GetAxisRaw("Horizontal");
        axisV = Input.GetAxisRaw("Vertical");

        // 追従処理
        Sheild.transform.position = new Vector2(transform.position.x, transform.position.y + 0.8f);
    }

    void FixedUpdate()
    {
        //移動の適用
        rbody.linearVelocity = new Vector2(axisH * speed, axisV * speed);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
            if (!collision.gameObject.GetComponent<g_enemy>().OnHitting)
            {
                // 被弾処理
                GameObject.Find("Player").GetComponent<Player>().Health--;
                Destroy(collision.gameObject);
                Debug.Log(Health);
        }
    }
}
