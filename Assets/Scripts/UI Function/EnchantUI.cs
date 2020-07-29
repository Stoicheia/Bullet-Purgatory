using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnchantUI : MonoBehaviour
{
	List<Image> powers = new List<Image>();
	Enchanter trackedEnchanter;

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
    	Enchanter.OnUsePower += Refresh;
    	if(trackedEnchanter!=null)
    		Refresh();
    }

    void OnDisable()
    {
    	Enchanter.OnUsePower -= Refresh;
    }

    void Update()
    {
        if(trackedEnchanter==null){
            trackedEnchanter = FindObjectOfType<Enchanter>();
            if(trackedEnchanter!=null)
                Refresh();
        }
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
