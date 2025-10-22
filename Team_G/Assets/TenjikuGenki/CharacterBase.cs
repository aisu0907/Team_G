using UnityEngine;

public class CharacterBase : MonoBehaviour
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

    Vector2 GetPos()
    {
        return transform.position;
    }
}
