using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HidingItem : Interactable
{
    public Material col;

    public override void Interact()
    {
        //Remove item
        p.hidingItems++;
        p.GetComponentInChildren<PlayerInteractables>().interactables.Remove(this);
        p.SetHidden(false);
        Destroy(transform.gameObject);
    }

    public override void Highlight()
    {
        col.SetColor("_Color", Color.red);
    }

    public override void RemoveHighlight()
    {
        col.SetColor("_Color", Color.yellow);
    }

    public override void SetUnlockStatus(bool var)
    {
        Debug.Log("Not an unlockable interactable");
    }
}
