using UnityEngine;

public class ShootableStoneWithHuman : Shootable
{
    public override void GetShot()
    {
        base.GetShot();
        GameObject objectParent = GameObject.Find("ObjectParent");

        if (shootableHealth <= 0)
        {
            Transform humanChild = transform.GetChild(0);
            humanChild.SetParent(objectParent.transform);

            Animator humanAnimator = humanChild.GetComponent<Animator>();
            if (humanAnimator != null)
                humanAnimator.enabled = true;

            humanAnimator.SetTrigger("isJump");
        }
    }
}