using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class information : MonoBehaviour
{
    [SerializeField] List<Sprite> img;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            SceneManager.LoadScene("PlayScene");

        }
    }
}
