using UnityEngine;

public class ObstacleSpin : MonoBehaviour
{
    [SerializeField]
    private float spinSpeed = 20f;

    void Update()
    {
        transform.Rotate(-Vector3.up * spinSpeed * Time.deltaTime);
    }
}
