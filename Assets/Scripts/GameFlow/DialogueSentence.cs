using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Dialogue Box", menuName = "Dialogue Box")]
public class DialogueSentence : ScriptableObject
{
	public string speaker;		
	public Sprite portrait;
	[TextArea(3,10)]
	public string sentence;

	public void Print()
	{
		Debug.Log(speaker + ": " + sentence);
	}
}
