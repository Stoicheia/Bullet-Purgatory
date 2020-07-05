using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TickAlternater : RhythmicObject
{
	List<RhythmicObject> alternatingShooters;
	int currentAlternation;
	public bool alternateStyle;

	void Start(){
		currentAlternation = 0;
		List<RhythmicObject> unfilteredAlternatingShooters = new List<RhythmicObject>(GetComponentsInChildren<RhythmicObject>());
		alternatingShooters = new List<RhythmicObject>();
		foreach (var s in unfilteredAlternatingShooters){
			if(s.GetComponent<RhythmListener>()==null)
				alternatingShooters.Add(s);
			else if(s.GetComponent<TickAlternater>()==null)
				Debug.LogWarning(s.name+" has a rhythm of its own! Remove its RhythmListener component to include it in the alternation...");
		}
	}

    public override void Shoot(){
    	if(!shootEnabled)
    		return;
    	alternatingShooters[currentAlternation%alternatingShooters.Count].Shoot();
    	if(!alternateStyle)
    		currentAlternation++;
    }

    public override void Change(int i){
    	foreach(var s in alternatingShooters)
    		s.Change(i);
    	if(alternateStyle)
    		currentAlternation = i;
    }
}
