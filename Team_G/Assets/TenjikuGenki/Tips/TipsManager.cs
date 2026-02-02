using System.Collections.Generic;
using System.Xml;
using TMPro;
using UnityEngine;

public class TipsManager : MonoBehaviour
{
    [Header("âEâ∫Tips")]
    [SerializeField] Tips text;
    [SerializeField] int timer;
    bool ch = false;
    TextMeshProUGUI tips;
    [SerializeField] GameObject go;
    IPhazeManager pm;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tips = GetComponent<TextMeshProUGUI>();
        ChangeNextTips(0);
    }


    void Update()
    {
        pm = go.GetComponent<IPhazeManager>();

        if (Player.Instance.health > 0)
        {
            if(go.TryGetComponent<GameManager>(out var gm))
            {
                if ((pm.phase + 1) % 2 == 0)
                {
                    tips.color = new Color(255, 0, 0);
                }
                else
                {
                    tips.color = new Color(255, 255, 255);
                }

            }

            timer++;
            if (pm.phase == 8)
            {
                ChangeNextTips(2);
            }
            else if (timer == 300)
            {
                if (tips.text.Length == 2)
                {
                    //ChangeNextTips(pm.phase + 1);
                }
                else
                {
                    if (ch) ChangeNextTips(0);
                    if (!ch) ChangeNextTips(1);
                    timer = 0;
                    ch = !ch;
                }
            }

            if(go.TryGetComponent<GameManager>(out var game_m))
            {
                if (gm.phase == 0)
                {
                    if (timer == 300)
                    {
                        if (ch) ChangeNextTips(0);
                        if (!ch) ChangeNextTips(1);
                        timer = 0;
                        ch = !ch;
                    }
                }
                else
                    ChangeNextTips(pm.phase + 1);
            }
        }
    }

    /// <summary>
    /// TipsÇnî‘ñ⁄Ç…ïœçX
    /// </summary>
    /// <param name="n"></param>
    void ChangeNextTips(int n)
    {
        tips.text = text.text[n];
    }
}