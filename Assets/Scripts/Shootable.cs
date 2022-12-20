using TMPro;
using UnityEngine;

public class Shootable : MonoBehaviour
{
    [SerializeField]
    private GameObject stoneParticle;
    private TextMeshPro textChild;
    protected int shootableHealth;

    private Animator stoneAnimator;

    private void Awake()
    {
        textChild = GetComponentInChildren<TextMeshPro>();
        stoneAnimator = GetComponent<Animator>();
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

    private void ScaleObject()
    {
        if (stoneAnimator != null)
            stoneAnimator.SetTrigger("isScale");
    }

    public virtual void GetShot()
    {
        ScaleObject();

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