using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public StatsManager levelStats;

    private void Awake()
    {
    }

    private void OnEnable()
    {
        Grazebox.OnGraze += AddScoreGraze;
        ItemPicker.OnCoinPickup += AddScoreCoin;
        levelStats.OnEnemyKilledAfterCombo += AddScoreKill;
    }
    
    private void OnDisable()
    {
        Grazebox.OnGraze -= AddScoreGraze;
        ItemPicker.OnCoinPickup -= AddScoreCoin;
        levelStats.OnEnemyKilledAfterCombo -= AddScoreKill;
    }

    void AddScoreGraze(float t, Transform trans)
    {
        levelStats.AddScore(Graze2Score(t)*(1+levelStats.killCombo));
    }

    void AddScoreKill(string t, Enemy e)
    {
        if(e.WasKilled())
            levelStats.AddScore(Kill2Score(e)*levelStats.killCombo);
    }

    void AddScoreCoin(int amount, Player p)
    {
        levelStats.AddScore(Coin2Score(amount));
    }


    public static float Graze2Score(float t)
    {
        return t;
    }

    public static float Kill2Score(Enemy e)
    {
        return e.maxHP * 10;
    }

    public static float Coin2Score(int amount)
    {
        return (float) amount * 100;
    }
}
