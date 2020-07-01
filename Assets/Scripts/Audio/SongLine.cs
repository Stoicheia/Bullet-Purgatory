using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongLine : MonoBehaviour
{
	public delegate void TickAction();
	public TickAction Tick;
    public delegate void SwitchAction(int s);
    public SwitchAction Switch;

    public string part;

	AudioSource lineClip;

    float bpm;
    int notesPerBar;
    string map;
    string[] commands;

    int currentCommand;
    int currentStyle;

    float breakInterval;
    float breakIntervalSeconds;
    float nextTickTime;

    const float MAX_INTERVAL = 99999;

    void Awake(){
    	nextTickTime = 0;
    }

    public void StartRhythm()
    {
    	commands = map.Split(' ');
    	currentCommand = 0;
    	nextTickTime = 0;
        currentStyle = 0;
    	breakInterval = 100000;
    }

    void Update()
    {
    	if(lineClip.time>=nextTickTime){
    		ReadCommand();
	    	breakIntervalSeconds = (breakInterval>MAX_INTERVAL)? 0 : BeatsToSeconds(breakInterval, bpm);
	    	nextTickTime = nextTickTime + breakIntervalSeconds;
	    }
    }

    public void SetMap(string s){
    	map = s;
    }

    public void SetAudioSource(AudioSource c){
    	lineClip = c;
    }

    void ReadCommand(){
        int commandToRead = (currentCommand>=commands.Length) ?
            3+(currentCommand-commands.Length)%(commands.Length-3) : currentCommand%commands.Length;
    	string command = commands[commandToRead];
    	string[] commandParts = command.Split(',');
    	if(commandParts.Length!=1){
    		string issue = commandParts[0];
    		float val = float.Parse(commandParts[1]);
    		switch (issue){
    			case "BPM":
    				bpm = val;
    				breakInterval = 100000;
    				break;
    			case "NPB":
    				notesPerBar = (int)val;
    				breakInterval = 100000;
    				break;
                case "S":
                    currentStyle = (int)val;
                    breakInterval = 100000;
                    Switch(currentStyle);
                    break;
    			default:
    				breakInterval = SecondsToBeats(val/1000, bpm);
    				break;
    		}
    	}
    	else{
    		if(commandParts[0]==""){
    			breakInterval = 100000;
    			currentCommand++;
    			return;
    		}
    		if(Tick!=null)
    			Tick();
    		string interval = commandParts[0];
    		string[] divisors = interval.Split('/');
    		float val;
    		if(divisors.Length>1)
    			val = float.Parse(divisors[0])/float.Parse(divisors[1]);
    		else
    			val = float.Parse(interval);
    		breakInterval = val;
    	}
    	currentCommand++;
    }

    float BeatsToSeconds(float b, float bpm){
        return 60/(b*bpm);
    }

    float SecondsToBeats(float s, float bpm){ //these are the same function. repeated so names are more descriptive
        return 60/(s*bpm);
    }
}
