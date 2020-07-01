using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RhythmicObject))]
public class RhythmListener : MonoBehaviour
{
	RhythmMap map;
	public string part;
	SongLine line;
	RhythmicObject responder;

    void Start()
    {
    	responder = GetComponent<RhythmicObject>();
        map = Object.FindObjectOfType(typeof(RhythmMap)) as RhythmMap;
        line = map.GetLine(part);
        line.Tick += Act;
        line.Switch += ChangeStyle; 
    }

    void OnDisable(){
    	line.Tick -= Act;
    	line.Switch -= ChangeStyle;
    }


    void Update()
    {
        
    }

    public void Act(){
    	responder.Shoot();
    }

    public void ChangeStyle(int i){
    	responder.Change(i);
    }
}
