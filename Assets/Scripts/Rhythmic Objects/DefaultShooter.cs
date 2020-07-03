using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultShooter : RhythmicObject
{
    public override void Shoot(){
    	if(!shootEnabled) return;

    	Bullet toSpawn = GetBullet();
    	Bullet b = pooler.Spawn(toSpawn.gameObject, toSpawn.poolTag, transform.position, transform.rotation).GetComponent<Bullet>();
        b.SetSpeed(bulletSpeed);
        b.SetFriendly(friendly);
        FindObjectOfType<SFXManager>().Play("Shot");
    }
}
