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
    }


    void OnEnable()
    {
        mapManager = Object.FindObjectOfType(typeof(RhythmMapsManager)) as RhythmMapsManager;
        RhythmMap mapOnSpawn = mapManager.GetActiveMap();
        if(mapOnSpawn!=null){
            line = mapOnSpawn.GetLine(part);
            line.Tick += Act;
            line.Switch += ChangeStyle;
        }            
        RhythmMapsManager.NewSong += ChangeSong;
    }

    void OnDisable(){
        RhythmMapsManager.NewSong -= ChangeSong;
        if(line!=null){
            line.Tick -= Act;
            line.Switch -= ChangeStyle;        
        }
    }

    public void Act(){
    	responder.Shoot();
    }

    public void ChangeStyle(int i){
    	responder.Change(i);
    }

    public void ChangeSong(){
        if(line!=null){
            line.Tick -= Act;
            line.Switch -= ChangeStyle;
        }
        line = mapManager.GetActiveMap().GetLine(part);
        line.Tick += Act;
        line.Switch += ChangeStyle;
    }

    public float GetNextTickTime(){
        return line.GetNextAudioTickTime();
    }

    public float GetOneBeatTime(){
        return 60/line.GetBPM();
    }
}
