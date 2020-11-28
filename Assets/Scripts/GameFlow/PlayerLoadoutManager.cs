using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLoadoutManager : MonoBehaviour
{
	//here be code

	/*public Player selectedPlayer;

	public static PlayerLoadoutManager instance;
	public List<PlayerLoadout> startingLoadouts;
	[SerializeField]
	List<PlayerLoadout> loadouts = new List<PlayerLoadout>();
	public List<PlayerLoadout> Loadouts { get => loadouts; set => loadouts = value; }

	void Awake()
	{
		if(instance==null)
			instance = this;
		else{
			Destroy(this);
			return;
		}
		DontDestroyOnLoad(gameObject);
	}

	void Start()
	{

	}

	public void UseStartingLoadouts()
	{
		for (int i= 0; i<startingLoadouts.Count; i++){
			if (loadouts.Count <= i) loadouts.Add(startingLoadouts[i]);
			loadouts[i] = startingLoadouts[i];
		}
	}

	public PlayerLoadout GetLoadout(Player player)
	{
		return loadouts[player.id];
	}

	public void AddToLoadout(Player player, Song toAdd)
	{
		loadouts[player.id].AddSong(toAdd);
	}

	public void AddToLoadout(Player player, PowerObject toAdd)
	{
		loadouts[player.id].AddPower(toAdd);
	}

	public void AddToLoadout(Player player, Weapon toAdd)
	{
		loadouts[player.id].AddShooter(toAdd);
	}
	public void AddToLoadout(Player player, Weapon toAdd, int i)
	{
		loadouts[player.id].AddShooter(toAdd, i);
	}
	public void AddToLoadout(Player player, StatModifier toAdd)
	{
		loadouts[player.id].AddUpgrade(toAdd);
	}

	public void AddToLoadout(Equippable item)
	{
		if (item is Song) AddToLoadout(selectedPlayer, (Song) item);
		else if (item is PowerObject) AddToLoadout(selectedPlayer, (PowerObject)item);
		else if (item is Weapon) AddToLoadout(selectedPlayer, (Weapon)item);
		else if (item is StatModifier) AddToLoadout(selectedPlayer, (StatModifier)item);
	}

	public bool IsEquipped(Equippable e)
	{
		foreach(var loadout in loadouts)
		{
			if (loadout.GetAllEquipped().Contains(e))
			{
				return true;
			}
		}
		return false;
	}*/
}
