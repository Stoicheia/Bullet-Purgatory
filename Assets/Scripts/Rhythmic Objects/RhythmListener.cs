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

    void Awake()
    {
        mapManager = FindObjectOfType<RhythmMapsManager>();
    	responder = GetComponent<RhythmicObject>();          
    }


    void OnEnable()
    {   
        ChangeSong();   
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
        RhythmMap activeMap = mapManager.GetActiveMap();
        if(activeMap!=null)
            line = activeMap.GetLine(part);
        if(line!=null){
            line.Tick += Act;
            line.Switch += ChangeStyle;
        }
    }

    public float GetNextTickTime(){
        return line.GetNextAudioTickTime();
    }

    public float BeatsToSeconds(float b){
        if(line==null) return 0;
        return b*60/line.GetBPM();
    }
}
