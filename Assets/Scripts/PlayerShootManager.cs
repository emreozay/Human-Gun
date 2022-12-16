using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootManager : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private float bulletForce;

    private bool canShoot = true;

    private Queue<GameObject> bulletList = new Queue<GameObject>();

    void Start()
    {
        CreateBulletList();
    }

    private void Update()
    {
        Debug.DrawRay(transform.position, Vector3.forward * 3f, Color.cyan);
        if (Physics.Raycast(transform.position, Vector3.forward, out RaycastHit raycast, 3f))
        {
            if (canShoot)
            {
                Shootable shootable = raycast.transform.GetComponent<Shootable>();
                if (shootable != null)
                    StartCoroutine(WaitForShoot(0.15f));
            }
        }
    }

    private void CreateBulletList()
    {
        for (int i = 0; i < 10; i++)
        {
            var bullet = Instantiate(bulletPrefab);
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

        bulletList.Enqueue(nextBullet);
    }

    private IEnumerator WaitForShoot(float waitTime)
    {
        canShoot = false;
        Shoot();

        yield return new WaitForSeconds(waitTime);
        canShoot = true;
    }
}