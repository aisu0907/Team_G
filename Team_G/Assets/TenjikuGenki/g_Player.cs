using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    Rigidbody2D rbody; 
    float axisH = 0.0f; //���x�N�g��
    float axisV = 0.0f; //�c�x�N�g��

    public float speed = 3.0f;  //�ړ����x
    public int Health = 3;  //�̗�

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //RigidBody2D��������擾����
        rbody = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // �L�[�擾
        axisH = Input.GetAxisRaw("Horizontal");
        axisV = Input.GetAxisRaw("Vertical");
    }
    void FixedUpdate()
    {
        //�ړ��̓K�p
        rbody.linearVelocity = new Vector2(axisH * speed, axisV * speed);
    }
}
