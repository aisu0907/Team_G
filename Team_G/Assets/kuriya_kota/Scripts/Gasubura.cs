using UnityEngine;

public class Gasubura : MonoBehaviour,IDamageable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject,1);
    }
    public void Damage()
    {
        Player.Instance.health-=2;
    }
}
