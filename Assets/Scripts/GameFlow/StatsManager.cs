﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CauseOfFail{DEATH, TIMEOUT}

public class StatsManager : MonoBehaviour
{
    public int kills {get; private set;}
    public int lives {get; private set;} 
    public float grazing {get; private set;}
    public CauseOfFail causeOfFail {get; private set;}

    public int currentWave {get; private set;}
    public int totalWaves {get; private set;}

    RhythmMapsManager rhythmMaps;
    public float currentSongTime {get; private set;}
    public float currentSongLength {get; private set;}
    public float totalSongTime {get; private set;}
    public float totalSongLength {get; private set;}
    public bool isMainSong {get; private set;}

    void Start()
    {
        rhythmMaps = FindObjectOfType<RhythmMapsManager>();
        kills = 0;
        grazing = 0;
    }

    void OnEnable()
    {
    	Enemy.OnDeath += IncrementKills;
        Player.OnPlayerDeath += SetFailFromDeath;
        Grazebox.OnGraze += AddGraze;
        RhythmMapsManager.OnAllSongsFinished += SetFailFromTimeout;
        WaveSpawner.OnNextWave += UpdateWaveInfo;
        Player.OnLivesChange += ChangeLives;
    }

    void OnDisable()
    {
    	Enemy.OnDeath -= IncrementKills;
    }

    void Update()
    {
        UpdateSongProgress();
    }

    void IncrementKills (string s) => kills++;
    void ChangeLives (int l) => lives = l;
    void AddGraze (float s) => grazing += s;
    void UpdateWaveInfo(int[] waveInfo) {
        currentWave = waveInfo[0]; totalWaves = waveInfo[1];
    }
    void UpdateSongProgress(){
        float[] songInfo = rhythmMaps.GetCurrentMapInfo();
        currentSongTime = songInfo[0];
        currentSongLength = songInfo[1];
        float[] allSongInfo = rhythmMaps.GetAllMapsInfo();
        totalSongTime = allSongInfo[0];
        totalSongLength = allSongInfo[1];
        isMainSong = rhythmMaps.IsStartingMap();
    }
    void SetFailFromDeath () => causeOfFail = CauseOfFail.DEATH;
    void SetFailFromTimeout () => causeOfFail = CauseOfFail.TIMEOUT;
}
