using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class RhythmMap : MonoBehaviour
{
    AudioSource songPlayer;
    public AudioClip musicClip;
    public TextAsset mapDataFile;
    public SongLine lineObjectPrefab;
    string mapData;
    Dictionary<string, SongLine> songLines; //the point of this class: mapDataFile -> mapData -> songLines and sync with audio
    
    void Awake(){
    	songPlayer = GetComponent<AudioSource>();
        songPlayer.clip = musicClip;
        mapData = mapDataFile.ToString();
        mapData = mapData.Replace("\n", " ");
        string[] lines = mapData.Split('#');

        songLines = new Dictionary<string, SongLine>();
        foreach(var nameAndDatas in lines){
            string[] nameAndData = nameAndDatas.Split(':');

            SongLine line = Instantiate(lineObjectPrefab, transform.position, transform.rotation) as SongLine;
            line.transform.parent = transform;
            line.name = line.part = nameAndData[0];
            line.SetMap(nameAndData[1]);
            line.SetAudioSource(songPlayer);

            songLines.Add(nameAndData[0],line);
        }
    }

    void Start()
    {
        RestartSong();
    }

    void Update()
    {
        if(!songPlayer.isPlaying){
            RestartSong();
        }
    }

    public SongLine GetLine(string l){
        return songLines[l];
    }

    void RestartSong(){
        songPlayer.Play();
        foreach(var line in songLines.Values)
            line.StartRhythm();
    }
}
