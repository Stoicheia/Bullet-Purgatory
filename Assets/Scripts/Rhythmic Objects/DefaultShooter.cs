using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultShooter : Shooter
{
	public delegate void ShootAction();
	public static event ShootAction OnShoot;

    public override void Shoot(){
    	if(!shootEnabled) return;

    	Bullet b = SpawnFromPoolAtAngle(0);
        if(OnShoot!=null)
        	OnShoot();
    }
}
