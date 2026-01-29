//Health.cs

using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    //ゲームオブジェクト
    private Image image_component;  //画像コンポーネント
    //HPグラフィック
    private Sprite hp3;
    private Sprite hp2;
    private Sprite hp1;
    private Sprite hp0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //HPの画像を設定
        hp3 = Resources.Load<Sprite>("HP3");
        hp2 = Resources.Load<Sprite>("HP2");
        hp1 = Resources.Load<Sprite>("HP1");
        hp0 = Resources.Load<Sprite>("HP0");

        // コンポーネントの取得
        image_component = GetComponent<Image>();
    }

    void Update()
    {
        //プレイヤーのHPを参照して表示する体力を決める
        switch (Player.Instance.health)
        {
            case 3: image_component.sprite = hp3; break;
            case 2: image_component.sprite = hp2; break;
            case 1: image_component.sprite = hp1; break;
            case 0: image_component.sprite = hp0; break;
        }
     }
 }
