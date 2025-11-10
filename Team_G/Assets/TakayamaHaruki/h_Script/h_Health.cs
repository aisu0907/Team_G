//h_Health.cs

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

    private GameObject image_object; //イメージオブジェクト
    private Image image_component;   //イメージコンポーネント
    private int hp; //現在hp
    private int prevHp = -1; //hp確認用

    // 初期化関数
    void Start()
    {
        //hpの画像を設定
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
        //プレイヤーの現在hpを代入
        hp = Player.Instance.health;
        if (hp == prevHp) return; // HPが変わらなければ何もしない
        prevHp = hp;

        Texture2D hp_img = null;

        //表示するhpを決める
        switch (hp)
        {
            case 3: hp_img = hp3; break;
            case 2: hp_img = hp2; break;
            case 1: hp_img = hp1; break;
            case 0: hp_img = hp0; break;
        }
        
        //hp_imgに画像が入っていれば表示
        if (hp_img != null)
        {
            image_component.sprite = Sprite.Create(
                hp_img,
                new Rect(0, 0, hp_img.width, hp_img.height),
                Vector2.zero
            );
        }
    }

}
