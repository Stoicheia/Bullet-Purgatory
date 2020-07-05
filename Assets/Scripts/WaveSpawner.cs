using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WaveSpawner : RhythmicObject
{
	RhythmListener listener;
	int enemiesOnScreen = 0;
	public Wave[] waves;
	int currentWave;
	int nextWave;

	protected override void Awake(){
		base.Awake();
		listener = GetComponent<RhythmListener>();
		if(listener == null){
			listener = gameObject.AddComponent<RhythmListener>();
			listener.part = "Wave";
		}
	}

	void OnEnable(){
		Enemy.OnDeath += KilledOneEnemy;
	}

	void OnDisable(){
		Enemy.OnDeath -= KilledOneEnemy;
	}

    void Start()
    {
    	currentWave = -1;
        nextWave = 0;
    }


    void Update()
    {
        if(enemiesOnScreen <= 0)
        	SpawnNextWave();
    }

    public override void Shoot(){

    }

    public void SpawnNextWave(){
    	currentWave++; nextWave++;
    	Wave waveToSpawn = waves[currentWave%waves.Length];
    	waveToSpawn.Spawn();
    	enemiesOnScreen += waveToSpawn.GetEnemyCount();
    }

    void KilledOneEnemy(string tagOfDead){
		Debug.Log(enemiesOnScreen);
    	enemiesOnScreen--;
    }


}
