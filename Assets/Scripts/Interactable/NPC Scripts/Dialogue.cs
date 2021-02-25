using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class Dialogue
{
    public string name;

    [TextArea(3, 10)]
    public string[] currentSentences;

    public string[] answers = new string[3];

    public bool hasOptions;
    public int numOptions;

    public TextAsset currentDialogueText;

    private TextAsset dialogueTree1;
    private TextAsset dialogueTree2;
    private TextAsset dialogueTree3;

    //FIXME: Choose tree should do more than this

    public void ChooseButton(int buttonNum)
    {
        TextAsset temp = currentDialogueText;
        switch(buttonNum)
        {
            //FIXME: Event occurs, Thing spawns, tree dialogue
            case 1:
                currentDialogueText = dialogueTree1;
                dialogueTree1 = temp;
                return;
            case 2:
                currentDialogueText = dialogueTree2;
                dialogueTree2 = temp;
                return;
            case 3:
                currentDialogueText = dialogueTree3;
                dialogueTree3 = temp;
                return;
            default:
                return;
        }
    }

    public void ReadDataToSentences()
    {
        

        string s = currentDialogueText.text;
        currentSentences = s.Split('\n');

        if(int.Parse(currentSentences[0]) == 1)
        {
            hasOptions = true;
        }
        else
        {
            hasOptions = false;
        }
        answers[0] = currentSentences[2];
        answers[1] = currentSentences[3];
        answers[2] = currentSentences[4];

        dialogueTree1 = Resources.Load<TextAsset>("Dialogue/" + currentSentences[5].Trim());
        dialogueTree2 = Resources.Load<TextAsset>("Dialogue/" + currentSentences[6].Trim());
        dialogueTree3 = Resources.Load<TextAsset>("Dialogue/" + currentSentences[7].Trim());

        numOptions = int.Parse(currentSentences[1]);        
    }
}
