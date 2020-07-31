using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    Player selectedPlayer;
    Player player;
    Enchanter enchanter;
    PowerUser powerUser;
    PlayerLoadoutManager playerLoadout;
    PlayerLoadout selectedLoadout;

    void Start()
    {
    	playerLoadout =  PlayerLoadoutManager.instance;
    	selectedPlayer = playerLoadout.selectedPlayer;
    	selectedLoadout = playerLoadout.loadouts[selectedPlayer];
    	player = Instantiate(selectedPlayer, transform.position, transform.rotation);
    	enchanter = player.GetComponent<Enchanter>();
    	powerUser = player.GetComponent<PowerUser>();
    	foreach(var shooter in selectedLoadout.GetShooters()){
    		player.AddShooter(shooter);
    	}
    	foreach(var song in selectedLoadout.GetSongs()){
    		enchanter.AddSong(song);
    	}
    	enchanter.UpdateSongs();
    	foreach(var power in selectedLoadout.GetPowers()){
    		powerUser.AddPower(power);
    	}
    	foreach(var upgrade in selectedLoadout.GetUpgrades()){
    		upgrade.Activate(player);
    	}
    }
}
