﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enchanter : MonoBehaviour
{
    public delegate void UseAction();
    public static event UseAction OnUsePower;

	LevelStateManager stateManager;
	RhythmMapsManager rhythmMaps;

    [SerializeField]
	public List<Song> enchantSongs;
	List<RhythmMap> myEnchantSongs;

    int currentEnchantment;
    public int CurrentEnchantment{get{return currentEnchantment;} private set{currentEnchantment = value;}}
    int totalEnchantments;

    [SerializeField]
    private float enchantCooldown = 5;
    float nextEnchantTime;

	void Awake()
	{
        currentEnchantment = 0;
        nextEnchantTime = 0;
        enchantSongs = new List<Song>();
        myEnchantSongs = new List<RhythmMap>();
        stateManager = FindObjectOfType<LevelStateManager>();
        rhythmMaps = FindObjectOfType<RhythmMapsManager>();
	}

    void Start()
    {
        
    }

    void OnEnable(){
    	RhythmMapsManager.NewSong += CheckAutoUse;
    }

    void OnDisable(){
    	RhythmMapsManager.NewSong -= CheckAutoUse;    	
    }


    void Update()
    {
        if(Keybinds.instance.GetInputDown("Enchant")){
        	Activate();
        }
    }

    public void Activate()
    {
        if(!EnchantConditionsMet()) return;
        rhythmMaps.ChangeSongRestart(myEnchantSongs[currentEnchantment], 0.5f, 0.05f);
        currentEnchantment++;
        nextEnchantTime = Time.time + enchantCooldown;
        if(OnUsePower!=null)
            OnUsePower();
    }

    bool EnchantConditionsMet()
    {
        return currentEnchantment<totalEnchantments&&stateManager.levelState==LevelState.PLAYING
                    &&Time.time>=nextEnchantTime&&rhythmMaps.GetActiveMap()!=null;
    }

    void CheckAutoUse()
    {
        for(int i=currentEnchantment; i<totalEnchantments; i++){
            if(rhythmMaps.GetActiveMap()==myEnchantSongs[i]){
                currentEnchantment = i+1;
                break;
            }
        }
    }

    public void UpdateSongs()
    {
        foreach(Song enchantSong in enchantSongs){
            RhythmMap myEnchantSong = rhythmMaps.AddMap(enchantSong.RhythmMap);
            myEnchantSongs.Add(myEnchantSong);
        }
        totalEnchantments = myEnchantSongs.Count;
    }

    public void AddSong(Song map)
    {
        enchantSongs.Add(map);
    }

    public Song GetSong(int i){
        return enchantSongs[i];
    }

    public int GetSongCount()
    {
        return enchantSongs.Count;
    }
}
