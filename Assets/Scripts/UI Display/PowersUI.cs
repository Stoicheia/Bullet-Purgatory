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

    }

    void OnEnable()
    {
    	PowerUser.OnActivate += Refresh;
    	if(trackedPoweruser!=null)
    		Refresh();
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
    	int powerCount = powers.Count;
        for(int i=0; i<powerCount; i++){
        	powers[i].sprite = PowerIcons.instance.GetPowerIcon(trackedPoweruser.GetPower(i));
        	bool upToMe = i<trackedPoweruser.CurrentPower;
        	powers[i].gameObject.SetActive(!upToMe);
        }
    }
}
