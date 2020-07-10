using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomSpreadShooter : Shooter
{
    public delegate void ShootAction();
    public static event ShootAction OnShoot;

    public AngleSpeedPair[] shotAngles;
    
    public override void Shoot(){
        if(!shootEnabled)return;
        
    	foreach(AngleSpeedPair asp in shotAngles){
    		SpawnFromPoolAtAngle(asp.angle);	
    		ReadjustShootAngle(ref asp.angle);	
    	}
    	if(OnShoot!=null)
    		OnShoot();
    }

    void Update(){
    	foreach(AngleSpeedPair asp in shotAngles){
    		asp.angle += asp.angularVel*Time.deltaTime;
    	}
    }

    void ReadjustShootAngle(ref float s){
        s = (s%360+360)%360;
    }

    [System.Serializable]
    public class AngleSpeedPair{
    	[Range(0,360)]
    	public float angle;
    	public float angularVel;
    	public AngleSpeedPair(float _angle, float _speed){
    		angle = _angle;
    		angularVel = _speed;
    	}
    }
}
