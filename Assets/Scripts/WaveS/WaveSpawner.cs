using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WaveSpawner : RhythmicObject
{
	public delegate void StateChangeAction(LevelState state);
	public static event StateChangeAction GoToState;

    public delegate void WaveSpawnAction(int[] waveInfo);
    public static event WaveSpawnAction OnNextWave;

    public Power[] powersBetweenWaves;

	RhythmListener listener;
	List<string> enemiesOnScreen;
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
        enemiesOnScreen = new List<string>();
    	currentWave = -1;
        nextWave = 0;
        if(OnNextWave!=null)
            OnNextWave(GetWaveInfo());
    }


    void Update()
    {
        if(enemiesOnScreen.Count <= 0 && spawnStarted){
	        foreach (Power power in powersBetweenWaves)
	        {
		        power.Activate();
	        }
        	SpawnNextWave();
        }
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
    	StartCoroutine(SpawnActiveWaveAfter(TimeBetweenWaves()));
    	enemiesOnScreen = activeWave.GetEnemyTags();
        if(OnNextWave!=null)
            OnNextWave(GetWaveInfo());
    }

    IEnumerator SpawnActiveWaveAfter(float wait)
    {
        yield return new WaitForSeconds(wait);
        activeWave.Spawn();
    }

    IEnumerator WinSequence()
    {
        StopSpawning();
        yield return new WaitForSeconds(TimeBetweenWaves()*2);
        GoToState(LevelState.ENDSCREEN);
    }

    void KilledOneEnemy(string tagOfDead, Enemy e){
        enemiesOnScreen.Remove(tagOfDead);
    	Debug.Log(enemiesOnScreen.Count);
    }

    public void StartSpawning(){
    	spawnStarted = true;
    }

    public void StopSpawning(){
    	spawnStarted = false;
    }

    public int[] GetWaveInfo(){
        return new int[] {currentWave+1,totalWaves};
    }

    public float TimeBetweenWaves()
    {
	    return listener.BeatsToSeconds(2);
    }
}
