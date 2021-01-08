using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RhythmicObject))]
public class Oscillator : MonoBehaviour
{
	[Range(0,360)]
    public float oscillationMagnitude;
    [Range(0.01f,60)]
    public float oscillationPeriodSeconds;

    public bool stationaryCenterOfRotation;
    public bool constantRotation;
    public bool reverse;

    float fixedCenterOfRotation;

    void Start(){
        fixedCenterOfRotation = transform.eulerAngles.z;
    }

    void FixedUpdate()
    {
    	float angleToRotate;
        if(stationaryCenterOfRotation){
            float targetAngle;
            targetAngle = fixedCenterOfRotation+oscillationMagnitude/2*Mathf.Sin(Time.fixedTime*2*Mathf.PI/oscillationPeriodSeconds);
            transform.rotation = Quaternion.Euler(0,0,targetAngle);
            return;
        }
    	else if(!constantRotation){
	        angleToRotate = Time.fixedDeltaTime*Mathf.PI*oscillationMagnitude/oscillationPeriodSeconds
	        	*Mathf.Cos(Time.fixedTime*2*Mathf.PI/oscillationPeriodSeconds); //simple calculs
    	}
	    else{
	    	angleToRotate = reverse?-Time.fixedDeltaTime*360/oscillationPeriodSeconds:Time.fixedDeltaTime*360/oscillationPeriodSeconds;
	    }
	    transform.Rotate(0,0,angleToRotate);
    }
}
