using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeyInstructionUI : MonoBehaviour
{
	TextMeshProUGUI instruction;
	public string pretext;
	public string action;
	public string posttext;

	void Awake()
	{
		instruction = GetComponent<TextMeshProUGUI>();
	}
    
    void Start()
    {
     	Display();   
    }
    
    void Display()
    {
    	instruction.text = pretext + " '"+Keybinds.instance.keys[action]+"' " + posttext;
    }
}
