using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalStats : MonoBehaviour
{
	public ItemDatabase database;
	public PlayerData initialPlayerData;
	[SerializeField]
	PlayerData playerData;
	public PlayerData PlayerData { get=>playerData; set=>playerData=value; }
	public int Coins{get{return playerData.coins;} private set{playerData.coins = value;}}

	public int LastLevelPassed{get{return playerData.lastLevelPassed;} private set{playerData.lastLevelPassed = value;}}
	
	
	public List<Equippable> OwnedItems { get => playerData.ownedItems; private set => playerData.ownedItems = value; }

	public Song MySong { get => playerData.mySong; private set => playerData.mySong = value; }
	public PowerObject MyBomb { get => playerData.myBomb; private set => playerData.myBomb = value; }

	public List<Weapon> MyWeapons { get => playerData.myWeapons; private set => playerData.myWeapons = value; }
	public int TotalBombs { get => playerData.totalBombs; private set => playerData.totalBombs = value; }
	public int TotalLives { get => playerData.totalLives; private set => playerData.totalLives = value; }

	public List<StatModifier> StatModifiers { get => playerData.statModifiers; private set => playerData.statModifiers = value; }
	public Player SelectedPlayer { get => playerData.selectedPlayer; private set => playerData.selectedPlayer = value; }
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

	private void Start()
	{
		database = GlobalManager.instance.itemDatabase;
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
		if(!playerData.ownedItems.Contains(equip))
			playerData.ownedItems.Add(equip);
	}

	public void RemoveItem(Equippable equip)
	{
		if(playerData.ownedItems.Contains(equip))
			playerData.ownedItems.Remove(equip);
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
