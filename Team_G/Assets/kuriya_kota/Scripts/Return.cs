using UnityEngine;
using UnityEngine.SceneManagement;

public class Return : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // Zならコンテニュー
        if(Input.GetKeyUp(KeyCode.Z))
        {
            SceneManager.LoadScene("Play_Scene");
        }

        // Xならタイトルに戻る
        if(Input.GetKeyUp(KeyCode.X))
        {
            SceneManager.LoadScene("Title");
        }
    }
}
