using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadShooter : Shooter
{
    public delegate void ShootAction();
    public static event ShootAction OnShoot;

	public int numberOfBullets;
	public float minAngle; public float maxAngle;
    public bool randomSpread;

    public override void Shoot(){
        if(!shootEnabled) return;

        float trueMaxAngle = (maxAngle>minAngle)? maxAngle:maxAngle+360;
    	float interval = (numberOfBullets>1)? (trueMaxAngle-minAngle)/(numberOfBullets-1) : (trueMaxAngle-minAngle)/2;
    	for(int i=0;i<numberOfBullets;i++){
    		float angleToShoot = randomSpread? Random.Range(minAngle,trueMaxAngle):interval*i+minAngle;
            SpawnFromPoolAtAngle(angleToShoot);
    	}
        if(OnShoot!=null)
            OnShoot();
        ReadjustShootAngles(ref minAngle, ref maxAngle);
    }

    public void Rotate(float speed){
        minAngle+=speed*Time.deltaTime;
        maxAngle+=speed*Time.deltaTime;
    }

    public void Rotate(float minChange, float maxChange){
        minAngle+=minChange*Time.deltaTime;
        maxAngle+=maxChange*Time.deltaTime;
    }

    void ReadjustShootAngles(ref float min, ref float max){
        min = (min%360+360)%360;
        max = (max%360+360)%360;
    }

    public void AdjustRandom(bool r) => randomSpread = r;
}
