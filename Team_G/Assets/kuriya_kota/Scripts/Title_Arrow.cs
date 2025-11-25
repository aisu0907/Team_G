//シールド用のカーソル矢印

using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title_Arrow : MonoBehaviour
{
    public GameObject arrow;  // 矢印
    public GameObject start;    
    public GameObject end;

    public int scene_arrow=0;

    public Vector2 vec;

    public AudioClip sound1;
    public AudioClip sound2;
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>(); 
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.UpArrow)) {
            scene_arrow++;
            audioSource.PlayOneShot(sound1);
        }

        if (Input.GetKeyUp(KeyCode.DownArrow)) {
            scene_arrow--;
            audioSource.PlayOneShot(sound1);
        }

        if (scene_arrow < 0) scene_arrow = 1;

        if (scene_arrow > 1) scene_arrow = 0;

        if (scene_arrow==0)
        {
            vec = start.transform.position;
            vec.x -= 3.5f;
            transform.position = vec;
        }
        if (scene_arrow==1)
        {
            vec = end.transform.position;
            vec.x -= 2.3f;
            transform.position = vec;
        }

        if (Input.GetKeyUp(KeyCode.Z))
        {
            audioSource.PlayOneShot(sound2);
            StartCoroutine(WaitAndExecute(scene_arrow)); //コルーチンを呼び出す
        }
    }

    private System.Collections.IEnumerator WaitAndExecute(int selected)
    {
        // 決定音が鳴り終わるまで少し待つ
        yield return new WaitForSeconds(0.5f);

        switch (selected)
        {
            case 0:
                SceneManager.LoadScene("g_Play_Scene");
                break;
            case 1:
                EndGame();
                break;
        }
    }

    private void EndGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}