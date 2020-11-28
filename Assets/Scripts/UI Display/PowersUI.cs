using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowersUI : MonoBehaviour
{
	List<Image> powers = new List<Image>();
	PowerUser trackedPoweruser;

	void Awake()
	{
		foreach(Transform power in transform)
		{
			powers.Add(power.GetComponent<Image>());
		}    
	}

    void Start()
    {
        trackedPoweruser = FindObjectOfType<PowerUser>();
        Refresh();
    }

    void OnEnable()
    {
    	PowerUser.OnActivate += Refresh;
    }

    void OnDisable()
    {
    	PowerUser.OnActivate -= Refresh;
    }

    void Update()
    {
        if(trackedPoweruser==null){
            trackedPoweruser = FindObjectOfType<PowerUser>();
            if(trackedPoweruser!=null)
                Refresh();
        }
    }

    
    void Refresh()
    {
    	int powerCount = trackedPoweruser.PowersRemaining;
        for(int i=0; i<powerCount; i++){
        	powers[i].sprite = trackedPoweruser.GetPower(i).Icon;
        	powers[i].gameObject.SetActive(true);
        }
        for(int i=powerCount; i<powers.Count; i++)
        {
            powers[i].gameObject.SetActive(false);
        }
    }
}
