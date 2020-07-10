using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WaveSpawner : RhythmicObject
{
	public delegate void StateChangeAction(LevelState state);
	public static event StateChangeAction GoToState; 

	RhythmListener listener;
	int enemiesOnScreen = 0;
	public Wave[] waves;
	int currentWave;
	int nextWave;
	int totalWaves;

	Wave activeWave;

	bool spawnStarted = false;

	protected override void Awake(){
		base.Awake();
		listener = GetComponent<RhythmListener>();
		if(listener == null){
			listener = gameObject.AddComponent<RhythmListener>();
			listener.part = "Wave";
		}
		totalWaves = waves.Length;
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
        if(enemiesOnScreen <= 0 && spawnStarted)
        	SpawnNextWave();
    }

    public override void Shoot(){

    }

    public void SpawnNextWave(){
    	currentWave++; nextWave++;
    	if(currentWave==totalWaves){
    		StartCoroutine(WinSequence());
    		return;
    	}
    	activeWave = waves[currentWave%totalWaves];
    	StartCoroutine(SpawnActiveWaveAfter(listener.BeatsToSeconds(2)));
    	enemiesOnScreen += activeWave.GetEnemyCount();
    }

    IEnumerator SpawnActiveWaveAfter(float wait)
    {
        yield return new WaitForSeconds(wait);
        activeWave.Spawn();
    }

    IEnumerator WinSequence()
    {
        StopSpawning();
        yield return new WaitForSeconds(listener.BeatsToSeconds(4));
        GoToState(LevelState.ENDSCREEN);
    }

    void KilledOneEnemy(string tagOfDead){
    	enemiesOnScreen--;
    	Debug.Log(enemiesOnScreen);
    }

    public void StartSpawning(){
    	spawnStarted = true;
    }

    public void StopSpawning(){
    	spawnStarted = false;
    }


}
