using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadShooter : Shooter
{
	public int numberOfBullets;
	public float minAngle; public float maxAngle;
    public bool randomSpread;

    public override void Shoot(){
        if(!shootEnabled) return;

        float trueMaxAngle = (maxAngle>minAngle)? maxAngle:maxAngle+360;
    	float interval = (numberOfBullets!=1)? (trueMaxAngle-minAngle)/(numberOfBullets-1) : (trueMaxAngle-minAngle)/2;
    	for(int i=0;i<numberOfBullets;i++){
    		float angleToShoot = randomSpread? Random.Range(minAngle,trueMaxAngle):interval*i+minAngle;
            Bullet toSpawn = GetBullet();
    		Bullet b = pooler.Spawn(toSpawn.gameObject, toSpawn.poolTag, transform.position, Quaternion.Euler(0,0,angleToShoot)*transform.rotation)
    						 .GetComponent<Bullet>();
        	b.SetSpeed(bulletSpeed);
       	    b.SetFriendly(friendly);
    	}
        FindObjectOfType<SFXManager>().Play("Shot");
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
        min %= 360;
        max %= 360;
    }

    public void AdjustRandom(bool r){
        randomSpread = r;
    }
}
