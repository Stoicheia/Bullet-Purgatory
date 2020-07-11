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

    RhythmMap[] maps;
    public int startingActiveMapNumber;
    RhythmMap activeMap;
    bool paused;

    Dictionary<RhythmMap, bool> mapsFinished;

    void Awake(){
    	startingActiveMapNumber = 0;
        paused = true;
    }

    void OnEnable()
    {
        RhythmMap.OnSongEnd += SetFinished;

        maps = GetComponentsInChildren<RhythmMap>();
        mapsFinished = new Dictionary<RhythmMap, bool>();
        foreach(var map in maps)
            mapsFinished.Add(map, false);            
    }

    void OnDisable()
    {
        RhythmMap.OnSongEnd -= SetFinished;
    }

    void Update()
    {   
        if(activeMap!=null)
            Debug.Log(activeMap.name); 
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
        paused = false;
        if(activeMap!=null) activeMap.Pause();
        map.RestartSong();
        mapsFinished[map] = false;
        activeMap = map;
        NewSong();
    }

    IEnumerator SongChangeSequence(RhythmMap map, float wait, float fadein){
        if(activeMap!=null)
           activeMap.Pause();
        paused = true;
        map.UnpauseSongAfter(wait);
        yield return new WaitForSeconds(wait);
        paused = false;
        VolumeFadeIn(fadein);
        mapsFinished[map] = false;
        activeMap = map;
        NewSong();       
    }

    IEnumerator SongChangeRestartSequence(RhythmMap map, float wait, float fadein){
        if(activeMap!=null)
           activeMap.Pause();  
        paused = true;
        map.RestartSongAfter(wait);
        yield return new WaitForSeconds(wait);
        paused = false;
        VolumeFadeIn(fadein);
        mapsFinished[map] = false;
        activeMap = map;
        NewSong();  
    }

    void SetFinished(RhythmMap map){
        mapsFinished[map] = true;
        foreach(var b in mapsFinished){
            if(!b.Value) {
                ChangeSong(b.Key, 0.5f, 2f);
                return;
            }
        }
        GoToState(LevelState.FAILSCREEN);
    }

    public void VolumeFadeIn(float duration){
        StartCoroutine(FadeAudioMixer.StartFade(audioMixer, "vol", duration, 1));
    }

    public void VolumeFadeOut(float duration){
        StartCoroutine(FadeAudioMixer.StartFade(audioMixer, "vol", duration, 0));
    }

    public void EnterRhythm(){
        ChangeSongRestart(maps[startingActiveMapNumber], 0.5f);
    }

    public bool IsPaused(){
        return paused;
    }
    
     public RhythmMap GetActiveMap(){
        return activeMap;
    }
}
