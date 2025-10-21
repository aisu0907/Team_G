using UnityEngine;

public class UnitBase : MonoBehaviour
{
    public int Health = 1;
    public float Speed = 1.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public Vector2 GetPos()
    {
        return new Vector2(transform.position.x, transform.position.y);
    }
}
