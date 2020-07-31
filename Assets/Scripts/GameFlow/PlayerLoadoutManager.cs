using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLoadoutManager : MonoBehaviour
{
	public Player selectedPlayer;

	public static PlayerLoadoutManager instance;
	public PlayerLoadout[] startingLoadouts;
	public Dictionary<Player, PlayerLoadout> loadouts = new Dictionary<Player, PlayerLoadout>();

	void Awake()
	{
		if(instance==null)
			instance = this;
		else{
			Destroy(gameObject);
			return;
		}
		DontDestroyOnLoad(gameObject);
		UseStartingLoadouts(); //read from save later
	}

	void Start()
	{

	}

	void UseStartingLoadouts()
	{
		foreach(var load in startingLoadouts){
			loadouts[load.player] = load;
		}
	}
}

[System.Serializable]
public class PlayerLoadout
{
	public Player player;

	public int allowedShooters = 1;
	[SerializeField]
	List<RhythmicObject> shooters = new List<RhythmicObject>();

	public int allowedSongs = 1;
	[SerializeField]
	List<RhythmMap> enchantSongs = new List<RhythmMap>();

	public int allowedPowers = 1;
	[SerializeField]
	public List<Power> powers = new List<Power>();

	[SerializeField]
	List<Upgrade> upgrades = new List<Upgrade>();

	public void AddShooter(RhythmicObject shooter, int i)
	{
		if(i>=allowedShooters) return;
		shooters[i] = shooter;
	}

	public void AddSong(RhythmMap map)
	{
		for(int i=1; i<allowedSongs; i++){
			enchantSongs[i] = enchantSongs[i-1];
		}
		enchantSongs[0] = map;
	}

	public void AddPower(Power power)
	{
		for(int i=1; i<allowedPowers; i++){
			powers[i] = powers[i-1];
		}
		powers[0] = power;
	}

	public void AddUpgrade(Upgrade upgrade)
	{
		bool flag = true;
		List<Upgrade> newUpgrades = new List<Upgrade>(upgrades);
		for(int i=0; i<upgrades.Count; i++){
			if(upgrades[i].GetSignature()==upgrade.GetSignature()){
				newUpgrades[i] = upgrade;
				flag = false;
			}
		}
		if(flag){
			newUpgrades.Add(upgrade);
		}
		upgrades = new List<Upgrade>(newUpgrades);
	}

	public List<RhythmicObject> GetShooters()
	{
		return shooters;
	}

	public List<RhythmMap> GetSongs()
	{
		return enchantSongs;
	}

	public List<Power> GetPowers()
	{
		return powers;
	}

	public List<Upgrade> GetUpgrades()
	{
		return upgrades;
	}
}
