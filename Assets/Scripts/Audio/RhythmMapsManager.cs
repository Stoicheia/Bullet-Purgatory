using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmMapsManager : MonoBehaviour
{
    public delegate void StateChangeAction(LevelState state);
    public static event StateChangeAction GoToState;

    public delegate void SongChangeAction();
    public static event SongChangeAction NewSong;

    RhythmMap[] maps;
    public int startingActiveMapNumber;
    RhythmMap activeMap;

    Dictionary<RhythmMap, bool> mapsFinished;

    void Awake(){
    	startingActiveMapNumber = 0;
        maps = GetComponentsInChildren<RhythmMap>();
        mapsFinished = new Dictionary<RhythmMap, bool>();
        foreach(var map in maps)
            mapsFinished.Add(map, false);      
    }

    void OnEnable()
    {
        RhythmMap.OnSongEnd += SetFinished;
    }

    void OnDisable()
    {
        RhythmMap.OnSongEnd -= SetFinished;
    }

    void Start()
    {
        
    }

    public void ChangeSong(RhythmMap map, float delay){
        if(activeMap!=null)
    	   activeMap.Pause();
    	map.UnpauseSongAfter(delay);
    	activeMap = map;
        NewSong();
    }

    public void ChangeSongRestart(RhythmMap map, float delay){
        if(activeMap!=null)
           activeMap.Pause();
    	map.RestartSongAfter(delay);
        mapsFinished[map] = false;
    	activeMap = map;
        NewSong();
    }

    public RhythmMap GetActiveMap(){
    	return activeMap;
    }

    void SetFinished(RhythmMap map){
        mapsFinished[map] = true;
        foreach(var b in mapsFinished){
            if(!b.Value) ChangeSong(b.Key, 0.5f);
            return;
        }
        GoToState(LevelState.FAILSCREEN);
    }

    public void EnterRhythm(){
        ChangeSongRestart(maps[startingActiveMapNumber], 0.5f);
    }
}
