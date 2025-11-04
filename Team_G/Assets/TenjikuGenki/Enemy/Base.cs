using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int type, color;
    public Vector2 vec;
    public float speed;
    protected int score;
    protected int power;
    public bool on_hitting = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected void Delete(Collider2D obj)
    {
        Destroy(obj.gameObject);
        Destroy(gameObject);
    }

    public bool IsHitEnemy(GameObject obj)
    {
        if (obj.CompareTag("Enemy")) return obj.GetComponent<Enemy>().on_hitting;
        return false;
    }

    public void Damage()
    {
        Destroy(gameObject);
        Player.Instance.health--;
    }
}
