using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingSpot : Interactable
{
    public SpriteRenderer sr;

    public override void Interact()
    {
        if (p.GetHidden())
        {
            p.cantMove = false;
        }
        else
        {
            p.cantMove = true;
        }
        p.SetHidden(!p.GetHidden());
        
        //Set unable to move if hidden, and vice versa
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
