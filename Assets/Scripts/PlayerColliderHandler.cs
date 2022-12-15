using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerColliderHandler : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI moneyText;
    private int money = 0;

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
            if(moneyText != null)
            {
                money++;
                moneyText.text = "$" + money;
            }

            Destroy(other.gameObject);
        }

        if (other.CompareTag("Obstacle"))
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * 2f, ForceMode.Impulse);
        }
    }
}