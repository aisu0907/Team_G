using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public int type, color;
    public Vector2 vec;
    public float speed;
    public int score;
    protected int power;
    public bool on_hitting = false;

    public GameObject explode;
    public Slider bomb_gage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ;
    }

    // Update is called once per frame
    void Update()
    {
        ;
    }

    protected void Delete(Collider2D obj)
    {
        //オブジェクトを生成
        Instantiate(explode, transform.position, Quaternion.identity);
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
