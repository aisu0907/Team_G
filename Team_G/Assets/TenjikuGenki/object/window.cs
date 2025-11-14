using UnityEngine;
using UnityEngine.UIElements;


public class Window : MonoBehaviour
{
    // Update is called once per frame
    void Start()
    {
        Destroy(gameObject, 5);
        int pos = Random.Range(0, 2);
        if (pos == 0) transform.position = WindowConst.POS_1;
        if (pos == 1) transform.position = WindowConst.POS_2;
        if (pos == 2) transform.position = WindowConst.POS_3;
    }
}
