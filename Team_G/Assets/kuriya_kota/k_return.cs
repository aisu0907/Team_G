using UnityEngine;
using UnityEngine.SceneManagement;

public class k_return : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Return))
        {
            SceneManager.LoadScene("test_kuriya");
        }
    }
}
