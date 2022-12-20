using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootManager : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private Transform bulletParent;

    [SerializeField]
    private Animator recoilAnimator;

    [SerializeField]
    private float bulletForce;

    private float bulletWaitTime = 0.25f;
    private float bulletRange = 5f;
    private bool canShoot = true;

    private PlayerColliderHandler playerColliderHandler;
    private Queue<GameObject> bulletList = new Queue<GameObject>();

    private void Awake()
    {
        playerColliderHandler = GetComponent<PlayerColliderHandler>();
    }

    void Start()
    {
        CreateBulletList();
    }

    private void Update()
    {
        Debug.DrawRay(transform.position, Vector3.forward * bulletRange, Color.cyan);

        if (Physics.Raycast(transform.position, Vector3.forward, out RaycastHit raycast, bulletRange))
        {
            int health = 0;

            if (playerColliderHandler != null)
                health = playerColliderHandler.GetHealth();

            if (canShoot && health > 1)
            {
                Shootable shootable = raycast.transform.GetComponent<Shootable>();

                if (shootable != null)
                    StartCoroutine(WaitForShoot(bulletWaitTime));
            }
        }
    }

    private void CreateBulletList()
    {
        for (int i = 0; i < 10; i++)
        {
            var bullet = Instantiate(bulletPrefab, bulletParent);
            bullet.SetActive(false);

            bulletList.Enqueue(bullet);
        }
    }

    private void Shoot()
    {
        var nextBullet = bulletList.Dequeue();
        Rigidbody rb = nextBullet.GetComponent<Rigidbody>();
        nextBullet.SetActive(true);

        if (rb == null)
            return;

        Vector3 bulletPosition = new Vector3(transform.position.x, 0.15f, transform.position.z + 0.15f);
        nextBullet.transform.position = bulletPosition;

        rb.velocity = Vector3.zero;
        rb.AddForce(Vector3.forward * bulletForce, ForceMode.Impulse);

        Recoil();

        bulletList.Enqueue(nextBullet);
    }

    private IEnumerator WaitForShoot(float waitTime)
    {
        canShoot = false;
        Shoot();

        yield return new WaitForSeconds(waitTime);
        canShoot = true;
    }

    private void Recoil()
    {
        if (recoilAnimator != null)
            recoilAnimator.SetTrigger("isRecoil");
    }
}