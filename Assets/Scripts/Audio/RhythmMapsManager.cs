using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class RhythmMapsManager : MonoBehaviour
{
    public delegate void StateChangeAction(LevelState state);
    public static event StateChangeAction GoToState;

    public delegate void SongChangeAction();
    public static event SongChangeAction NewSong;

    public AudioMixer audioMixer;

    public RhythmMap startingMap;
    RhythmMap activeMap;

    Dictionary<RhythmMap, bool> mapsFinished;

    void Awake(){

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
        RhythmMap[] startingMaps = GetComponentsInChildren<RhythmMap>();
        mapsFinished = new Dictionary<RhythmMap, bool>();
        foreach(var map in startingMaps)
            mapsFinished.Add(map, false);  
    }

    public void ChangeSong(RhythmMap map, float delay){
        StartCoroutine(SongChangeSequence(map, delay, delay));
    }

    public void ChangeSongRestart(RhythmMap map, float delay){
        StartCoroutine(SongChangeRestartSequence(map, delay, delay));
    }    

    public void ChangeSong(RhythmMap map, float wait, float fadein){
        StartCoroutine(SongChangeSequence(map, wait, fadein));
    }

    public void ChangeSongRestart(RhythmMap map, float wait, float fadein){
        StartCoroutine(SongChangeRestartSequence(map, wait, fadein));
    }

    public void ChangeSongImmediate(RhythmMap map){
        if(activeMap!=null) {activeMap.Pause();}
        activeMap = map;
        map.RestartSong();
        mapsFinished[map] = false;
        NewSong();
    }

    IEnumerator SongChangeSequence(RhythmMap map, float wait, float fadein){
        if(activeMap!=null){
           activeMap.Pause();
        }
        activeMap = null;
        map.UnpauseSongAfter(wait);
        yield return new WaitForSeconds(wait);
        activeMap = map;
        mapsFinished[map] = false;
        NewSong();       
    }

    IEnumerator SongChangeRestartSequence(RhythmMap map, float wait, float fadein){
        if(activeMap!=null){
           activeMap.Pause();  
        }
        activeMap = null;
        map.RestartSongAfter(wait);
        yield return new WaitForSeconds(wait);
        activeMap = map;
        mapsFinished[map] = false;
        NewSong();  
    }

    void SetFinished(RhythmMap map){
        mapsFinished[map] = true;
        foreach(var b in mapsFinished){
            Debug.Log(b.Key + ": " + b.Value);
            if(!b.Value) {
                ChangeSong(b.Key, 0.5f, 2f);
                return;
            }
        }
        GoToState(LevelState.FAILSCREEN);
    }

    public void EnterRhythm(){
        ChangeSongRestart(startingMap, 0.5f);
    }

    public void PauseActive(){
        if(activeMap!=null)
            activeMap.Pause();
        else
            Debug.LogWarning("No current active map");
    }

    public void UnpauseActive(){
        if(activeMap!=null)
            activeMap.Unpause();
        else
            Debug.LogWarning("No current active map");            
    }
    
    public RhythmMap GetActiveMap(){
        return activeMap;
    }

    public RhythmMap AddMap(RhythmMap map){
        RhythmMap myMap = Instantiate(map);
        myMap.transform.parent = transform;
        mapsFinished.Add(myMap, false);
        return myMap;
    }
}
