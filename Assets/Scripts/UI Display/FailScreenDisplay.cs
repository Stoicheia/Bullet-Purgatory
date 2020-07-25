using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FailScreenDisplay : MonoBehaviour
{
	StatsManager stats;
	public TextMeshProUGUI cause;

    public string deathMessage;
    public string timeoutMessage;

    Dictionary<CauseOfFail, string> causeText;

    void Awake(){
        causeText = new Dictionary<CauseOfFail, string>();
        causeText.Add(CauseOfFail.DEATH, deathMessage);
        causeText.Add(CauseOfFail.TIMEOUT, timeoutMessage);
    }

    void Start()
    {
        stats = FindObjectOfType<StatsManager>();
        UpdateInfo();      
    }

    void UpdateInfo()
    {
    	cause.text = causeText[stats.causeOfFail];
    }
}
