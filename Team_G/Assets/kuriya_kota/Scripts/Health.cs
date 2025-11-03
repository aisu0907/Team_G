//HPの表示

using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    //HPのグラフィック設定
    public Sprite HP3;
    public Sprite HP2;
    public Sprite HP1;
    public Sprite HP0;

    SpriteRenderer img;


    //HPを取得する先のオブジェクト(プレイヤー)を入れる箱を作成
    public GameObject health;
   

    void Start()
    {
        img = GetComponent<SpriteRenderer>();
    }
    void Update()
    {

        //現在ＨＰの量によって表示する画像を差し替える
        int hp = Player.Instance.health;

        switch (hp) {
            case 3:
                img.sprite = HP3;
                break;
            case 2:
                img.sprite = HP2;
                break;
            case 1:
                img.sprite = HP1;
                break;
            case 0:
                img.sprite = HP0;
                break;
        }
    }
}
