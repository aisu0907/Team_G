using UnityEngine;
using UnityEngine.SceneManagement;

public class Return : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        //Zキー入力でタイトル
        if(Input.GetKey(KeyCode.Z))
        {
            SceneManager.LoadScene("Title");
        }
    }
}
