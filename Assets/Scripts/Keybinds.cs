using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Keybinds : MonoBehaviour
{
	public static Keybinds instance;
	public Dictionary<string, KeyCode> keys;
    public Dictionary<string, bool> actionEnabled;

    void Awake(){
    	if(instance==null)
    		instance = this;
    	else{
    		Destroy(gameObject);
    		return;
    	}
    	DontDestroyOnLoad(gameObject);

    	keys = new Dictionary<string, KeyCode>();
        actionEnabled = new Dictionary<string, bool>();
        keys.Add("Up", KeyCode.W);
        keys.Add("Left", KeyCode.A);
        keys.Add("Right", KeyCode.D);
        keys.Add("Down", KeyCode.S);
    	keys.Add("Sprint", KeyCode.RightShift);
    	keys.Add("Strafe", KeyCode.LeftShift);
    	keys.Add("Enchant", KeyCode.G);
        keys.Add("Power", KeyCode.F);
        keys.Add("SkipDialogue", KeyCode.Space);
        keys.Add("Pause", KeyCode.Escape);

        foreach(string action in keys.Keys){
            actionEnabled.Add(action, true);
        }   
    }

    void Start(){
        
    }

    public bool GetInput (string action) => Input.GetKey(keys[action])&&actionEnabled[action];
    public bool GetInputDown (string action) => Input.GetKeyDown(keys[action])&&actionEnabled[action];
    public bool GetInputUp (string action) => Input.GetKeyUp(keys[action])&&actionEnabled[action];

    int BoolToInt (bool b) => b ? 1 : 0;
    public Vector2 GetInputAxis(){
        float x = BoolToInt(GetInput("Right"))-BoolToInt(GetInput("Left"));
        float y = BoolToInt(GetInput("Up"))-BoolToInt(GetInput("Down"));
        return new Vector2(x,y);
    }

    public void Enable (string action)  => actionEnabled[action] = true;
    public void Disable (string action) => actionEnabled[action] = false;
    public void Rebind (string action, KeyCode k) => keys[action] = k;
    
    public void DisableAll(){
        foreach(string action in actionEnabled.Keys.ToList()){
            actionEnabled[action] = false;
        }
        actionEnabled["Pause"] = true;
    }

    public void EnableAll(){
        foreach(string action in actionEnabled.Keys.ToList()){
            actionEnabled[action] = true;
        }
    }

    public void SetDialogueBinds(){
        EnableAll();
        Disable("Enchant");
        Disable("Power");
    }

    public void SetPlayBinds(){
        EnableAll();
        Disable("SkipDialogue");
    }
}
