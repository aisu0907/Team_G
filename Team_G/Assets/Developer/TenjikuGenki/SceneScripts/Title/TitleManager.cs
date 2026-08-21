using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Const;
using System.Collections;

public class TitleManager : MonoBehaviour
{
    [Range(0f, 1f)]
    public float bgm_vlome; //BGM音量

    [Range(0f, 1f)]
    public float cursor_vlome; //カーソル移動SE音量

    [Range(0f, 1f)]
    public float decision_vlome; //選択SE音量

    [SerializeField] List<GameObject> _options = new();
    int currentOption = 0;

    private h_AudioManager audio;
    private Coroutine coroutine;
    void Start()
    {
        currentOption = 0;

        audio = h_AudioManager.Instance; //省略用

        audio.PlayBGM(AudioConst.BGM_ID.TITLE_BGM, bgm_vlome); //BGMを鳴らす

        Vector2 pos = new(_options[currentOption].transform.position.x - 2.7f, _options[currentOption].transform.position.y);
        transform.position = pos;
    }

    public void Interact(InputAction.CallbackContext ctx)
    {

        if (ctx.performed)
        {
            if (coroutine == null)
            {
                //音を鳴らす
                audio.PlaySE(AudioConst.SE_ID.DECISION_SE, decision_vlome);

                coroutine = StartCoroutine(WaitSceneChange());
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
        //音を鳴らす
        audio.PlaySE(AudioConst.SE_ID.CURSOR_SE, cursor_vlome);

        Vector2 pos = new(_options[currentOption].transform.position.x - 2.7f, _options[currentOption].transform.position.y);
        transform.position = pos;
    }

    //シーン移動待機用コルーチン
    IEnumerator WaitSceneChange()
    {
        yield return new WaitForSeconds(0.5f);

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
    void EndGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
