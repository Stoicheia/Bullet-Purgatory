using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUser : MonoBehaviour
{
	public delegate void PowerAction();
	public static event PowerAction OnActivate;

	[SerializeField]
	List<PowerObject> powers = new List<PowerObject>();

    public int CurrentPower { get; private set; }
    public int PowersRemaining { get { return powers.Count - CurrentPower; } }

    void Awake()
	{
		CurrentPower = 0;
	}

    void Start()
    {
        
    }


    void Update()
    {
        if(Keybinds.instance.GetInputDown("Power")){
        	if(CurrentPower>=powers.Count) return;
            powers[CurrentPower].UseEffect();
        	CurrentPower++;
        	if(OnActivate!=null)
        		OnActivate();
        }
    }

    public void AddPower(PowerObject p){
    	powers.Add(p);
    }

    public PowerObject GetPower(int i){
        if (powers.Count == 0)
            return null;
    	return powers[i];
    }

    public void Reset(){
    	CurrentPower = 0;
    }
}
