using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerColliderHandler : MonoBehaviour
{
    int money = 0;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Lens"))
        {
            var textChild = other.GetComponentInChildren<TextMeshPro>();
            var mesh = other.GetComponentInChildren<MeshRenderer>();
            
            if(textChild != null)
            {
                int num = int.Parse(textChild.text);
            }

            if (mesh != null)
            {
                mesh.material.color = Color.gray;
            }
        }

        if (other.CompareTag("Money"))
        {
            print("Money: $" + ++money);
            Destroy(other.gameObject);
        }
    }
}