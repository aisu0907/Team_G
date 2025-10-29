using Unity.VisualScripting;
using UnityEngine;

public class Shield_Arrow : MonoBehaviour
{
    public GameObject arrow;  // 矢印
    public GameObject red;    // 赤のターゲット
    public GameObject green;  // 緑のターゲット

    public Vector2 vec;
   

    void Start()
    {
       
    }

    void Update()
    {
        if (Sheild.Instance.SheildColor == 0)
        {
            vec = red.transform.position;
            vec.y += 1.4f;
            transform.position = vec;
        }
        if (Sheild.Instance.SheildColor == 1)
        {
            vec = green.transform.position;
            vec.y += 1.4f;
            transform.position = vec;
        }
    }
}
