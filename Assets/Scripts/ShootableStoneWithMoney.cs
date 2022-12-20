using UnityEngine;

public class ShootableStoneWithMoney : Shootable
{
    public override void GetShot()
    {
        base.GetShot();
        GameObject objectParent = GameObject.Find("ObjectParent");

        if (shootableHealth <= 0)
        {
            Transform moneyChild = transform.GetChild(0);
            moneyChild.SetParent(objectParent.transform);

            Animator moneyAnimator = moneyChild.GetComponent<Animator>();
            if (moneyAnimator != null)
                moneyAnimator.enabled = true;

            moneyAnimator.SetTrigger("isJump");
        }
    }
}