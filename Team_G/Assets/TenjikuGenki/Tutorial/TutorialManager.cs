using UnityEngine;
using System.Collections.Generic;

public class TutorialManager : MonoBehaviour, IPhazeManager
{
    public int phase { get; set; } = 0;
    public bool is_change_color { get; set; } = false;
    [SerializeField] GameObject window;
    [SerializeField] List<Sprite> window_img;
    bool is_window = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (is_window)
            {
                window.SetActive(false);
                phase++;
                is_window = false;
            }
        }
    }
}
