using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EndScreenDisplay : MonoBehaviour
{ 
	StatsManager stats;

	public TextMeshProUGUI killCount;

    void Start()
    {
        stats = FindObjectOfType<StatsManager>();
        killCount.text = stats.kills.ToString(); 
    }


    void Update()
    {
        
    }
}
