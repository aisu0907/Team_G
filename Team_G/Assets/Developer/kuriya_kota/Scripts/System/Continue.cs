using UnityEngine;
using UnityEngine.SceneManagement;

public class Continue : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // Zならコンテニュー
        if(Input.GetKeyUp(KeyCode.Z))
        {
            SceneManager.LoadScene("PlayScene");
        }

        // Xならタイトルに戻る
        if(Input.GetKeyUp(KeyCode.X))
        {
            SceneManager.LoadScene("TitleScene");
            DataHolder.DataReset();
        }
    }
}
