using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUI : MonoBehaviour
{
	List<Image> powers = new List<Image>();
	public Enchanter trackedEnchanter;

	void Awake()
	{
		foreach(Transform power in transform)
		{
			powers.Add(power.GetComponent<Image>());
		}    
	}

    void Start()
    {
    	if(trackedEnchanter==null)
    		trackedEnchanter = FindObjectOfType<Enchanter>();
    	Refresh();
    }

    void OnEnable()
    {
    	Enchanter.OnUsePower += Refresh;
    	if(trackedEnchanter!=null)
    		Refresh();
    }

    void OnDisable()
    {
    	Enchanter.OnUsePower -= Refresh;
    }

    
    void Refresh()
    {
    	int powerCount = powers.Count/2;
        for(int i=0; i<powerCount; i++){
        	bool upToMe = i<trackedEnchanter.CurrentEnchantment;
        	powers[i+powerCount].gameObject.SetActive(upToMe);
        	powers[i].gameObject.SetActive(!upToMe);
        }
    }
}
