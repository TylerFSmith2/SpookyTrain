using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : Interactable
{
    public Item item;
    public bool yokaiRealmOnly;

    [SerializeField]
    private string itemName_;
    [SerializeField]
    private Sprite itemSprite_;
    [SerializeField]
    private string itemDesc_;

    public SpriteRenderer sr;

    public void Awake()
    {
        item = new Item(itemName_, itemSprite_, itemDesc_);
        
    }

    public override void Interact()
    {
        p.GetComponent<Inventory>().AddItem(item);
        Destroy(transform.gameObject);
    }

    public void SetVisible(bool vis)
    {
        transform.gameObject.SetActive(vis);
    }

    public void DestroyThis()
    {
        Destroy(transform.gameObject);
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
