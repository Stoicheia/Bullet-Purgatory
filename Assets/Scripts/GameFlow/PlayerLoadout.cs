using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class PlayerLoadout
{
	public Player player;

	public int allowedShooters = 1;
	[SerializeField]
	List<Weapon> shooters = new List<Weapon>();

	public int allowedSongs = 1;
	[SerializeField]
	List<Song> enchantSongs = new List<Song>();

	public int allowedPowers = 1;
	[SerializeField]
	public List<PowerObject> powers = new List<PowerObject>();

	[SerializeField]
	List<StatModifier> upgrades = new List<StatModifier>();


	public void AddShooter(Weapon shooter, int i)
	{
		if (i >= allowedShooters) return;
		shooters[i] = shooter;
	}

	public void AddShooter(Weapon shooter)
	{
		for (int i = 0; i < allowedShooters; i++)
		{
			shooters[i] = shooters[i - 1];
		}
		shooters[0] = shooter;
	}

	public void AddSong(Song map)
	{
		for (int i = 1; i < allowedSongs; i++) {
			enchantSongs[i] = enchantSongs[i - 1];
		}
		enchantSongs[0] = map;
	}

	public void AddPower(PowerObject power)
	{
		for (int i = 1; i < allowedPowers; i++) {
			powers[i] = powers[i - 1];
		}
		powers[0] = power;
	}

	public void AddUpgrade(StatModifier upgrade)
	{
		bool flag = true;
		List<StatModifier> newUpgrades = new List<StatModifier>(upgrades);
		for (int i = 0; i < upgrades.Count; i++) {
			if (upgrades[i].Signature == upgrade.Signature) {
				newUpgrades[i] = upgrade;
				flag = false;
			}
		}
		if (flag) {
			newUpgrades.Add(upgrade);
		}
		upgrades = new List<StatModifier>(newUpgrades);
	}



	public List<Weapon> GetShooters()
	{
		return shooters;
	}

	public List<Song> GetSongs()
	{
		return enchantSongs;
	}

	public List<PowerObject> GetPowers()
	{
		return powers;
	}

	public List<StatModifier> GetUpgrades()
	{
		return upgrades;
	}

	public List<Equippable> GetAllEquipped()
	{
		List<Equippable> shooters = new List<Equippable>(GetShooters());
		List<Equippable> songs = new List<Equippable>(GetSongs());
		List<Equippable> powers = new List<Equippable>(GetPowers());
		List<Equippable> upgrades = new List<Equippable>(GetUpgrades());
		return shooters.Concat(songs).Concat(powers).Concat(upgrades).ToList();
	}
}
