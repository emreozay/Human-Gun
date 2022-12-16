using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shootable : MonoBehaviour
{
    protected int num;

    public virtual void GetShot()
    {
        var textChild = GetComponentInChildren<TextMeshPro>();

        if (textChild != null)
        {
            num = Mathf.Abs(int.Parse(textChild.text));
            num--;

            textChild.text = num.ToString();

            if (num <= 0)
                Destroy(gameObject);
        }
    }
}