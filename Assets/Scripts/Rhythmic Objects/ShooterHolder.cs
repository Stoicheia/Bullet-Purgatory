using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterHolder : RhythmicObject
{
    public override void Shoot(){}

    void Update(){
    	foreach(Transform c in transform){
    		RhythmicObject shooter = c.GetComponent<RhythmicObject>();
    		if(shooter!=null)
    			shooter.shootEnabled = shootEnabled;
    	}
    }
}
