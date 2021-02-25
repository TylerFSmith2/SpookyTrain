using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerInteractables : MonoBehaviour
{
    public Character p;
    public List<Interactable> interactables = new List<Interactable>();
    

    public Interactable toInteract;

    public GameObject ShowInteractableName;

    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Interactable>() != null)
        {
            interactables.Add(other.gameObject.GetComponent<Interactable>());
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Interactable>() != null)
        {
            interactables.Remove(other.gameObject.GetComponent<Interactable>());
            p.SetCurrentInteractable(null);
        }
    }

    public void Update()
    {
        
        //Tests if last interactable was removed
        if (interactables.Count <= 0 && toInteract != null)
        {
            toInteract.RemoveHighlight();
            toInteract = null;
        }


        float dist = 99;
        Interactable prev = toInteract;
        foreach (Interactable i in interactables)
        {
            if (dist > Vector3.Distance(i.gameObject.transform.position, p.transform.position))
            {
                dist = Vector3.Distance(i.gameObject.transform.position, p.transform.position);
                toInteract = i;
            }
        }

        if (toInteract != null)
        {
            if (prev != null && prev != toInteract)
            {
                prev.RemoveHighlight();
            }
            p.SetCurrentInteractable(toInteract);
            toInteract.Highlight();
        }

        if(toInteract == null)
        {
            ShowInteractableName.gameObject.SetActive(false);
        }
        else
        {
            ShowInteractableName.gameObject.SetActive(true);
            ShowInteractableName.GetComponentInChildren<Text>().text = toInteract.name;
        }
    }
}
