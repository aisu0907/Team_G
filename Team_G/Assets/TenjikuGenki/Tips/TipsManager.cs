using System.Collections.Generic;
using System.Xml;
using TMPro;
using UnityEngine;

public class TipsManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI tips;
    [SerializeField] Tips text;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tips = GetComponent<TextMeshProUGUI>();
    }

    void ChangeNextTips(int n)
    {
        tips.text = text.text[n];
    }
}
