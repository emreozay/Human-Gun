using TMPro;
using UnityEngine;

public class FinishText : MonoBehaviour
{
    [SerializeField]
    private int multiplier;

    private TextMeshPro[] stoneTexts;
    private int stoneHealth;

    private void Awake()
    {
        stoneTexts = GetComponentsInChildren<TextMeshPro>();
    }

    private void Start()
    {
        WriteTexts();
    }

    private void WriteTexts()
    {
        stoneHealth = 10 * multiplier * PlayerPrefs.GetInt("Level", 1);

        if (stoneTexts == null)
            return;

        for (int i = 0; i < stoneTexts.Length; i++)
        {
            stoneTexts[i].text = stoneHealth.ToString();
        }
    }
}
