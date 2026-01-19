//h_Health.cs

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI; // 追加

public class h_Health : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    //HPグラフィック
    private Sprite hp3;
    private Sprite hp2;
    private Sprite hp1;
    private Sprite hp0;

    private GameObject image_object; //画像オブジェクト
    private Image image_component;   //画像コンポーネント
    private int hp; //現在hp
    private int prevHp = -1; //hp確認用
    private Texture2D hp_img;
    // 初期化関数
    void Start()
    {
        //hpの画像を設定
        hp3 = Resources.Load<Sprite>("HP3");
        hp2 = Resources.Load<Sprite>("HP2");
        hp1 = Resources.Load<Sprite>("HP1");
        hp0 = Resources.Load<Sprite>("HP0");

        // オブジェクトの取得
        image_object = GameObject.Find("HP");
        // コンポーネントの取得
        image_component = image_object.GetComponent<Image>();
    }


    void Update()
    {
        //プレイヤーの現在hpを代入
        hp = Player.Instance.health;
        if (hp == prevHp) return; // HPが変わらなければ何もしない
        prevHp = hp;

        //画像をリセット
        hp_img = null;

        //hp表示
        hp_display();

        //表示するhpを決める
        switch (hp)
        {
            case 3: image_component.sprite = hp3; break;
            case 2: image_component.sprite = hp2; break;
            case 1: image_component.sprite = hp1; break;
            case 0: image_component.sprite = hp0; break;
        }

        //hp_imgに画像が入っていれば表示
        if (hp_img != null)
        {
            //画像を生成
            image_component.sprite = Sprite.Create(
                hp_img,
                new Rect(0.0f, 0.0f, hp_img.width, hp_img.height),
                new Vector2(0.5f, 0.5f),
                1.0f
            );
        }
    }

    //hp表示
    public void hp_display()
    {
      
    }
}
