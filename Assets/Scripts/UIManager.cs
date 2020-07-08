using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
	//public delegate void StateChangeAction(LevelState state);
	//public static event StateChangeAction GoToState;

	public EndScreenDisplay endScreenDisplay;
	EndScreenDisplay myEndScreenDisplay;

	public FailScreenDisplay failScreenDisplay;
	FailScreenDisplay myFailScreenDisplay;

	public void EnterEndscreen()
	{
    	myEndScreenDisplay = Instantiate(endScreenDisplay, transform.position, transform.rotation);
	}

	public void EnterFailScreen()
	{
		myFailScreenDisplay = Instantiate(failScreenDisplay, transform.position, transform.rotation);
	}
}
