using UnityEngine;

public class TopArea : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy" && collision.gameObject.GetComponent<g_enemy>().OnHitting)
        {
            Destroy(collision.gameObject);
        }
    }
}
