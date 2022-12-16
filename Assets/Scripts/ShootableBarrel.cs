using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootableBarrel : Shootable
{
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

        var colliders = Physics.OverlapSphere(transform.position, 2f);
        foreach (var item in colliders)
        {
            if(item.GetComponent<Shootable>() != null)
                Destroy(item.gameObject);
        }
    }
}
