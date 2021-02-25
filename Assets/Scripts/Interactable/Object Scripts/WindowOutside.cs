using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowOutside : Interactable
{
    public SpriteRenderer sr;

    public override void Interact()
    {
        //Look outside
        if (p.GetHidden())
        {
            p.cantMove = false;
            Color a = p.GetComponent<SpriteRenderer>().color;
            a.a = 1f;
            p.GetComponent<SpriteRenderer>().color = a;
        }
        else
        {
            p.cantMove = true;
            Color a = p.GetComponent<SpriteRenderer>().color;
            a.a = 0.5f;
            p.GetComponent<SpriteRenderer>().color = a;
        }
        p.SetHidden(!p.GetHidden());
    }

    public override void Highlight()
    {
        sr.color = Color.red;
    }

    public override void RemoveHighlight()
    {
        sr.color = Color.white;
    }

    public override void SetUnlockStatus(bool var)
    {
        Debug.Log("Not an unlockable interactable");
    }
}
