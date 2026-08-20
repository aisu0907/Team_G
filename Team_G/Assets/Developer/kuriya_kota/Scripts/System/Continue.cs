using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Const;

public class Continue : MonoBehaviour
{
    public void Interact(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            SceneManager.LoadScene(SceneNames.Play);
        }
    }
    public void Cancel(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            SceneManager.LoadScene(SceneNames.Title);
        }
    }
}
