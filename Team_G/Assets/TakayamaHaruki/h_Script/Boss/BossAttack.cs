//BossAttack.cs

using UnityEngine;

public class BossAttack : MonoBehaviour
{
    [Header("▼Range Attack Setting")]
    public int damage;//ダメージ
    public int damage_interval;//ダメージ間隔
    public int Display_end;//表示終了
    [Header("▼Audio Setting")]
    public AudioClip attack_sound;


    private AudioSource attack_audio;
    private int Display_time;//表示時間　　　　　　　　　　　　　　　　　　　　　　　　　　　
    private int damage_time; //ダメージタイム
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //ダメージ間隔をリセット
        damage_time = damage_interval;
        attack_audio = GetComponent<AudioSource>();
        attack_audio.PlayOneShot(attack_sound);
    }

    // Update is called once per frame
    void Update()
    {
        //タイムカウント
        damage_time++;
        Display_time++;

        //表示時間が終了したまたはボスが死んでいた場合
        if (Display_time >= Display_end || h_Boss.Instance.health <= 0)
            Destroy(gameObject); //範囲攻撃を削除
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //プレイヤーに触れた場合
        if(collision.CompareTag("Player") && damage_interval <= damage_time)
        {
            damage_time = 0; //タイムリセット
            Player.Instance.Damage(damage, gameObject, false); //プレイヤーにダメージ
        }
    }
}
