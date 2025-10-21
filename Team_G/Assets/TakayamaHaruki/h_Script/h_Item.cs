using System.Collections.Generic;
using UnityEngine;

public class h_Item : MonoBehaviour
{
    [SerializeField] List<GameObject> itemList;
    Vector2 v;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
        Debug.Log("ƒAƒCƒeƒ€");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        int randitem = Random.Range(0, itemList.Count);
        v = new Vector2(collision.gameObject.transform.position.x, collision.gameObject.transform.position.y);

        if (collision.gameObject.GetComponent<g_enemy>().OnHitting != true)
        {
            Instantiate(itemList[randitem], v , Quaternion.identity);
        }
    }
}
