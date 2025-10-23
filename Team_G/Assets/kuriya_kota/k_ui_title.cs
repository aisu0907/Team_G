using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class k_ui_title : MonoBehaviour
{
    public GameObject arrow;  // –îˆó
    public GameObject start;    
    public GameObject end;

    public int scene_arrow=0;



    public Vector2 vec;
   

    void Start()
    {
       
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.UpArrow)) {
            scene_arrow++; 
        }

        if (Input.GetKeyUp(KeyCode.DownArrow)) {
            scene_arrow--;
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
            switch (scene_arrow) {
                case 0:
                    SceneManager.LoadScene("k_Play_Scene");
                    break;
                case 1:
                    EndGame();
                    break;
            }
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
