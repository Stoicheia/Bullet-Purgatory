using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerIcons : MonoBehaviour
{
	public static PowerIcons instance;
	public Sprite defaultPower;
	public Sprite defaultSong;

	[Space(20)]

	
	public PowerIconPair[] powers;
	public SongIconPair[] songs;
	public Dictionary<Power, Sprite> iconOfPower = new Dictionary<Power, Sprite>();
	public Dictionary<RhythmMap, Sprite> iconOfSong = new Dictionary<RhythmMap, Sprite>();
	public Dictionary<RhythmMap, Sprite> iconOfSongInactive = new Dictionary<RhythmMap, Sprite>();
	

    void Awake()
    {
        if(instance==null)
        	instance = this;
        else{
        	Destroy(gameObject);
        	return;
        }
    }
    
    void Start()
    {
    	RefreshIcons();
    }

    void Update()
    {
        
    }

    void RefreshIcons()
    {
    	foreach(var p in powers){
    		if(p.power==null || p.icon==null) continue;
    		iconOfPower[p.power] = p.icon;
    	}

    	foreach(var s in songs){
    		if(s.song==null || s.icon==null) continue;
    		iconOfSong[s.song] = s.icon;
    	}

    	foreach(var s in songs){
    		if(s.song==null || s.inactiveIcon==null) continue;
    		iconOfSongInactive[s.song] = s.inactiveIcon;
    	}
    }

    public Sprite GetPowerIcon(Power p){
    	if(!iconOfPower.ContainsKey(p)) return defaultPower;
    	return iconOfPower[p];
    }

    public Sprite GetSongIcon(RhythmMap m){
    	if(!iconOfSong.ContainsKey(m)) return defaultSong;
    	return iconOfSong[m];
    }

    public Sprite GetSongIconInactive(RhythmMap m){
    	if(!iconOfSongInactive.ContainsKey(m)) return defaultSong;
    	return iconOfSongInactive[m];
    }
}

[System.Serializable]
public class PowerIconPair
{
	public Power power;
	public Sprite icon;
}

[System.Serializable]
public class SongIconPair
{
	public RhythmMap song;
	public Sprite icon;
	public Sprite inactiveIcon;
}