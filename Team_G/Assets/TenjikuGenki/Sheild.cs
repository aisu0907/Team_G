using UnityEngine;

public class Sheild : MonoBehaviour
{
    public Sprite RSheild;
    public Sprite GSheild;
    public GameObject follow;
    Vector2 tmp_v;
    SpriteRenderer tmp_s;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tmp_s = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // ’Ç]ˆ—
        tmp_v = follow.transform.position;
        tmp_v.y += 0.7f;
        transform.position = tmp_v;

        // F•ÏXˆ—
        if (Input.GetKey(KeyCode.Z))
        {
            tmp_s.sprite = RSheild;
        }
        if (Input.GetKey(KeyCode.X))
        {
            tmp_s.sprite = GSheild;
        }
    }
}
