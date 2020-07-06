using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
	//public delegate void StateChangeAction(LevelState state);
	//public static event StateChangeAction GoToState;

	public EndScreenDisplay endScreenDisplay;
	EndScreenDisplay myEndScreenDisplay;

	public void EnterEndscreen()
	{
    	myEndScreenDisplay = Instantiate(endScreenDisplay, transform.position, transform.rotation);
	}
}
