using UnityEngine;

public class DataHolder : MonoBehaviour
{
    public static DataHolder Instance { get; private set; }

    // 変数の定義
    static public int[] player_took_item = new int[3];
    static public int game_phaze;

    void Awake()
    {
        // シーンを跨いでも、オブジェクトが破棄されないようにする
        DontDestroyOnLoad(gameObject);

        // シングルトン
        Instance = this;
    }

    static public void GetGameData()
    {
        // 盾からアイテムの情報を取得
        var sheild = Sheild.Instance.GetComponent<Sheild_Item>();
        for(int i = 0; i < 3; i++) player_took_item[i] = sheild.item_count[i];

        // フェーズの数値を取得
        game_phaze = GameManager.Instance.faze;
    }
}