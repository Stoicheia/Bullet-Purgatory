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

    Dictionary<RhythmMap, bool> mapsFinished;

    void Awake(){
    	startingActiveMapNumber = 0;
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

    void Start()
    {
        
    }

    public void ChangeSong(RhythmMap map, float delay){
        StartCoroutine(SongChangeSequence(map, delay, delay/2, delay));
    }

    public void ChangeSongRestart(RhythmMap map, float delay){
        StartCoroutine(SongChangeRestartSequence(map, delay, delay/2, delay));
    }    

    public void ChangeSong(RhythmMap map, float fadeout, float wait, float fadein){
        StartCoroutine(SongChangeSequence(map, fadeout, wait, fadein));
    }

    public void ChangeSongRestart(RhythmMap map, float fadeout, float wait, float fadein){
        StartCoroutine(SongChangeRestartSequence(map, fadeout, wait, fadein));
    }

    public void FadeOutIn(float fadeout, float fadein){
        StartCoroutine(FadeOutInSequence(fadeout, fadein));
    }

    IEnumerator SongChangeSequence(RhythmMap map, float fadeout, float wait, float fadein){
        VolumeFadeOut(fadeout);
        yield return new WaitForSeconds(fadeout);
        if(activeMap!=null)
           activeMap.Pause();
        map.UnpauseSongAfter(wait);
        yield return new WaitForSeconds(wait);
        VolumeFadeIn(fadein);
        mapsFinished[map] = false;
        activeMap = map;
        NewSong();       
    }

    IEnumerator SongChangeRestartSequence(RhythmMap map, float fadeout, float wait, float fadein){
        VolumeFadeOut(fadeout);
        yield return new WaitForSeconds(fadeout);
        if(activeMap!=null)
           activeMap.Pause();
        map.RestartSongAfter(wait);
        yield return new WaitForSeconds(wait);
        VolumeFadeIn(fadein);
        mapsFinished[map] = false;
        activeMap = map;
        NewSong();   
    }

    IEnumerator FadeOutInSequence(float fadeout, float fadein){
        VolumeFadeOut(fadeout);
        yield return new WaitForSeconds(fadeout);
        VolumeFadeIn(fadein);
    }


    public RhythmMap GetActiveMap(){
    	return activeMap;
    }

    void SetFinished(RhythmMap map){
        mapsFinished[map] = true;
        foreach(var b in mapsFinished){
            if(!b.Value) {
                ChangeSong(b.Key, 0.1f, 0.5f, 2f);
                return;
            }
        }
        GoToState(LevelState.FAILSCREEN);
    }

    void VolumeFadeIn(float duration){
        StartCoroutine(FadeAudioMixer.StartFade(audioMixer, "vol", duration, 1));
    }

    void VolumeFadeOut(float duration){
        StartCoroutine(FadeAudioMixer.StartFade(audioMixer, "vol", duration, 0));
    }

    public void EnterRhythm(){
        ChangeSongRestart(maps[startingActiveMapNumber], 0.1f, 0.5f, 2f);
    }
    
}
