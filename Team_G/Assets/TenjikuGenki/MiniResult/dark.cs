using UnityEngine;

public class Dark : MonoBehaviour
{
    public static Dark Instance;

    void Awake()
    {
        Instance = this;
    }
}