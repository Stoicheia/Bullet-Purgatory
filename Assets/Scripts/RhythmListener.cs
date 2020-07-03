using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RhythmicObject))]
public class RhythmListener : MonoBehaviour
{
	RhythmMapsManager mapManager;
	public string part;
	SongLine line;
	RhythmicObject responder;

    void Start()
    {
    	responder = GetComponent<RhythmicObject>();
        mapManager = Object.FindObjectOfType(typeof(RhythmMapsManager)) as RhythmMapsManager;
        line = mapManager.GetActiveMap().GetLine(part);
        line.Tick += Act;
        line.Switch += ChangeStyle; 
    }

    void OnDisable(){
    	line.Tick -= Act;
    	line.Switch -= ChangeStyle;
    }

    public void Act(){
    	responder.Shoot();
    }

    public void ChangeStyle(int i){
    	responder.Change(i);
    }
}
