using UnityEngine;

public class ShootableBarrel : Shootable
{
    [SerializeField]
    private GameObject explosionParticle;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 2f);
    }

    public override void GetShot()
    {
        base.GetShot();

        if (num > 0)
            return;

        GameObject particle = Instantiate(explosionParticle, transform.position, Quaternion.identity);
        Destroy(particle, 1);

        var colliders = Physics.OverlapSphere(transform.position, 2f);

        foreach (var item in colliders)
        {
            if (item.GetComponent<Shootable>() != null)
                Destroy(item.gameObject);
        }
    }
}
