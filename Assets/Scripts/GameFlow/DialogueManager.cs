using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
	public delegate void StateChangeAction(LevelState state);
	public static event StateChangeAction GoToState; 

	public DialogueDisplay dialogueDisplay;
	DialogueDisplay myDialogueDisplay;
	public Dialogue dialogue;

	int currentSentence;
	DialogueSentence currentDialogue;

    void Start()
    {

    }

    void Update()
    {
        if(Keybinds.instance.GetInputDown("SkipDialogue"))
            NextSentence();
    }

    public void NextSentence()
    {
    	currentSentence++;
    	if(currentSentence>=dialogue.GetSentenceCount()){
    		GoToState(LevelState.PLAYING);
    		return;
    	}
    	currentDialogue = dialogue.sentences[currentSentence];
    	myDialogueDisplay.SetDialogue(currentDialogue);
    }

    public void EnterDialogue()
    {
    	myDialogueDisplay = Instantiate(dialogueDisplay, transform.position, transform.rotation);
    	currentSentence = 0;
    	currentDialogue = dialogue.sentences[currentSentence];
    	myDialogueDisplay.SetDialogue(currentDialogue);
    }

    public void ExitDialogue()
    {
    	myDialogueDisplay.gameObject.SetActive(false);
    }

}
