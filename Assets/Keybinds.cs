using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keybinds : MonoBehaviour
{
	public static Keybinds instance;
	public Dictionary<string, KeyCode> keys;

    void Awake(){
    	if(instance==null)
    		instance = this;
    	else{
    		Destroy(gameObject);
    		return;
    	}
    	DontDestroyOnLoad(gameObject);

    	keys = new Dictionary<string, KeyCode>();
    	keys.Add("Sprint", KeyCode.LeftShift);
    	keys.Add("Strafe", KeyCode.Space);
    	keys.Add("Enchant", KeyCode.G);
    }
}
