using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SocialPlatforms.Impl;

public class Sheild : MonoBehaviour
{
    public Sprite RED;
    public Sprite GREEN;
    SpriteRenderer img;
    string SheildColor = "Red";

    public GameObject follow;
    Vector2 vec;

    GameObject Player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        img = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // í«è]èàóù
        vec = follow.transform.position;
        vec.y += 0.7f;
        transform.position = vec;

        // êFïœçXèàóù
        if (Input.GetKey(KeyCode.Z))
        {
            img.sprite = RED;
            SheildColor = "Red";
        }
        if (Input.GetKey(KeyCode.X))
        {
            img.sprite = GREEN;
            SheildColor = "Green";
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Red" || collision.gameObject.tag == "Green" || collision.gameObject.tag == "Blue")
        {
            if (collision.gameObject.tag == SheildColor)
            {
                collision.gameObject.GetComponent<fall>().speed *= -1.0f;
            }
            else
            {
                // îÌíeèàóù
                GameObject.Find("Player").GetComponent<Player>().Health --;
                Destroy(collision.gameObject);
                Debug.Log(GameObject.Find("Player").GetComponent<Player>().Health);
            }
        }
    }
}
