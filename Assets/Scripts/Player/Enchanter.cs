using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enchanter : MonoBehaviour
{
	LevelStateManager stateManager;
	RhythmMapsManager rhythmMaps;

	public RhythmMap[] enchantSongs;
	List<RhythmMap> myEnchantSongs;

    int currentEnchantment;
    int totalEnchantments;

    [SerializeField]
    private float enchantCooldown = 5;
    float nextEnchantTime;

	void Awake()
	{
        currentEnchantment = 0;
        nextEnchantTime = 0;
        myEnchantSongs = new List<RhythmMap>();
	}

    void Start()
    {
        stateManager = FindObjectOfType<LevelStateManager>();
        rhythmMaps = FindObjectOfType<RhythmMapsManager>();

        foreach(RhythmMap enchantSong in enchantSongs){
            RhythmMap myEnchantSong = rhythmMaps.AddMap(enchantSong);
            myEnchantSongs.Add(myEnchantSong);
        }
        totalEnchantments = myEnchantSongs.Count;
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
        rhythmMaps.ChangeSongRestart(myEnchantSongs[currentEnchantment], 0.5f, 0.1f);
        currentEnchantment++;
        nextEnchantTime = Time.time + enchantCooldown;
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
}
