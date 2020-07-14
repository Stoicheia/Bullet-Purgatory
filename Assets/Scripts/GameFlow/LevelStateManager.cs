using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LevelState{PRE, DIALOGUE, PLAYING, PAUSED, ENDSCREEN, FAILSCREEN}

public class LevelStateManager : MonoBehaviour
{
    public delegate void StateChangeAction(LevelState state);
    public static event StateChangeAction OnStateChange;

	public LevelState levelState {get; private set;}
    public LevelState lastState {get; private set;}

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

        Bullet.SetStage(GameObject.FindGameObjectWithTag("Stage").GetComponent<MeshCollider>());
        Bullet.SetPooler(FindObjectOfType(typeof(ObjectPooler)) as ObjectPooler);
	}

	void Update(){
        if(Keybinds.instance.GetInputDown("Pause")){
            Debug.Log(levelState + " " + lastState);
            if(levelState!=LevelState.PAUSED)
                SetLevelState(LevelState.PAUSED);
            else
                SetLevelState(lastState);
        }
	}

	void OnEnable()
	{
		DialogueManager.GoToState += SetLevelState;
		RhythmMapsManager.GoToState += SetLevelState;
		WaveSpawner.GoToState += SetLevelState;
        Player.OnPlayerDeath += Fail;
	}

	void OnDisable()
	{
		DialogueManager.GoToState -= SetLevelState;
		RhythmMapsManager.GoToState -= SetLevelState;
		WaveSpawner.GoToState -= SetLevelState;
        Player.OnPlayerDeath -= Fail;
	}

    void Fail(){
        SetLevelState(LevelState.FAILSCREEN);
    }

    public void SetLevelState(LevelState newState){
        if(newState==levelState) return;

    	LevelState prevState = levelState;
        lastState = prevState;

        //Exit
    	switch(prevState){
            case LevelState.ENDSCREEN:
            case LevelState.FAILSCREEN:
                return;
            case LevelState.PRE:
                switch(newState){
                    case LevelState.DIALOGUE:
                        dialogueManager.EnterDialogue();
                        break;
                }
                break;
    		case LevelState.DIALOGUE:
                switch(newState){
        			case LevelState.PLAYING:
                        dialogueManager.ExitDialogue();          
                        rhythmManager.EnterRhythm();
                        break;
                }
                break;
    		case LevelState.PLAYING:
                switch(newState){
                    case LevelState.ENDSCREEN:
                    case LevelState.FAILSCREEN:
            			waveSpawner.StopSpawning();
            			break;
                }
                break;
            case LevelState.PAUSED:
                uiManager.ExitPause();
                break;
    	}
    	levelState = newState;

        //Enter
    	switch(newState){
    		case LevelState.DIALOGUE:
                Keybinds.instance.SetDialogueBinds();
    			break;
    		case LevelState.PLAYING:
                Keybinds.instance.SetPlayBinds();
    			waveSpawner.StartSpawning();    
                rhythmManager.UnpauseActive();		
    			break;
            case LevelState.PAUSED:
                Keybinds.instance.DisableAll();
                waveSpawner.StopSpawning();
                rhythmManager.PauseActive();
                uiManager.EnterPause();
                break;
    		case LevelState.ENDSCREEN:
    			uiManager.EnterEndscreen();
    			break;
    		case LevelState.FAILSCREEN:
    			uiManager.EnterFailScreen();
    			break;
    	}
        OnStateChange(levelState);
    }
}
