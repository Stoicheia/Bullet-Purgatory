using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultShooter : RhythmicObject
{
    public override void Shoot(){
    	if(!shootEnabled) return;

    	Bullet b = Instantiate(GetBullet(), transform.position, transform.rotation) as Bullet;
        b.SetSpeed(bulletSpeed);
        b.SetFriendly(friendly);
        FindObjectOfType<SFXManager>().Play("Shot");
    }
}
