using UnityEngine;

public class Fps : MonoBehaviour
{
    public int fps = 60;�@//FPS

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //FPS�ݒ�
        Application.targetFrameRate = fps;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
