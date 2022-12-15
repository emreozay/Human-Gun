using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Shootable shootable = other.GetComponent<Shootable>();
        if (shootable != null)
        {
            shootable.GetShot();
            gameObject.SetActive(false);
        }
    }
}