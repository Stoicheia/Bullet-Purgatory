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
    }

    void Start()
    {
        maps = GetComponentsInChildren<RhythmMap>();
        activeMap = maps[startingActiveMapNumber];
    }

    public void ChangeSong(RhythmMap map, bool restart){
    	activeMap.Pause();
    	if(restart){
    		map.RestartSong();
    	}
    	else{
    		map.Unpause();
    	}
    	activeMap = map;
    }

    public RhythmMap GetActiveMap(){
    	return activeMap;
    }
}
