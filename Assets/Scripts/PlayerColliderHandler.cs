using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerColliderHandler : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI moneyText;

    private int money = 0;
    private int health = 2;
    private bool isFinished;

    private void Start()
    {
        isFinished = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Lens"))
        {
            var textChild = other.GetComponentInChildren<TextMeshPro>();
            var mesh = other.GetComponentInChildren<MeshRenderer>();

            if (textChild != null)
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
            if (moneyText != null)
            {
                money++;
                moneyText.text = "$" + money;
            }

            Destroy(other.gameObject);
        }

        if (other.CompareTag("Obstacle"))
        {
            health--;
            print("HEALTH: " + health);
            if (health > 0)
            {
                if (isFinished)
                    print("YOU WIN!");
                else
                {
                    if (other.GetComponent<Shootable>() != null)
                        Destroy(other.gameObject);
                    else
                        GetComponent<Rigidbody>().AddForce(Vector3.up * 2f, ForceMode.Impulse);
                }
            }
            else
            {
                if (isFinished)
                    print("YOU WIN!");
                else
                    print("YOU LOSE!");

                GetComponent<Rigidbody>().velocity = Vector3.zero;

            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Finish"))
        {
            isFinished = true;
        }
    }
}