using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUser : MonoBehaviour
{
	public delegate void PowerAction();
	public static event PowerAction OnActivate;

	[SerializeField]
	List<Power> powers = new List<Power>();
	int currentPower;
	public int CurrentPower{get{return currentPower;}}

	void Awake()
	{
		currentPower = 0;
	}

    void Start()
    {
        
    }


    void Update()
    {
        if(Keybinds.instance.GetInputDown("Power")){
        	if(currentPower>=powers.Count) return;
        	powers[currentPower].Activate();
        	currentPower++;
        	if(OnActivate!=null)
        		OnActivate();
        }
    }

    public void AddPower(Power p){
    	powers.Add(p);
    }

    public Power GetPower(int i){
    	return powers[i];
    }

    public void Reset(){
    	currentPower = 0;
    }
}
