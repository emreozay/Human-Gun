using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FinishText : MonoBehaviour
{
    [SerializeField]
    private int multiplier;

    private TextMeshPro[] stoneTexts;
    private int stoneHealth;

    private void Start()
    {
        WriteTexts();
    }

    private void WriteTexts()
    {
        stoneHealth = 10 * multiplier;
        stoneTexts = GetComponentsInChildren<TextMeshPro>();

        for (int i = 0; i < stoneTexts.Length; i++)
        {
            stoneTexts[i].text = stoneHealth.ToString();
        }
    }
}
