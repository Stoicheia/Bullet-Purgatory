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
    GlobalStats globalStats;

    void Awake()
    {
        globalStats = GlobalStats.instance;
    	selectedPlayer = globalStats.SelectedPlayer;
    	player = Instantiate(selectedPlayer, transform.position, transform.rotation);
    	enchanter = player.GetComponent<Enchanter>();
    	powerUser = player.GetComponent<PowerUser>();

        player.maxLives = globalStats.TotalLives;
    	foreach(var wep in globalStats.MyWeapons){
    		player.AddShooter(wep.Shooter);
    	}
        if(globalStats.MySong!=null)
            enchanter.AddSong(globalStats.MySong);
    	enchanter.UpdateSongs();
        if (globalStats.MyBomb != null)
        {
            for (int i = 0; i < globalStats.TotalBombs; i++)
            {
                powerUser.AddPower(globalStats.MyBomb);
            }
        }
    	foreach(var upgrade in globalStats.StatModifiers){
            if(upgrade!=null)
    		    upgrade.Apply(player);
    	}
    }
}
