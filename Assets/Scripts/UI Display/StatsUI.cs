using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StatsUI : MonoBehaviour
{
	public StatsManager stats;
	private LevelStateManager stateManager;

	public TextMeshProUGUI grazing;
	void UpdateGrazing() => grazing.text = Mathf.Round(stats.grazing).ToString();

	public TextMeshProUGUI wave;
	void UpdateWave() => wave.text = stats.currentWave.ToString() + "/" + stats.totalWaves.ToString();

	public Image loadingBar;
	public Image progress;
	Color DEF_PROGRESS_COLOR = new Color(60f/255f, 200f/255f, 80f/255f, 0.8f);
	Color ALT_PROGRESS_COLOR = new Color(60f/255f, 180f/255f, 220f/255f, 0.8f);

	private float enterMainTime;
	private bool enteredMainTime = false;
	private float enterMainProgress;
	void UpdateSongProgress(){ //i dont care
		if (stateManager.levelState == LevelState.PLAYING && !enteredMainTime)
		{
			enterMainTime = Time.time;
			enteredMainTime = true;
			enterMainProgress = (Time.time - firstTime)/songBarPreLoadSeconds;
		}
		if (stateManager.levelState == LevelState.DIALOGUE)
		{
			enteredMainTime = false;
			progress.color = DEF_PROGRESS_COLOR;
			progress.transform.localScale = new Vector3(Mathf.Min(1,(Time.time-firstTime)/songBarPreLoadSeconds), 1, 1);
		}
		else
		{
			if (Time.time - enterMainTime <= songBarLoadSeconds)
			{
				progress.color = DEF_PROGRESS_COLOR;
				progress.transform.localScale = new Vector3(Mathf.Min(1,enterMainProgress+(1-enterMainProgress)*(Time.time-enterMainTime)/songBarLoadSeconds), 1, 1);
			}
			else
			{
				float songProgress = stats.currentSongTime / (0.001f + stats.currentSongLength);
				progress.transform.localScale = new Vector3(1 - songProgress, 1, 1);
				progress.color = stats.isMainSong || stats.isNoSong ? DEF_PROGRESS_COLOR : ALT_PROGRESS_COLOR;
			}
		}
	}

	public TextMeshProUGUI coins;
	void UpdateCoins() => coins.text = stats.coins.ToString();

	public TextMeshProUGUI score;
	void UpdateScore() => score.text = ((int) stats.score).ToString();

	public TextMeshProUGUI combo;
	void UpdateCombo() => combo.text = stats.killCombo.ToString();

	private float firstTime;
	private void Start()
	{
		firstTime = Time.time;
		LevelStateManager[] managers = FindObjectsOfType<LevelStateManager>();
		bool noManagers = false;
		if (managers.Length == 0)
		{
			Debug.LogError("No Level State Manager Found!", this);
			noManagers = true;
		}
		if(managers.Length>1) Debug.LogError("Multiple Level State Managers Found!", this);
		if (!noManagers)
		{
			stateManager = managers[0];
		}
	}

	void Update()
    {
    	UpdateGrazing();
    	UpdateWave();
    	UpdateSongProgress();
    	UpdateCoins();
        UpdateScore();
        UpdateCombo();
    }

	public float songBarPreLoadSeconds = 1800;
	public float songBarLoadSeconds = 1;
}
