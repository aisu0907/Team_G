using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Const;

public class TitleManager : MonoBehaviour
{
    [SerializeField] List<GameObject> _options = new();
    int currentOption = 0;

    void Start()
    {
        
    }

    void Update()
    {

    }

    public void Interact(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            switch (currentOption)
            {
                case 0:
                    SceneManager.LoadScene(SceneNames.Information);
                    break;

                case 1:
                    EndGame();
                    break;
            }
        }
    }

    public void CursorUp(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            if (++currentOption >= _options.Count)
                currentOption = 0;

            Draw();
        }
    }

    public void CursorDown(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            if (--currentOption < 0)
                currentOption = _options.Count - 1;

            Draw();
        }
    }

    void Draw()
    {
        Vector2 pos = new(_options[currentOption].transform.position.x - 2.7f, _options[currentOption].transform.position.y);
        transform.position = pos;
    }

    void EndGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
