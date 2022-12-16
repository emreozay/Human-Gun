using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootableStoneWithHuman : Shootable
{
    public override void GetShot()
    {
        base.GetShot();

        if(num == 0){
            Transform humanChild = transform.GetChild(0);
            humanChild.SetParent(null);

            Vector3 newPosition = humanChild.position;
            newPosition.y = -0.25f;
            humanChild.position = newPosition;
        }
    }
}