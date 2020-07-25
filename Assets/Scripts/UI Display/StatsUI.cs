using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StatsUI : MonoBehaviour
{
	public StatsManager stats;

	public TextMeshProUGUI grazing;
	void UpdateGrazing() => grazing.text = Mathf.Round(stats.grazing).ToString();

	public TextMeshProUGUI wave;
	void UpdateWave() => wave.text = stats.currentWave.ToString() + "/" + stats.totalWaves.ToString();

	public Image loadingBar;
	public Image progress;
	Color DEF_PROGRESS_COLOR = new Color(60f/255f, 200f/255f, 80f/255f, 0.8f);
	Color ALT_PROGRESS_COLOR = new Color(60f/255f, 180f/255f, 220f/255f, 0.8f);
	void UpdateSongProgress(){
		float songProgress = stats.currentSongTime/(0.001f+stats.currentSongLength);
		progress.transform.localScale = new Vector3(songProgress,1,1);
		progress.color = stats.isMainSong? DEF_PROGRESS_COLOR:ALT_PROGRESS_COLOR;
	}

    void Update()
    {
    	UpdateGrazing();
    	UpdateWave();
    	UpdateSongProgress();
    }
}
