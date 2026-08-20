using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Information : MonoBehaviour
{
    public void Interact(InputAction.CallbackContext ctx)
    {
        SceneManager.LoadScene("TutorialScene");
    }
}
