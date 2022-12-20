using System.Collections;
using UnityEngine;

public class FinishText : MonoBehaviour
{
    [SerializeField]
    private int multiplier;

    private Shootable[] shootables;
    private int stoneHealth;

    private void Awake()
    {
        shootables = GetComponentsInChildren<Shootable>();
    }

    private void Start()
    {
        StartCoroutine(WaitForSetHealths());
    }

    private void SetStoneHealths()
    {
        stoneHealth = 5 * multiplier * PlayerPrefs.GetInt("Level", 1);

        if (shootables == null)
            return;

        for (int i = 0; i < shootables.Length; i++)
        {
            shootables[i].SetHealth(stoneHealth);
        }
    }

    private IEnumerator WaitForSetHealths()
    {
        yield return new WaitForSeconds(1);
        SetStoneHealths();
    }
}
