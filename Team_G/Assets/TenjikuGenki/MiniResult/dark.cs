using UnityEngine;

public class dark : MonoBehaviour
{
    public static dark Instance;

    void Awake()
    {
        Instance = this;
    }
}