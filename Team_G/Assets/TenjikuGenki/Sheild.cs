using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SocialPlatforms.Impl;

public class Sheild : MonoBehaviour
{
    public Sprite RED;
    public Sprite GREEN;
    SpriteRenderer tmp_s;
    string SheildColor;

    public GameObject follow;
    Vector2 tmp_v;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tmp_s = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // í«è]èàóù
        tmp_v = follow.transform.position;
        tmp_v.y += 0.7f;
        transform.position = tmp_v;

        // êFïœçXèàóù
        if (Input.GetKey(KeyCode.Z))
        {
            tmp_s.sprite = RED;
            SheildColor = "Red";
        }
        if (Input.GetKey(KeyCode.X))
        {
            tmp_s.sprite = GREEN;
            SheildColor = "Green";
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == SheildColor)
        {
            collision.gameObject.GetComponent<fall>().speed = -1.0f;
        }
    }
}
