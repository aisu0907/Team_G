//FpsLimiter.cs

using UnityEngine;

public class Fps : MonoBehaviour
{
    [Header("Å•FPS Setting")]
    public int fps = 60;Å@//FPS

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //FPSê›íË
        Application.targetFrameRate = fps;
        Time.timeScale = 1.0f;
    }
}
