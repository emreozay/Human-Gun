using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerColliderHandler : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI moneyText;

    [SerializeField]
    private GameObject stickman;
    [SerializeField]
    private Transform recoilParent;

    [SerializeField]
    private Animator playerAnimator;

    [SerializeField]
    private GameObject moneyParticle;

    [SerializeField]
    private Material[] stickmanMaterials;

    private Stack<GameObject> stickmanStack = new Stack<GameObject>();

    private int money = 0;
    private int health = 1;
    private int animIndex = 1;
    private static int bulletDamage = 1;

    private bool isFinished;

    private void Awake()
    {
        LevelGenerator.NewLevel += ResetVariables;
    }

    private void Start()
    {
        money = PlayerPrefs.GetInt("Money", 0);
        moneyText.text = "$" + money;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Lens"))
            TriggerWithLens(other);

        if (other.CompareTag("Money"))
            TriggerWithMoney(other);

        if (other.CompareTag("Obstacle"))
            TriggerWithObstacle(other);

        if (other.CompareTag("Human"))
            AddNewStickman(other);
    }

    private void TriggerWithLens(Collider other)
    {
        var textChild = other.GetComponentInChildren<TextMeshPro>();
        var mesh = other.GetComponentInChildren<MeshRenderer>();

        if (textChild != null)
        {
            float sign = Mathf.Sign(int.Parse(textChild.text));
            int stickmanNumber = Mathf.Abs(int.Parse(textChild.text));

            if (sign > 0f)
            {
                StartCoroutine(WaitForNewStickman(stickmanNumber));
            }
            else if (sign < 0f)
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

    private void TriggerWithMoney(Collider other)
    {
        if (moneyText != null)
        {
            UpdateMoney();
        }

        Destroy(other.gameObject);

        Vector3 moneyParticlePosition = transform.position + Vector3.forward / 3f;
        Instantiate(moneyParticle, moneyParticlePosition, Quaternion.identity);
    }

    private void TriggerWithObstacle(Collider other)
    {
        DestroyStickman();

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

    private void AddNewStickman(Collider humanCollider)
    {
        Animator humanAnimator = humanCollider.GetComponent<Animator>();

        if (playerAnimator != null && animIndex == 1)
            playerAnimator.SetTrigger("Pose_01");

        humanCollider.enabled = false;

        Material humanMaterial = stickmanMaterials[Random.Range(0, stickmanMaterials.Length)];
        humanCollider.GetComponentInChildren<SkinnedMeshRenderer>().material = humanMaterial;

        if (humanAnimator != null)
        {
            animIndex++;

            if (animIndex > 6)
            {
                animIndex = 6;
                Destroy(humanCollider.gameObject);
                UpdateMoney();

                return;
            }

            health++;
            bulletDamage++;

            if (animIndex > 2)
                humanCollider.transform.SetParent(recoilParent);
            else
                humanCollider.transform.SetParent(transform);

            humanCollider.transform.localPosition = new Vector3(0f, humanCollider.transform.localPosition.y, 0f);
            humanAnimator.SetTrigger("Pose_0" + animIndex.ToString());

            stickmanStack.Push(humanCollider.gameObject);

            StartCoroutine(DisableAnimator(humanAnimator));
        }
    }

    private void DestroyStickman()
    {
        health--;
        animIndex--;
        bulletDamage--;

        if (health < 2)
        {
            Time.timeScale = 0;
            LevelGenerator.LevelFailed();
        }

        if (stickmanStack.Count > 0)
        {
            GameObject deletedStickman = stickmanStack.Pop();
            Destroy(deletedStickman);
        }
    }

    private IEnumerator WaitForNewStickman(int stickmanNumber)
    {
        for (int i = 0; i < stickmanNumber; i++)
        {
            yield return new WaitForSeconds(0.2f);

            GameObject newStickman = Instantiate(stickman);
            Collider stickmanCollider = newStickman.GetComponent<Collider>();

            if (stickmanCollider != null)
                AddNewStickman(stickmanCollider);
        }
    }

    private IEnumerator DisableAnimator(Animator anim)
    {
        yield return new WaitForSeconds(0.2f);

        if (anim != null)
            anim.enabled = false;
    }

    private void ResetVariables()
    {
        isFinished = false;
        health = 1;
        animIndex = 1;
        bulletDamage = 1;

        playerAnimator.SetTrigger("isRunning");
    }

    public static int GetBulletDamage()
    {
        return bulletDamage;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Finish"))
        {
            isFinished = true;
        }
    }

    private void UpdateMoney()
    {
        money++;
        moneyText.text = "$" + money;
    }

    public int GetHealth()
    {
        return health;
    }

    private void OnDestroy()
    {
        PlayerPrefs.SetInt("Money", money);
        LevelGenerator.NewLevel -= ResetVariables;
    }
}