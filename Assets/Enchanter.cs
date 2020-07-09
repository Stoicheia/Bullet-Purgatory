using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enchanter : MonoBehaviour
{

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
        rhythmMaps = FindObjectOfType<RhythmMapsManager>();

        myEnchantSong = Instantiate(enchantSong, transform.position, transform.rotation);
        myEnchantSong.transform.parent = rhythmMaps.transform;
    }


    void Update()
    {
        if(Input.GetKeyDown(enchantButton)&&!used){
        	rhythmMaps.ChangeSongRestart(myEnchantSong, 0.5f);
        }
    }
}
