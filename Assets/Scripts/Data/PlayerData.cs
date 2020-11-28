using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int coins;
    public int lastLevelPassed;
    public List<string> ownedItems;
    public string mySong;
    public string myBomb;
    public int allowedWeapons;
    public List<string> myWeapons;
    public int totalBombs;
    public int totalLives;
    public List<string> statModifiers;
    public string selectedPlayer;

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
