using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Interactable
{
    public Dialogue dialogue;

    public ParticleSystem npcColor;

    public bool dialogueStarted;

    //public string itemWanted;

    public override void Interact()
    {
        /*
        if(itemWanted != "")
        {
            //Search player for item
            Debug.Log(itemWanted);
        }*/


        DialogueManager dm = FindObjectOfType<DialogueManager>();
        //Talk
        if (!dialogueStarted)
        {
            dm.StartDialogue(dialogue);
            dm.SetCurrentNPC(this);
            p.cantMove = true;
            dialogueStarted = true;
        }
        else
        {
            dm.DisplayNextSentence();
        }
    }

    public void StopDialogue()
    {
        p.cantMove = false;
        dialogueStarted = false;
    }

    public override void Highlight()
    {
        //Maybe
    }

    public override void RemoveHighlight()
    {
        //Maybe
    }

    public override void SetUnlockStatus(bool var)
    {
        Debug.Log("Not an unlockable interactable");
    }

    public void ShowColor()
    {
        npcColor.gameObject.SetActive(true);
    }

    public void RemoveColor()
    {
        npcColor.gameObject.SetActive(false);
    }
}
