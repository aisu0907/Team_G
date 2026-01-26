//シールド用の矢印

using Unity.VisualScripting;
using UnityEngine;

public class ShieldArrow : MonoBehaviour
{
    public GameObject red;    // 赤のターゲット
    public GameObject green;  // 緑のターゲット

    public Vector2 vec;

    void Update()
    {
        if (Sheild.Instance.color == 0)
        {
            vec = red.transform.position;
            vec.y += 1.4f;
            transform.position = vec;
        }
        if (Sheild.Instance.color == 1)
        {
            vec = green.transform.position;
            vec.y += 1.4f;
            transform.position = vec;
        }
    }
}
