using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    private Dialogue currentDialogue;

    private Queue<string> sentences;

    private NPC currentNPC;

    public Text nameText;
    public Text dialogueText;

    public Animator anim;

    public bool optionsAvailable = false;
    public GameObject[] optionsButtons;
    // Start is called before the first frame update
    private void Start()
    {
        sentences = new Queue<string>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && currentDialogue != null && currentNPC.dialogueStarted)
        {
            //E to skip dialogue
            
        }
    }


    public void StartDialogue(Dialogue d)
    {
        //FIXME: Add NPC?
        currentDialogue = d;

        anim.SetBool("isOpen", true);
        //Reset sentences
        sentences.Clear();

        currentDialogue.ReadDataToSentences();
        optionsAvailable = currentDialogue.hasOptions;
        //Queue up sentences. Skip first two options deciders
        for(int i = 8; i < d.currentSentences.Length; i++)
        {
            sentences.Enqueue(d.currentSentences[i]);
        }
        nameText.text = d.name;

        DisplayNextSentence();
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach(char l in sentence.ToCharArray())
        {
            dialogueText.text += l;
            yield return new WaitForSeconds(0.05f);
        }
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            if (optionsAvailable)
            {
                //Show buttons
                int i = 0;
                foreach (GameObject but in optionsButtons)
                {
                    if(i < currentDialogue.numOptions)
                    {
                        but.GetComponentInChildren<Text>().text = currentDialogue.answers[i];
                        but.SetActive(true);
                        i++;
                    }
                }
                return;
            }
            else
            {
                EndDialogue();
                return;
            }
        }


        string nextSentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(nextSentence));
    }

    public void EndDialogue()
    {
        anim.SetBool("isOpen", false);
        currentDialogue = null;
        currentNPC.StopDialogue();
    }

    public void ChooseDialogueOption(int option)
    {
        //Choose options
        currentDialogue.ChooseButton(option);

        optionsAvailable = false;

        foreach (GameObject but in optionsButtons)
        {
            but.SetActive(false);
        }

        StartDialogue(currentDialogue);
    }

    public void SetCurrentNPC(NPC newNPC)
    {
        currentNPC = newNPC;
    }
}
