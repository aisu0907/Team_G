//BossAttack.cs

using UnityEngine;

public class BossAttack : MonoBehaviour, IHitable
{
    [Header("▼Range Attack Setting")]
    public int Damage => damage;
    public int damage;//ダメージ
    public float damage_interval;//ダメージ間隔
    public float display_end;//表示終了
    [Header("▼Audio Setting")]

    public AudioClip attack_sound;

    private AudioSource attack_audio;
    private float display_time;//表示時間　　　　　　　　　　　　　　　　　　　　　　　　　　　
    private float damage_time; //ダメージタイム
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
        damage_time += Time.deltaTime;
        display_time += Time.deltaTime;

        //表示時間が終了したまたはボスが死んでいた場合
        if (display_time >= display_end || h_Boss.Instance.health <= 0)
            Destroy(gameObject); //範囲攻撃を削除
    }

    public void Hit()
    {
        //プレイヤーに触れた場合
        if (damage_interval <= damage_time)
        {
            damage_time = 0; //タイムリセット
        }
    }
}
