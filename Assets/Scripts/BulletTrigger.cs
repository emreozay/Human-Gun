using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Stone"))
        {
            DestroyParent(other);
        }

        if (other.CompareTag("StoneWithMoney"))
        {
            Transform moneyChild = other.transform.GetChild(0);

            HandleMoney(moneyChild);
            DestroyParent(other);
        }
    }

    private void HandleMoney(Transform moneyChild)
    {
        moneyChild.SetParent(null);

        Vector3 newPosition = moneyChild.position;
        newPosition.y = -0.6f;
        moneyChild.position = newPosition;
    }

    private void DestroyParent(Collider other)
    {
        gameObject.SetActive(false);
        Destroy(other.gameObject);
    }
}
