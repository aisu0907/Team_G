using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Const;

public class TitleManager : MonoBehaviour
{
    [Range(0f, 1f)]
    public float bgm_vlome;

    [Range(0f, 1f)]
    public float cursor_vlome;

    [Range(0f, 1f)]
    public float decision_vlome;

    [SerializeField] List<GameObject> _options = new();
    int currentOption = 0;

    h_AudioManager audio;

    void Start()
    {
        audio = h_AudioManager.Instance; //È—ª—p

        audio.PlayBGM(AudioConst.BGM_ID.TITLE_BGM, bgm_vlome); //BGM‚ð–Â‚ç‚·
    }

    public void Interact(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            audio.PlaySE(AudioConst.SE_ID.DECISION_SE, decision_vlome);
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
        //
        audio.PlaySE(AudioConst.SE_ID.DECISION_SE, decision_vlome);

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
