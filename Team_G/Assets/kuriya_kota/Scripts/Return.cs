using UnityEngine;
using UnityEngine.SceneManagement;

public class Return : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        //Z�L�[���͂Ń^�C�g��
        if(Input.GetKey(KeyCode.Z))
        {
            SceneManager.LoadScene("Title");
        }
    }
}
