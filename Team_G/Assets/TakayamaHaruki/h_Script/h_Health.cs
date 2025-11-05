using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI; // 追加

public class h_Health : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    //HPグラフィック
    private Texture2D hp3;
    private Texture2D hp2;
    private Texture2D hp1;
    private Texture2D hp0;

    private GameObject image_object;
    private Image image_component;
    private int hp;
    private int prevHp = -1;

    // 初期化関数
    void Start()
    {
        hp3 = Resources.Load("HP3") as Texture2D;
        hp2 = Resources.Load("HP2") as Texture2D;
        hp1 = Resources.Load("HP1") as Texture2D;
        hp0 = Resources.Load("HP0") as Texture2D;

        // オブジェクトの取得
        image_object = GameObject.Find("HP");
        // コンポーネントの取得
       image_component = image_object.GetComponent<Image>();
    }


    void Update()
    {
        hp = Player.Instance.health;
        if (hp == prevHp) return; // HPが変わらなければ何もしない
        prevHp = hp;

        Texture2D currentTex = null;
        switch (hp)
        {
            case 3: currentTex = hp3; break;
            case 2: currentTex = hp2; break;
            case 1: currentTex = hp1; break;
            case 0: currentTex = hp0; break;
        }

        if (currentTex != null)
        {
            image_component.sprite = Sprite.Create(
                currentTex,
                new Rect(0, 0, currentTex.width, currentTex.height),
                Vector2.zero
            );
        }
    }

}
