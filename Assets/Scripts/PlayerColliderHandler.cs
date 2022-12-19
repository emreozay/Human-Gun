using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerColliderHandler : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI moneyText;

    [SerializeField]
    private Animator playerAnimator;

    [SerializeField]
    private Material[] stickmanMaterials;

    [SerializeField]
    private GameObject stickman;

    private Stack<GameObject> stickmanStack = new Stack<GameObject>();

    private int money = 0;
    private int health = 2;
    private bool isFinished;

    private int animIndex = 1;

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
                float sign = Mathf.Sign(int.Parse(textChild.text));
                int stickmanNumber = Mathf.Abs(int.Parse(textChild.text));

                if(sign > 0f)
                {
                    StartCoroutine(WaitForNewStickman(stickmanNumber));
                }
                else if(sign < 0f)
                {
                    for (int i = 0; i < stickmanNumber; i++)
                    {
                        DestroyStickman();
                    }
                }
            }

            if (mesh != null)
                mesh.material.color = Color.gray;
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
            AddNewStickman(other);
        }
    }

    private void AddNewStickman(Collider humanCollider)
    {
        Animator humanAnimator = humanCollider.GetComponent<Animator>();

        if (playerAnimator != null)
            playerAnimator.SetTrigger("Pose_01");

        humanCollider.enabled = false;

        Material humanMaterial = stickmanMaterials[Random.Range(0, stickmanMaterials.Length)];
        humanCollider.GetComponentInChildren<SkinnedMeshRenderer>().material = humanMaterial;

        if (humanAnimator != null)
        {
            animIndex++;

            if (animIndex > 6)
                return;

            humanCollider.transform.SetParent(transform);
            humanCollider.transform.localPosition = new Vector3(0f, humanCollider.transform.localPosition.y, 0f);
            humanAnimator.SetTrigger("Pose_0" + animIndex.ToString());

            stickmanStack.Push(humanCollider.gameObject);
        }
    }

    private void DestroyStickman()
    {
        animIndex--;
        GameObject deletedStickman = stickmanStack.Pop();

        Destroy(deletedStickman);
    }

    private IEnumerator WaitForNewStickman(int stickmanNumber)
    {
        for (int i = 0; i < stickmanNumber; i++)
        {
            yield return new WaitForSeconds(0.25f);

            GameObject newStickman = Instantiate(stickman);
            Collider stickmanCollider = newStickman.GetComponent<Collider>();

            if (stickmanCollider != null)
                AddNewStickman(stickmanCollider);
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