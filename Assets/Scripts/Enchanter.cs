using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enchanter : MonoBehaviour
{
	LevelStateManager state;
	RhythmMapsManager rhythmMaps;
	KeyCode enchantButton = KeyCode.G;

	public RhythmMap enchantSong;
	RhythmMap myEnchantSong;
	bool used;

	void Awake()
	{
		used = false;
	}

    void Start()
    {
        enchantButton = Keybinds.instance.keys["Enchant"];
        state = FindObjectOfType<LevelStateManager>();
        rhythmMaps = FindObjectOfType<RhythmMapsManager>();

        myEnchantSong = Instantiate(enchantSong, transform.position, transform.rotation);
        myEnchantSong.transform.parent = rhythmMaps.transform;
    }

    void OnEnable(){
    	RhythmMapsManager.NewSong += CheckAutoUse;
    }

    void OnDisable(){
    	RhythmMapsManager.NewSong -= CheckAutoUse;    	
    }


    void Update()
    {
        if(Input.GetKeyDown(enchantButton)&&!used&&state.levelState==LevelState.PLAYING){
        	rhythmMaps.ChangeSongRestart(myEnchantSong, 0.1f, 0.5f, 0.1f);
        	used = true;
        }
    }

    void CheckAutoUse()
    {
    	if(rhythmMaps.GetActiveMap()==myEnchantSong)
    		used = true;
    }
}
