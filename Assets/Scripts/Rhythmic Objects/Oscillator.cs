using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RhythmicObject))]
public class Oscillator : MonoBehaviour
{
	RhythmicObject shooter;

	[Range(0,360)]
    public float oscillationMagnitude;
    [Range(0.01f,10)]
    public float oscillationPeriodSeconds;
    public bool constantRotation;

    void FixedUpdate()
    {
    	float angleToRotate;
    	if(!constantRotation){
	        angleToRotate = Time.deltaTime*Mathf.PI*oscillationMagnitude/oscillationPeriodSeconds
	        	*Mathf.Cos(Time.time*2*Mathf.PI/oscillationPeriodSeconds); //simple calculs
    	}
	    else{
	    	angleToRotate = Time.fixedDeltaTime*360/oscillationPeriodSeconds;
	    }
	    transform.Rotate(0,0,angleToRotate);
    }
}
