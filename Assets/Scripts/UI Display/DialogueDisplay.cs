using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueDisplay : MonoBehaviour
{

	public DialogueSentence dialogue;

	public TextMeshProUGUI characterName;
	public TextMeshProUGUI speechText;
	public Image characterPortrait;

    void Start()
    {
        UpdateInfo();
    }

    public void SetDialogue(DialogueSentence d)
    {
    	dialogue = d;
		UpdateInfo(); 	
    }

    void UpdateInfo()
    {
		characterName.text = dialogue.speaker;
        speechText.text = dialogue.sentence;
        characterPortrait.sprite = dialogue.portrait;
    }

}
