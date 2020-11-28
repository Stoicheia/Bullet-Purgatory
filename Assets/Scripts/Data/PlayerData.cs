using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int coins;
    public int lastLevelPassed;
    public List<Equippable> ownedItems;
    public Song mySong;
    public PowerObject myBomb;
    public int allowedWeapons;
    public List<Weapon> myWeapons;
    public int totalBombs;
    public int totalLives;
    public List<StatModifier> statModifiers;
    public Player selectedPlayer;

    public PlayerData(PlayerData toCopy)
    {
        coins = toCopy.coins;
        lastLevelPassed = toCopy.lastLevelPassed;
        ownedItems = toCopy.ownedItems;
        mySong = toCopy.mySong;
        myBomb = toCopy.myBomb;
        allowedWeapons = toCopy.allowedWeapons;
        myWeapons = toCopy.myWeapons;
        totalBombs = toCopy.totalBombs;
        totalLives = toCopy.totalLives;
        statModifiers = toCopy.statModifiers;
        selectedPlayer = toCopy.selectedPlayer;
    }
}
