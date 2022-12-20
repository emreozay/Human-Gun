using UnityEngine;

public class ShootableStoneWithMoney : Shootable
{
    public override void GetShot()
    {
        base.GetShot();

        if (shootableHealth <= 0)
        {
            Transform moneyChild = transform.GetChild(0);
            moneyChild.SetParent(null);

            Animator moneyAnimator = moneyChild.GetComponent<Animator>();
            if (moneyAnimator != null)
                moneyAnimator.enabled = true;

            moneyAnimator.SetTrigger("isJump");
            /*
            Vector3 newPosition = moneyChild.position;
            newPosition.y = -0.6f;
            moneyChild.position = newPosition;*/
        }
    }
}