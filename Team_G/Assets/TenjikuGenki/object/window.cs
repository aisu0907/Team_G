using UnityEngine;
using UnityEngine.UIElements;


public class Window : MonoBehaviour
{
    // Update is called once per frame
    void Start()
    {
        Destroy(gameObject, 5.0f);
        int pos = Random.Range(0, 2);
        if (pos == 0) transform.position = new Vector2(-3, 3);
        if (pos == 1) transform.position = new Vector2(-1, 1);
        if (pos == 2) transform.position = new Vector2(3.5f, 1.5f);
    }
}
