using UnityEngine;

public class h_Item : MonoBehaviour
{
    public Rigidbody rb;
    public float fall_Velocity = 3.0f;
    public int item_id;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2 (0, fall_Velocity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //�V�[���h�ɓ��������ꍇ
        if(collision.gameObject.tag == "Sheild")
        {
            //�A�C�e�����폜
            Destroy(this);
            //�X�s�[�h�A�b�v
            if (item_id == 0)
            {

            }
            else if(item_id == 1)
            {
                
            }
            else if(item_id == 2)
            {

            }
        }
    }
}
