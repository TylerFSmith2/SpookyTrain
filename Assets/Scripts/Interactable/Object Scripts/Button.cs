using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button : Interactable
{
    public Renderer r;

    public Interactable interactableToUnlock;
    public override void Interact()
    {
        //Unlock door
        interactableToUnlock.SetUnlockStatus(true);
        Fade("The door is unlocked.");
    }

    public override void Highlight()
    {
        r.material.color = Color.red;
    }

    public override void RemoveHighlight()
    {
        r.material.color = Color.white;
    }

    public override void SetUnlockStatus(bool var)
    {
        Debug.Log("Not an unlockable interactable");
    }
}
