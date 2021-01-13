using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class RhythmMapsManager : MonoBehaviour
{

    public delegate void SongChangeAction();
    public static event SongChangeAction NewSong;
    public static event SongChangeAction OnAllSongsFinished;

    public AudioMixer audioMixer;

    public RhythmMap startingMap;
    RhythmMap activeMap;

    [SerializeField]
    bool loopCurrent = false;
    public bool startImmediately;

    public float ActiveTime {get; private set;}

    Dictionary<RhythmMap, bool> mapsFinished = new Dictionary<RhythmMap, bool>();

    void Awake(){
        ActiveTime = 0;
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
        if(startImmediately)
            EnterRhythm();
        RhythmMap[] startingMaps = GetComponentsInChildren<RhythmMap>();
        foreach(var map in startingMaps)
            mapsFinished[map] = false;
    }

    void Update()
    {
        if(activeMap!=null)
            ActiveTime += Time.deltaTime;
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
        if(NewSong!=null)
            NewSong();
    }

    IEnumerator SongChangeSequence(RhythmMap map, float wait, float fadein){
        if(activeMap!=null){
           activeMap.Pause();
        }
        activeMap = null;
        map.UnpauseSongAfter(wait);
        map.FadeIn(fadein);
        yield return new WaitForSeconds(wait);
        activeMap = map;
        mapsFinished[map] = false;
        if(NewSong!=null)
            NewSong();    
    }

    IEnumerator SongChangeRestartSequence(RhythmMap map, float wait, float fadein){
        if(activeMap!=null){
           activeMap.Pause();  
        }
        activeMap = null;
        map.RestartSongAfter(wait);
        map.FadeIn(fadein);
        yield return new WaitForSeconds(wait);
        activeMap = map;
        mapsFinished[map] = false;
        if(NewSong!=null)
            NewSong();
    }

    void SetFinished(RhythmMap map){
        if(!loopCurrent)
            mapsFinished[map] = true;
        foreach(var b in mapsFinished){
            Debug.Log(b.Key + ": " + b.Value);
            if(!b.Value) {
                ChangeSong(b.Key, 0.1f, 2f);
                return;
            }
        }
        OnAllSongsFinished();
        
    }

    public void EnterRhythm(){
        ChangeSongRestart(startingMap, 0.1f, 2f);
    }

    public void PauseActive(){
        if(activeMap!=null)
            activeMap.Pause();
        else
            Debug.LogWarning("No current active map to pause");
    }

    public void UnpauseActive(){
        if(activeMap!=null)
            activeMap.Unpause();        
    }
    
    public RhythmMap GetActiveMap(){
        return activeMap;
    }

    public bool IsStartingMap(){
        return activeMap == startingMap;
    }

    public bool IsNoMap()
    {
        return activeMap == null;
    }

    public float[] GetCurrentMapInfo(){
        if(activeMap==null) return new float[] {0,1};
        return activeMap.GetSongProgressInfo();
    }

    public RhythmMap AddMap(RhythmMap map){
        RhythmMap myMap = Instantiate(map);
        myMap.transform.parent = transform;
        mapsFinished.Add(myMap, false);
        return myMap;
    }
}
