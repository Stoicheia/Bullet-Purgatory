using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class RhythmMap : MonoBehaviour
{
    public delegate void SongAction(RhythmMap map);
    public static event SongAction OnSongEnd;

    AudioSource songPlayer;
    public AudioClip musicClip;
    public TextAsset mapDataFile;
    public SongLine lineObjectPrefab;

    RhythmMapsManager theManager;

    [SerializeField]
    private float fadeOutTime = 0;
    bool finished;

    string mapData;
    Dictionary<string, SongLine> songLines; //the point of this class: mapDataFile -> mapData -> songLines and sync with audio

    IEnumerator songStartRoutine;

    void Awake(){
    	songPlayer = GetComponent<AudioSource>();
        songPlayer.clip = musicClip;
        mapData = mapDataFile.ToString();
        mapData = mapData.Replace("\n", " ");
        string[] lines = mapData.Split('#');

        songLines = new Dictionary<string, SongLine>();
        foreach(var nameAndDataStrings in lines){
            CreateLineFromData(nameAndDataStrings);
        }
    }

    void Start()
    {
        theManager = transform.parent.GetComponent<RhythmMapsManager>();
        //RestartSongAfter(0.5f);
    }

    void Update()
    {
        if(songPlayer.time>=songPlayer.clip.length-fadeOutTime){
            theManager.audioMixer.SetFloat("vol", -80*(1-(songPlayer.clip.length-songPlayer.time)/fadeOutTime));
        }
        else{
            theManager.audioMixer.SetFloat("vol", 0);
        }
        //Debug.Log(songPlayer.name + ": " + songPlayer.time + "("+(songPlayer.time+Time.deltaTime)+")"+ "... " + songPlayer.clip.length);
        if(songPlayer.time>=songPlayer.clip.length-2*Time.deltaTime){
            songPlayer.time = 0;
            Pause();
            Debug.Log("end");
            if(OnSongEnd!=null)
                OnSongEnd(this);
        }
    }

    void CreateLineFromData(string data){
        string[] nameAndData = data.Split(':');

        SongLine line = Instantiate(lineObjectPrefab, transform.position, transform.rotation) as SongLine;
        line.transform.parent = transform;
        line.name = line.part = nameAndData[0];
        line.SetMap(nameAndData[1]);
        line.SetAudioSource(songPlayer);

        songLines.Add(nameAndData[0],line);        
    }

    public SongLine GetLine(string l){
        return songLines[l];
    }

    public void RestartSongAfter(float s){
        if(songStartRoutine!=null)
            StopCoroutine(songStartRoutine);
        songStartRoutine = StartSongAfterSeconds(s);
        StartCoroutine(songStartRoutine);
    }

    public void UnpauseSongAfter(float s){
        if(songStartRoutine!=null)
            StopCoroutine(songStartRoutine);        
        songStartRoutine = UnpauseSongAfterSeconds(s);
        StartCoroutine(songStartRoutine);
    }    

    public void Pause(){
        if(songStartRoutine!=null)
            StopCoroutine(songStartRoutine);        
        songPlayer.Pause();
    }

    public void Unpause(){
        songPlayer.UnPause();
    }    

    public void RestartSong(){
        songPlayer.Play();
        foreach(var line in songLines.Values)
            line.StartRhythm();
    }

    IEnumerator StartSongAfterSeconds(float s){
        yield return new WaitForSeconds(s);
        RestartSong();
    }

    IEnumerator UnpauseSongAfterSeconds(float s){
        yield return new WaitForSeconds(s);
        Debug.Log("ok");
        Unpause();
    }    

}
