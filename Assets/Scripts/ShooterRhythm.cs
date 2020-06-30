using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Shooter))]
public class ShooterRhythm : MonoBehaviour
{
	RhythmMap map;
	public string part;
	SongLine line;
	Shooter shooter;

    void Start()
    {
    	shooter = GetComponent<Shooter>();
        map = Object.FindObjectOfType(typeof(RhythmMap)) as RhythmMap;
        line = map.GetLine(part);
        line.Tick += Act;
        line.Switch += ChangeStyle;
    }

    void OnDisable(){
    	line.Tick -= Act;
    	line.Switch -= ChangeStyle;
    }


    void Update()
    {
        
    }

    public void Act(){
    	shooter.Shoot();
    }

    public void ChangeStyle(int i){
    	shooter.Change(i);
    }
}
