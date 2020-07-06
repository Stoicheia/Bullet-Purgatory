using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    public int kills {get; private set;}

    void Start()
    {
        kills = 0;
    }

    void IncrementKills (string s) => kills++;

    void OnEnable()
    {
    	Enemy.OnDeath += IncrementKills;
    }

    void OnDisable()
    {
    	Enemy.OnDeath -= IncrementKills;
    }

    void Update()
    {
        
    }
}
