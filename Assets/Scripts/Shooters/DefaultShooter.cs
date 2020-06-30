using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultShooter : Shooter
{
    public override void Shoot(){
    	Bullet b = Instantiate(bullet, transform.position, transform.rotation) as Bullet;
        b.SetSpeed(bulletSpeed);
        b.SetFriendly(friendly);
        FindObjectOfType<SFXManager>().Play("Shot");
    }
}
