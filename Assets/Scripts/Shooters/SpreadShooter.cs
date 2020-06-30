using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadShooter : Shooter
{
	public int numberOfBullets;
	public float minAngle; public float maxAngle;

    public override void Shoot(){
    	float interval = (maxAngle-minAngle)/(numberOfBullets-1);
    	for(int i=0;i<numberOfBullets;i++){
    		float angleToShoot = interval*i+minAngle;
    		Bullet b = Instantiate(bullet, transform.position, Quaternion.Euler(0,0,angleToShoot)*transform.rotation)
    						 as Bullet;
        	b.SetSpeed(bulletSpeed);
       	    b.SetFriendly(friendly);
    	}
        FindObjectOfType<SFXManager>().Play("Shot");
    }
}
