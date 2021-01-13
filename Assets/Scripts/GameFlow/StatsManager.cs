using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum CauseOfFail{DEATH, TIMEOUT}

public class StatsManager : MonoBehaviour
{
    public delegate void ComboIncreaseAction();

    public event ComboIncreaseAction OnComboIncrease;

    public delegate void EnemyKillActionAfterCombo(string tag, Enemy e);

    public event EnemyKillActionAfterCombo OnEnemyKilledAfterCombo;
    
    public int kills {get; private set;}
    public int lives {get; private set;} 
    public float grazing {get; private set;}
    public int coins {get; private set;}
    public CauseOfFail causeOfFail {get; private set;}
    public float score { get; private set; }
    public int killCombo { get; private set; }

    public void AddScore(float s)
    {
        score += s;
        
    }

    public int currentWave {get; private set;}
    public int totalWaves {get; private set;}
    public int totalNormalWaves { get; private set; }

    RhythmMapsManager rhythmMaps;
    public float currentSongTime {get; private set;}
    public float currentSongLength {get; private set;}
    public float totalSongTime {get; private set;}
    public bool isMainSong {get; private set;}
    public bool isNoSong { get; private set; }

    void Start()
    {
        rhythmMaps = FindObjectOfType<RhythmMapsManager>();
        kills = 0;
        grazing = 0;
        score = 0;
        ResetCombo();
    }

    void OnEnable()
    {
    	Enemy.OnDeath += IncrementKills;
        Enemy.OnDeath += GiveCombo;
        Player.OnPlayerHit += ResetCombo;
        Player.OnPlayerDeath += SetFailFromDeath;
        Grazebox.OnGraze += AddGraze;
        RhythmMapsManager.OnAllSongsFinished += SetFailFromTimeout;
        WaveSpawner.OnNextWave += UpdateWaveInfo;
        Player.OnLivesChange += ChangeLives;
        ItemPicker.OnCoinPickup += UpdateCoins;
    }

    void OnDisable()
    {
        Enemy.OnDeath -= IncrementKills;
        Enemy.OnDeath -= GiveCombo;
        Player.OnPlayerHit -= ResetCombo;
        Player.OnPlayerDeath -= SetFailFromDeath;
        Grazebox.OnGraze -= AddGraze;
        RhythmMapsManager.OnAllSongsFinished -= SetFailFromTimeout;
        WaveSpawner.OnNextWave -= UpdateWaveInfo;
        Player.OnLivesChange -= ChangeLives;
        ItemPicker.OnCoinPickup -= UpdateCoins;
    }

    void Update()
    {
        UpdateSongProgress();
    }

    void IncrementKills (string s, Enemy e) => kills++;
    void ChangeLives (int l) => lives = l;
    void AddGraze (float s, Transform t) => grazing += s*(1+killCombo);
    void UpdateWaveInfo(int[] waveInfo) {
        currentWave = waveInfo[0]; 
        totalWaves = waveInfo[1];
        totalNormalWaves = waveInfo[2];
    }
    void UpdateSongProgress(){
        float[] songInfo = rhythmMaps.GetCurrentMapInfo();
        currentSongTime = songInfo[0];
        currentSongLength = songInfo[1];
        totalSongTime = rhythmMaps.ActiveTime;
        isMainSong = rhythmMaps.IsStartingMap();
        isNoSong = rhythmMaps.IsNoMap();
    }
    void UpdateCoins (int c, Player p) => coins += c;
    void SetFailFromDeath () => causeOfFail = CauseOfFail.DEATH;
    void SetFailFromTimeout () => causeOfFail = CauseOfFail.TIMEOUT;
    void ResetCombo() => killCombo = 0;

    void GiveCombo(string deadEnemy, Enemy e)
    {
        if (e.WasKilled())
        {
            killCombo = e.GetComponent<Boss>() == null ? killCombo + 1 : killCombo + 10;
            OnComboIncrease?.Invoke();
        }
        OnEnemyKilledAfterCombo?.Invoke(deadEnemy, e);
    }

    public void FeedToGlobal()
    {
        GlobalStats gstats = GlobalStats.instance;
        gstats.AddCoins(coins);
        gstats.LevelData.UpdateScore(GlobalManager.instance.CurrentLevel, score);
        gstats.PassLevel(SceneManager.GetActiveScene().buildIndex);
    }
}
