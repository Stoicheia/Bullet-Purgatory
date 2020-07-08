using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LevelState{PRE, DIALOGUE, PLAYING, PAUSED, ENDSCREEN, FAILSCREEN}

public class LevelStateManager : MonoBehaviour
{
	public LevelState levelState {get; private set;}

	WaveSpawner waveSpawner;
	RhythmMapsManager rhythmManager;
	DialogueManager dialogueManager;
	UIManager uiManager;

	void Awake()
	{
		SetLevelState(LevelState.PRE);
		waveSpawner = FindObjectOfType<WaveSpawner>();
		rhythmManager = FindObjectOfType<RhythmMapsManager>();
		dialogueManager = FindObjectOfType<DialogueManager>();
		uiManager = FindObjectOfType<UIManager>();
	}

	void Start()
	{
		SetLevelState(LevelState.DIALOGUE);
	}

	void Update(){

	}

	void OnEnable()
	{
		DialogueManager.GoToState += SetLevelState;
		RhythmMapsManager.GoToState += SetLevelState;
		WaveSpawner.GoToState += SetLevelState;
	}

	void OnDisable()
	{
		DialogueManager.GoToState -= SetLevelState;
		RhythmMapsManager.GoToState -= SetLevelState;
		WaveSpawner.GoToState -= SetLevelState;
	}

    public void SetLevelState(LevelState newState){
    	LevelState prevState = levelState;

    	switch(prevState){
    		case LevelState.ENDSCREEN:
    		case LevelState.FAILSCREEN:
    			return;
    	}


    	switch(prevState){
    		case LevelState.DIALOGUE:
    			dialogueManager.ExitDialogue();
    			break;
    		case LevelState.PLAYING:
    			waveSpawner.StopSpawning();
    			break;
    	}
    	levelState = newState;
    	switch(newState){
    		case LevelState.DIALOGUE:
    			dialogueManager.EnterDialogue();
    			break;
    		case LevelState.PLAYING:
    			waveSpawner.StartSpawning();    		
    			rhythmManager.EnterRhythm();
    			break;
    		case LevelState.ENDSCREEN:
    			uiManager.EnterEndscreen();
    			break;
    		case LevelState.FAILSCREEN:
    			uiManager.EnterFailScreen();
    			break;
    	}

    }
}
