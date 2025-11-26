using System.Collections.Generic;
using System.Xml;
using TMPro;
using UnityEngine;

public class TipsManager : MonoBehaviour
{
    [SerializeField] Tips text;
    [SerializeField] int timer;
    bool ch = false;
    TextMeshProUGUI tips;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tips = GetComponent<TextMeshProUGUI>();
        ChangeNextTips(0);
    }


    void Update()
    {
        timer++;
        if(GameManager.Instance.faze == 0)
        {
            if(timer == 300)
            {
                if (ch) ChangeNextTips(0);
                if (!ch) ChangeNextTips(1);
                timer = 0;
                ch = !ch;
            }
        }
        else
        {
            ChangeNextTips(GameManager.Instance.faze + 1);
        }
    }

    void ChangeNextTips(int n)
    {
        tips.text = text.text[n];
    }
}