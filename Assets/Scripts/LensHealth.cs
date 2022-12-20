using TMPro;
using UnityEngine;

public class LensHealth : MonoBehaviour
{
    [SerializeField]
    private Material redLensMaterial;
    [SerializeField]
    private Material blueLensMaterial;

    [SerializeField]
    private MeshRenderer lensMesh;

    [SerializeField]
    private TextMeshPro lensText;

    void Start()
    {
        SetLensHealth();
    }

    private void SetLensHealth()
    {
        int currentLevel = PlayerPrefs.GetInt("Level", 1);
        int randomHealth = Random.Range(-2, currentLevel * 2);

        if (randomHealth < 0)
        {
            lensMesh.material = redLensMaterial;
            lensText.text = randomHealth.ToString();
        }
        else if (randomHealth > 0)
        {
            lensMesh.material = blueLensMaterial;
            lensText.text = "+" + randomHealth;
        }
        else
        {
            lensMesh.material = blueLensMaterial;
            lensText.text = randomHealth.ToString();
        }
    }
}
