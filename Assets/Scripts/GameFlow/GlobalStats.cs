using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalStats : MonoBehaviour
{
	public ItemDatabase database = GlobalManager.instance.itemDatabase;
	public PlayerData initialPlayerData;
	[SerializeField]
	PlayerData playerData;
	public PlayerData PlayerData { get=>playerData; set=>playerData=value; }
	public int Coins{get{return playerData.coins;} private set{playerData.coins = value;}}

	public int LastLevelPassed{get{return playerData.lastLevelPassed;} private set{playerData.lastLevelPassed = value;}}
	
	
	public List<Equippable> OwnedItems { get => database.GetItems(playerData.ownedItems); private set => playerData.ownedItems = Equippable.GetIDs(value); }

	public Song MySong { get => (Song)database.GetItem[playerData.mySong]; private set => playerData.mySong = value.ID; }
	public PowerObject MyBomb { get => (PowerObject)database.GetItem[playerData.myBomb]; private set => playerData.myBomb = value.ID; }

	public List<Weapon> MyWeapons { 
		get {
			List<Equippable> weaponEquips = database.GetItems(playerData.myWeapons);
			List<Weapon> weaponsList = new List<Weapon>();
			foreach(var equip in weaponEquips)
			{
				if (equip is Weapon)
					weaponsList.Add((Weapon)equip);
			}
			return weaponsList;
		}
		private set {
			List<Equippable> wepsToEquips = new List<Equippable>();
			foreach(var wep in value)
			{
				wepsToEquips.Add((Equippable)wep);
			}
			playerData.myWeapons = Equippable.GetIDs(wepsToEquips);
		}
	}
	public int TotalBombs { get => playerData.totalBombs; private set => playerData.totalBombs = value; }
	public int TotalLives { get => playerData.totalLives; private set => playerData.totalLives = value; }

	public List<StatModifier> StatModifiers
	{
		get
		{
			List<Equippable> statEquips = database.GetItems(playerData.statModifiers);
			List<StatModifier> statsList = new List<StatModifier>();
			foreach (var equip in statEquips)
			{
				if (equip is StatModifier)
					statsList.Add((StatModifier)equip);
			}
			return statsList;
		}
		private set
		{
			List<Equippable> statsToEquips = new List<Equippable>();
			foreach (var wep in value)
			{
				statsToEquips.Add((Equippable)wep);
			}
			playerData.myWeapons = Equippable.GetIDs(statsToEquips);
		}
	}
	public Player SelectedPlayer { get => database.GetPlayer[playerData.selectedPlayer]; private set => playerData.selectedPlayer = value.id; }
	public int AllowedWeapons { get => playerData.allowedWeapons; private set => playerData.allowedWeapons = value; }



	int finalLevelIndex = 1;
	public int FinalLevelIndex { get { return finalLevelIndex; } private set { finalLevelIndex = value; } }

	public static GlobalStats instance;
	void Awake()
	{
		if(instance==null){
			instance = this;
		}
		else{
			Destroy(this);
			return;
		}
		DontDestroyOnLoad(gameObject);
	}

    public void ResetStats(){
		playerData = new PlayerData(initialPlayerData);
    }

    public void AddCoins(int c){
    	playerData.coins += c;
    }

	public void Pay(int c)
	{
		playerData.coins -= c;
	}

    public void PassLevel(int l){
    	playerData.lastLevelPassed = Mathf.Max(l, playerData.lastLevelPassed);
    }

	public void AddItem(Equippable equip)
	{
		if(!OwnedItems.Contains(equip))
			OwnedItems.Add(equip);
	}

	public void RemoveItem(Equippable equip)
	{
		if(OwnedItems.Contains(equip))
			OwnedItems.Remove(equip);
	}

	public ItemUIStatus GetItemStatus(Equippable item)
	{
		if (IsEquipped(item))
			return ItemUIStatus.EQUIPPED;
		if (OwnedItems.Contains(item))
			return ItemUIStatus.BOUGHT;
		return ItemUIStatus.NOTBOUGHT;
	}

	bool IsEquipped(Equippable item)
	{
		if(item is Weapon)
		{
			return MyWeapons.Contains((Weapon)item);
		}
		if(item is Song)
		{
			return MySong == (Song)item;
		}
		if(item is PowerObject)
		{
			return MyBomb == (PowerObject)item;
		}
		if (item is StatModifier)
		{
			return StatModifiers.Contains((StatModifier)item);
		}
		else return false;
	}

	public void EquipItem(Equippable item)
	{
		if (item is Weapon)
		{
			MyWeapons.Add((Weapon)item);
			while(MyWeapons.Count>AllowedWeapons)
			{
				MyWeapons.RemoveAt(0);
			}
		}
		if (item is Song)
		{
			MySong = (Song)item;
		}
		if (item is PowerObject)
		{
			MyBomb = (PowerObject)item;
		}
		if (item is StatModifier)
		{
			StatModifiers.Add((StatModifier)item);
		}
	}

	public void UnequipItem(Equippable item)
	{
		if (item is Weapon)
		{
			MyWeapons.Remove((Weapon)item);
		}
		if (item is Song)
		{
			MySong = null;
		}
		if (item is PowerObject)
		{
			MyBomb = null;
		}
		if (item is StatModifier)
		{
			StatModifiers.Remove((StatModifier)item);
		}
	}

	public void SetPlayerData(PlayerData data)
	{
		playerData = data;
	}
}
