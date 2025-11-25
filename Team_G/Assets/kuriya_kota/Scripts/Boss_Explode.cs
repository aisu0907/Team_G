//using UnityEngine;
//using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;



public class Boss_Explode : MonoBehaviour
{
    public float size = 0.0f;
    public float max_size = 0.8f;
    public float add_size = 0.1f;

    

    public int timer = 0;

    public AudioClip sound1;
    public AudioClip sound2;

    public GameObject explode;
    public GameObject flash;

    AudioSource audioSource;



    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        timer++;
        if (size <= max_size) size += add_size;
        transform.localScale = new Vector2(size, size);



        if (timer == 10) random();
        if (timer == 30)  random();
        if (timer == 40)  random();
        if (timer == 50)  random();
        if (timer == 80)  random();
        if (timer == 110) random();
        if (timer == 120) random();
        if (timer == 180)
        {
            audioSource.PlayOneShot(sound1);
        }
        if(timer ==220) StartCoroutine(DelayedFlash());

        //シーン見込み予定
        if (timer > 400) Destroy(gameObject);



    }

    private void random()
    {
        int x = Random.Range(-3, 4);
        int y = Random.Range(-3, 4);
        Instantiate(explode, new Vector2(k_boss.Instance.transform.position.x + x, k_boss.Instance.transform.position.y + y), Quaternion.identity);
    }


    private IEnumerator DelayedFlash()
    {
        int waitFrames = 60; // 待ちたいフレーム数

        for (int i = 0; i < waitFrames; i++)
        {
            yield return null; // 1フレーム待つ
        }
        audioSource.PlayOneShot(sound2);
        //screenFlash.Flash();
        Instantiate(flash, new Vector2(k_boss.Instance.transform.position.x, k_boss.Instance.transform.position.y), Quaternion.identity);
        Destroy(k_boss.Instance.gameObject);

        for (int i = 0; i < waitFrames; i++)
        {
            yield return null; // 1フレーム待つ
        }
        SceneManager.LoadScene("K_Result");
    }
}
