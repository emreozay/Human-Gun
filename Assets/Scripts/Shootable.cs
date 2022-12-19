using TMPro;
using UnityEngine;

public class Shootable : MonoBehaviour
{
    [SerializeField]
    private GameObject stoneParticle;
    private TextMeshPro textChild;
    protected int shootableHealth;

    private void Awake()
    {
        textChild = GetComponentInChildren<TextMeshPro>();
    }

    private void Start()
    {
        SetRandomHealth();
    }

    private void SetRandomHealth()
    {
        int currentLevel = PlayerPrefs.GetInt("Level", 1);
        int randomHealth = Random.Range(2, currentLevel * 4);

        SetHealth(randomHealth);
    }

    public virtual void GetShot()
    {
        Vector3 particlePosition = transform.position - (Vector3.forward / 2f);
        Instantiate(stoneParticle, particlePosition, Quaternion.identity);

        SetHealth(shootableHealth - 1);

        if (shootableHealth <= 0)
            Destroy(gameObject);
    }

    public void SetHealth(int health)
    {
        shootableHealth = health;

        if (textChild != null)
            textChild.text = shootableHealth.ToString();
    }
}