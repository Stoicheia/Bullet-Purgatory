using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EndScreenDisplay : MonoBehaviour
{ 
	StatsManager stats;

	public TextMeshProUGUI killCount;
    public TextMeshProUGUI grazing;
    public TextMeshProUGUI livesLeft;
    public TextMeshProUGUI timeTaken;

    void Start()
    {
        stats = FindObjectOfType<StatsManager>();

        killCount.text = stats.kills.ToString();
        grazing.text = Mathf.Round(stats.grazing).ToString();
        livesLeft.text = stats.lives.ToString();
        timeTaken.text = Mathf.Floor(stats.totalSongTime/60).ToString() +":"+ Mathf.Round(stats.totalSongTime%60).ToString();
    }
}
