using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultShooter : Shooter
{
	public delegate void ShootAction();
	public static event ShootAction OnShoot;

    public override void Shoot(){
    	if(!shootEnabled) return;

    	Bullet toSpawn = GetBullet();
    	Bullet b = pooler.Spawn(toSpawn.gameObject, toSpawn.poolTag, transform.position, transform.rotation).GetComponent<Bullet>();
        b.SetSpeed(bulletSpeed);
        b.SetFriendly(friendly);
        if(OnShoot!=null)
        	OnShoot();
    }
}
