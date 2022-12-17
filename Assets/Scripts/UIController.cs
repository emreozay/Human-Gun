using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private GameObject winPanel;
    [SerializeField]
    private GameObject losePanel;
    [SerializeField]
    private TextMeshProUGUI levelText;

    private void Awake()
    {
        LevelGenerator.LevelCompleted += OpenWinPanel;
        LevelGenerator.LevelFailed += OpenLosePanel;
        LevelGenerator.NewLevel += UpdateLevelText;
    }

    void Start()
    {
        UpdateLevelText();
    }

    private void UpdateLevelText()
    {
        levelText.text = "Level " + PlayerPrefs.GetInt("Level", 1);
    }

    private void OpenWinPanel()
    {
        winPanel.SetActive(true);
    }

    private void OpenLosePanel()
    {
        losePanel.SetActive(true);
    }

    private void ClosePanels()
    {
        winPanel.SetActive(false);
        losePanel.SetActive(false);
    }

    public void LoadLevel()
    {
        ClosePanels();
        LevelGenerator.NewLevel();
        Time.timeScale = 1;
    }

    private void OnDestroy()
    {
        LevelGenerator.LevelCompleted -= OpenWinPanel;
        LevelGenerator.LevelFailed -= OpenLosePanel;
        LevelGenerator.NewLevel -= UpdateLevelText;
    }
}
