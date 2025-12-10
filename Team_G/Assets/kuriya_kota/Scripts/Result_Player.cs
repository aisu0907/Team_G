using UnityEngine;

public class Result_Player : MonoBehaviour
{
    public float targetX = -3.2f;   // 到達したい位置
    public float speed = 5f;         // 右方向の移動速度

    void Update()
    {
        if (transform.position.x < targetX)
        {
            transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
        }
    }
}
