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
        pm = go.GetComponent<IPhazeManager>();
        ChangeNextTips(0);
    }


    void Update()
    {
        if (Player.Instance.health > 0)
        {
            timer++;
            if (timer == 300)
            {
                if (pm.phase == 0)
                {
                    if (ch) ChangeNextTips(0);
                    if (!ch) ChangeNextTips(1);
                    timer = 0;
                    ch = !ch;
                }
                else
                {
                    if (tips.text.Length == 2)
                    {
                        ChangeNextTips(pm.phase + 1);
                    }
                    else
                    {
                        if (ch) ChangeNextTips(0);
                        if (!ch) ChangeNextTips(1);
                        timer = 0;
                        ch = !ch;
                    }
                }
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