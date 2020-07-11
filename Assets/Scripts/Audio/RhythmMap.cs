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
    bool fading;
    bool finished;

    string mapData;
    Dictionary<string, SongLine> songLines; //the point of this class: mapDataFile -> mapData -> songLines and sync with audio

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

        fading = false;
        finished = false;
    }

    void Start()
    {
        theManager = transform.parent.GetComponent<RhythmMapsManager>();
        //RestartSongAfter(0.5f);
    }

    void Update()
    {
        Debug.Log(songPlayer.name + ": " + songPlayer.time + "("+(songPlayer.time+Time.deltaTime)+")"+ "... " + songPlayer.clip.length);
        if(songPlayer.time>=songPlayer.clip.length-fadeOutTime && !fading){
            Debug.Log("fade");
            theManager.VolumeFadeOut(Mathf.Max(0,fadeOutTime));
            fading = true;
        }
        if(fading && songPlayer.time == 0 && !finished){
            Debug.Log("end");
            finished = true;
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

    public void RestartSong(){
        fading = false;
        finished = false;
        theManager.SetPause(false);        
        songPlayer.Play();
        foreach(var line in songLines.Values)
            line.StartRhythm();
    }

    public void RestartSongAfter(float s){
        StartCoroutine(StartSongAfterSeconds(s));
    }

    public void UnpauseSongAfter(float s){
        StartCoroutine(UnpauseSongAfterSeconds(s));
    }    

    public void Pause(){
        theManager.SetPause(true);
        songPlayer.Pause();
    }

    public void Unpause(){
        theManager.SetPause(false);        
        songPlayer.UnPause();
    }    

    IEnumerator StartSongAfterSeconds(float s){
        yield return new WaitForSeconds(s);
        RestartSong();
    }

    IEnumerator UnpauseSongAfterSeconds(float s){
        yield return new WaitForSeconds(s);
        Unpause();
    }    



}
