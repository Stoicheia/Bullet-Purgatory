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
    }

    void Start()
    {
        theManager = transform.parent.GetComponent<RhythmMapsManager>();
        //RestartSongAfter(0.5f);
    }

    void Update()
    {
        if(songPlayer.time>=songPlayer.clip.length-fadeOutTime && !fading){
            theManager.VolumeFadeOut(Mathf.Max(0,fadeOutTime));
            fading = true;
        }
        if(songPlayer.time>=songPlayer.clip.length){
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
        songPlayer.Play();
        foreach(var line in songLines.Values)
            line.StartRhythm();
    }

    public void RestartSongAfter(float s){
        StartCoroutine(StartSongAfterSeconds(0.5f));
    }

    public void UnpauseSongAfter(float s){
        StartCoroutine(UnpauseSongAfterSeconds(0.5f));
    }    

    public void Pause(){
        songPlayer.Pause();
    }

    public void Unpause(){
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
