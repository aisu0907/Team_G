using UnityEngine;

public class target : MonoBehaviour
{
    int timer = 0;
    int limit;
    public GameObject enemy;

    // Update is called once per frame
    void Update()
    {
        ++timer;
        if(timer >= limit)
        {
            GameObject Enemy = Instantiate(enemy);
            g_enemy e = Enemy.GetComponent<g_enemy>();
            e.Create(g_boss.Instance.transform.position,(transform.position - g_boss.Instance.transform.position).normalized, 0, 0, 10);
            Destroy(gameObject);
        }
    }
    
    public void init(int _limit)
    {
        limit = _limit;
    }
}
