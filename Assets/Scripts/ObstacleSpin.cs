using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpin : MonoBehaviour
{
    [SerializeField]
    private float spinSpeed = 15f;

    void Update()
    {
        transform.Rotate(-Vector3.up * spinSpeed * Time.deltaTime);
    }
}
