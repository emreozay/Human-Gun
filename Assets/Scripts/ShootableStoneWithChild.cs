using UnityEngine;

public class ShootableStoneWithChild : Shootable
{
    public override void GetShot()
    {
        base.GetShot();
        GameObject objectParent = GameObject.Find("ObjectParent");

        if (shootableHealth <= 0)
        {
            Transform child = transform.GetChild(0);
            child.SetParent(objectParent.transform);

            Animator childAnimator = child.GetComponent<Animator>();
            if (childAnimator != null)
                childAnimator.enabled = true;

            childAnimator.SetTrigger("isJump");
        }
    }
}