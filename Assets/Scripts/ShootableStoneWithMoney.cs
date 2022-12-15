using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootableStoneWithMoney : Shootable
{
    public override void GetShot()
    {
        base.GetShot();

        if(num == 0){
            Transform moneyChild = transform.GetChild(0);
            moneyChild.SetParent(null);

            Vector3 newPosition = moneyChild.position;
            newPosition.y = -0.6f;
            moneyChild.position = newPosition;
        }
    }
}