using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmMapsManager : MonoBehaviour
{
    RhythmMap[] maps;
    public int startingActiveMapNumber;
    RhythmMap activeMap;

    void Awake(){
    	startingActiveMapNumber = 0;
        maps = GetComponentsInChildren<RhythmMap>();
        activeMap = maps[startingActiveMapNumber];        
    }

    void Start()
    {

    }

    public void ChangeSong(RhythmMap map){
    	activeMap.Pause();
    	map.Unpause();
    	activeMap = map;
    }

    public void ChangeSongRestart(RhythmMap map){
    	activeMap.Pause();
    	map.RestartSong();
    	activeMap = map;
    }

    public RhythmMap GetActiveMap(){
    	return activeMap;
    }
}
