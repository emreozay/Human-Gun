using TMPro;
using UnityEngine;

public class PlayerColliderHandler : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI moneyText;

    [SerializeField]
    private Animator playerAnimator;

    private int money = 0;
    private int health = 2;
    private bool isFinished;

    private void Awake()
    {
        LevelGenerator.NewLevel += ResetVariables;
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

            if (isFinished)
            {
                PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level", 1) + 1);

                Time.timeScale = 0;
                LevelGenerator.LevelCompleted();

                return;
            }

            if (health > 0)
            {
                if (other.GetComponent<Shootable>() != null)
                    Destroy(other.gameObject);
                else
                    GetComponent<Rigidbody>().AddForce(Vector3.up * 2f, ForceMode.Impulse);

            }
            else
            {
                Time.timeScale = 0;

                LevelGenerator.LevelFailed();
            }
        }

        if (other.CompareTag("Human"))
        {
            Animator humanAnimator = other.GetComponent<Animator>();
            playerAnimator.SetTrigger("Pose_01");

            if(humanAnimator != null)
            {
                other.transform.SetParent(transform);
                other.transform.localPosition = new Vector3(0f, other.transform.localPosition.y, 0f);

                humanAnimator.SetTrigger("Pose_02");
            }
        }
    }

    private void ResetVariables()
    {
        isFinished = false;
        health = 2;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Finish"))
        {
            isFinished = true;
            print("FINISHED!!!");
        }
    }

    private void OnDestroy()
    {
        LevelGenerator.NewLevel -= ResetVariables;
    }
}