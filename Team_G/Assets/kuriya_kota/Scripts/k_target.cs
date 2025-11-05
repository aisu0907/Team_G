using System.Collections;
using UnityEngine;

public class k_target : MonoBehaviour
{
    public GameObject prefab;   // 呼び出すオブジェクト
    public float blinkInterval = 0.05f; // 点滅間隔
    public float lifeTime = 3f; // 生成までの時間

    private SpriteRenderer sr;
    private bool visible = true;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        StartCoroutine(BlinkAndSpawn()); //
    }

    IEnumerator BlinkAndSpawn()
    {
        float timer = 0f;

        while (timer < lifeTime)
        {
            visible = !visible;
            sr.enabled = visible;
            yield return new WaitForSeconds(blinkInterval);
            timer += blinkInterval;
        }

        Instantiate(prefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
