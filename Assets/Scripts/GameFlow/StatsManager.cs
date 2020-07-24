using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    public int kills {get; private set;}
    public string causeOfFail {get; private set;}

    void Start()
    {
        kills = 0;
    }

    void OnEnable()
    {
    	Enemy.OnDeath += IncrementKills;
        Player.OnPlayerDeath += SetFailFromDeath;
        RhythmMapsManager.OnAllSongsFinished += SetFailFromTimeout;
    }

    void OnDisable()
    {
    	Enemy.OnDeath -= IncrementKills;
    }

    void IncrementKills (string s) => kills++;
    void SetFailFromDeath () => causeOfFail = "You died!";
    void SetFailFromTimeout () => causeOfFail = "You have run out of Songpower!";
}
