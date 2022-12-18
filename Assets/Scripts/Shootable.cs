using TMPro;
using UnityEngine;

public class Shootable : MonoBehaviour
{
    [SerializeField]
    private GameObject stoneParticle;
    private TextMeshPro textChild;
    protected int num;

    private void Awake()
    {
        textChild = GetComponentInChildren<TextMeshPro>();
    }

    public virtual void GetShot()
    {
        Vector3 particlePosition = transform.position - (Vector3.forward / 2f);
        Instantiate(stoneParticle, particlePosition, Quaternion.identity);

        if (textChild != null)
        {
            num = Mathf.Abs(int.Parse(textChild.text));
            num--;

            textChild.text = num.ToString();

            if (num <= 0)
                Destroy(gameObject);
        }
    }
}