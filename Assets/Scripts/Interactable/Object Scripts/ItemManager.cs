using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public PickUp[] allItems;
    public PlayerInteractables PI;


    // Start is called before the first frame update
    void Start()
    {
        allItems = FindObjectsOfType<PickUp>();
        PI = FindObjectOfType<PlayerInteractables>();
        SetRealmToHuman();
    }

    public void SetRealmToHuman()
    {
        foreach(PickUp p in allItems)
        {
            if(p.yokaiRealmOnly && p != null)
            {
                PI.interactables.Remove(p);
                p.SetVisible(false);
            }
        }
    }

    public void SetRealmToYokai()
    {
        foreach (PickUp p in allItems)
        {
            if (p.yokaiRealmOnly && p != null)
            {
                p.SetVisible(true);
            }
        }
    }
}
