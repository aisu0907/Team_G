using UnityEngine;
using System.Collections.Generic;

public class Window : MonoBehaviour
{
    [SerializeField]List<Vector2> pos;
    public AudioClip sound1;
    public AudioSource audioSource;

    // Update is called once per frame
    void Start()
    {
        Destroy(gameObject, 5);
        int index = Random.Range(0, pos.Count);
        transform.position = pos[index];
        audioSource.PlayOneShot(sound1);
    }
}
