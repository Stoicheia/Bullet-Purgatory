using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GlobalStats : MonoBehaviour
{
	GlobalManager global;
	ItemDatabase itemDatabase;
	public PlayerData initialPlayerData;
	[SerializeField]
	PlayerData playerData;
	public PlayerData PlayerData { get=>playerData; set=>playerData=value; }

	[SerializeField] private LevelData levelData;

	public LevelData LevelData
	{
		get => levelData;
		set => levelData = value;
	}

	public int Coins{get{return playerData.coins;} private set{playerData.coins = value;}}

	public int LastLevelPassed{get{return playerData.lastLevelPassed;} private set{playerData.lastLevelPassed = value;}}
	
	
	public List<Equippable> OwnedItems
	{
		get
		{
			return global.ItemDatabase.GetItems(playerData.ownedItems);
		}
		private set
		{
			playerData.ownedItems = Equippable.GetIDs(value);
		}
	}


	public Song MySong { get => (Song)global.ItemDatabase.GetItem(playerData.mySong); private set => playerData.mySong = value==null? "" : value.ID; }
	public PowerObject MyBomb { get => (PowerObject)global.ItemDatabase.GetItem(playerData.myBomb); private set => playerData.myBomb = value==null? "" : value.ID; }

	public List<Weapon> MyWeapons
	{
		get
		{
			List<Weapon> weapons = new List<Weapon>();
			foreach(string id in playerData.myWeapons)
			{
				weapons.Add((Weapon)global.ItemDatabase.GetItem(id));
			}
			return weapons;
		}
		private set
		{
			playerData.myWeapons = Equippable.GetIDs(value.ConvertAll<Equippable>(x => (Equippable)x));
		}
	}

	public int TotalBombs { get => playerData.totalBombs; private set => playerData.totalBombs = value; }
	public int TotalLives { get => playerData.totalLives; private set => playerData.totalLives = value; }

	public List<StatModifier> StatModifiers
	{
		get
		{
			List<StatModifier> stats = new List<StatModifier>();
			foreach (string id in playerData.statModifiers)
			{
				stats.Add((StatModifier)global.ItemDatabase.GetItem(id));
			}
			return stats;
		}
		private set
		{
			playerData.myWeapons = Equippable.GetIDs(value.ConvertAll<Equippable>(x => (Equippable)x));
		}
	}
	public Player SelectedPlayer { get => global.ItemDatabase.GetPlayer(playerData.selectedPlayer); private set => playerData.selectedPlayer = value.id; }
	public int AllowedWeapons { get => playerData.allowedWeapons; private set => playerData.allowedWeapons = value; }



	[SerializeField] int finalLevelIndex = 100;
	public int FinalLevelIndex { get { return finalLevelIndex; } private set { finalLevelIndex = value; } }

	public static GlobalStats instance;

	void Awake()
	{
		if (instance==null){
			instance = this;
		}
		else{
			Destroy(this);
			return;
		}
		DontDestroyOnLoad(gameObject);
	}

	private void OnEnable()
	{
		global = GlobalManager.instance;
	}

	public void ResetStats(){
		playerData = new PlayerData(initialPlayerData);
		levelData = new LevelData();
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
		if (!OwnedItems.Contains(equip) && equip != null)
		{
			playerData.ownedItems.Add(equip.ID);
		}
	}

	public void RemoveItem(Equippable equip)
	{
		if(OwnedItems.Contains(equip) && equip != null)
			playerData.ownedItems.Remove(equip.ID);
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
			playerData.myWeapons.Add(item.ID);
			while(MyWeapons.Count>AllowedWeapons)
			{
				playerData.myWeapons.RemoveAt(0);
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
			playerData.statModifiers.Add(item.ID);
		}
	}

	public void UnequipItem(Equippable item)
	{
		if (item is Weapon)
		{
			playerData.myWeapons.Remove(item.ID);
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
			playerData.statModifiers.Remove(item.ID);
		}
	}

	public void SetPlayerData(PlayerData data)
	{
		playerData = data;
	}
}
