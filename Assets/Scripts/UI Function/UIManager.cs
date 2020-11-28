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

	public PauseScreenDisplay pauseScreenDisplay;
	PauseScreenDisplay myPauseScreenDisplay;

	void Start()
	{
		myEndScreenDisplay = Instantiate(endScreenDisplay);
		myEndScreenDisplay.gameObject.SetActive(false);
		myFailScreenDisplay = Instantiate(failScreenDisplay);
		myFailScreenDisplay.gameObject.SetActive(false);
		myPauseScreenDisplay = Instantiate(pauseScreenDisplay);
		myPauseScreenDisplay.gameObject.SetActive(false);
	}

	public void EnterEndscreen()
	{
		myFailScreenDisplay.gameObject.SetActive(false);
    	myEndScreenDisplay.gameObject.SetActive(true);
	}

	public void EnterFailScreen()
	{
		myEndScreenDisplay.gameObject.SetActive(false);
		myFailScreenDisplay.gameObject.SetActive(true);
	}

	public void EnterPause()
	{
		myPauseScreenDisplay.gameObject.SetActive(true);
	}

	public void ExitPause()
	{
		myPauseScreenDisplay.gameObject.SetActive(false);
	}
}
