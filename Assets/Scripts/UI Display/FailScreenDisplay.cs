using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FailScreenDisplay : MonoBehaviour
{
	StatsManager stats;
	public TextMeshProUGUI cause;

    void Start()
    {
        stats = FindObjectOfType<StatsManager>();
        UpdateInfo();      
    }

    void UpdateInfo()
    {
    	cause.text = stats.causeOfFail;
    }
}
