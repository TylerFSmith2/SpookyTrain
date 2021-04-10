using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Door : Interactable
{
    public Transform placeToGo;

    public SpriteRenderer sr;

    public bool unlocked;

    public string unlockMechanism;

    public override void Interact()
    {
        //Go to place
        if(unlocked)
        {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            p.transform.position = placeToGo.position;
        }
        else
        {
            if(p.GetComponent<Inventory>().CheckForAndUseItem(unlockMechanism))
            {
                unlocked = true;
                p.transform.position = placeToGo.position;
            }
            else
            {
                Fade("Door is locked");
            }
        }
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
        unlocked = var;
    }
}
